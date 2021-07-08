using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

// TODO handle no internet situation
// TODO add forgot password function
// TODO log out button
// TODO visitor mode (currently, must log in to play)
public class AccountManager : MonoBehaviour
{
    private static FirebaseApp app;
    private static FirebaseAuth auth;
    public static FirebaseDatabase database;
    private static bool firebaseReady;
    private static bool firstTimeStartMenu = true;
    private static bool welcomeUser;

    [SerializeField]
    private GameObject signInDialog;
    [SerializeField]
    private ToastCreator toastCreator;

    public static AccountManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Destroy(Instance);
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    private void Start()
    {
        if (firstTimeStartMenu) HandleFirstTimeStartMenu();
        else SetUpFirebase();
    }

    private void Update()
    {
        if (firebaseReady && firstTimeStartMenu)
        {
            SetUpFirebase();
            firstTimeStartMenu = false;
            welcomeUser = true;
        }
        if (welcomeUser && auth.CurrentUser != null)
        {
            welcomeUser = false;
            WelcomeUser();
        }
    }

    private void HandleFirstTimeStartMenu()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
                firebaseReady = true;
            else
                Debug.LogError(string.Format(
                    "Could not resolve all Firebase dependencies: {0}",
                    dependencyStatus));
        });
    }

    private void SetUpFirebase()
    {
        app = FirebaseApp.DefaultInstance;
        auth = FirebaseAuth.GetAuth(app);
        auth.StateChanged += AuthStateChanged;
        database = FirebaseDatabase.GetInstance(app);
    }

    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser == null) signInDialog.SetActive(true);
    }

    public async Task<bool> IsExistingAccount(string email)
    {
        IEnumerable<string> providers = Enumerable.Empty<string>();
        await auth.FetchProvidersForEmailAsync(email).ContinueWith((authTask) =>
            {
                if (authTask.IsCanceled) Debug.Log("Provider fetch canceled.");
                else if (authTask.IsFaulted) Debug.LogErrorFormat(
                    "Provider fetch encountered an error: {0}", authTask.Exception);
                else if (authTask.IsCompleted) providers = authTask.Result;
            }
        );
        return providers != null && providers.Any<string>();
    }

    public static async Task<bool> Register(string email, string name, string password)
    {
        FirebaseUser newUser = null;
        await auth.CreateUserWithEmailAndPasswordAsync(email, password)
            .ContinueWith(task =>
                {
                    if (task.IsCanceled)
                        Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    else if (task.IsFaulted)
                        Debug.LogErrorFormat(
                            "CreateUserWithEmailAndPasswordAsync encountered an error: {0}",
                            task.Exception);
                    else
                    {
                        newUser = task.Result;
                        UserProfile profile = new UserProfile();
                        profile.DisplayName = name;
                        newUser.UpdateUserProfileAsync(profile).ContinueWith(
                            updateTask => welcomeUser = true);
                        string path = GetUserNamePath(newUser.UserId);
                        database.GetReference(path).SetValueAsync(name);
                    }
                });
        return newUser != null;
    }

    public static async Task<bool> SignIn(string email, string password)
    {
        FirebaseUser user = null;
        await auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(
            task =>
            {
                if (task.IsCanceled)
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                else if (task.IsFaulted)
                    Debug.LogErrorFormat(
                        "SignInWithEmailAndPasswordAsync encountered an error : {0}",
                        task.Exception);
                else
                {
                    user = task.Result;
                    welcomeUser = true;
                    Debug.LogFormat("Signed In successfully: {0}, ({1})",
                        user.DisplayName, user.UserId);
                }
            }
        );
        return user != null;
    }

    public void WelcomeUser()
    {
        if (!String.IsNullOrEmpty(auth.CurrentUser.DisplayName))
            toastCreator.CreateToast("Welcome!\n" + auth.CurrentUser.DisplayName);
        else welcomeUser = true;
    }

    public static async Task<long?> GetUserHighScore(string puzzle)
    {
        if (!SignedIn()) return Int32.MaxValue;
        string path = GetUserHighScorePath(puzzle);
        long? highScore = null;
        await database.GetReference(path).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted) Debug.LogErrorFormat(
                "GetValueAsync encountered an error: {0}", task.Exception);
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists) highScore = (long?)snapshot.Value;
                else Debug.Log("snapshot does not exists");
            }
        });
        // null means no existing high score
        return highScore;
    }

    // updates user high score and global high score (if required)
    public static async void UpdateHighScore(string puzzle, int highScore)
    {
        Debug.Log("AccountManager::UpdateHighScore");
        string userPath = GetUserHighScorePath(puzzle);
        await database.GetReference(userPath).SetValueAsync(highScore)
            .ContinueWith(task =>
            {
                if (task.IsFaulted) Debug.LogErrorFormat(
                    "SetValueAsync encountered an error: {0}", task.Exception);
                else if (task.IsCompleted)
                    Debug.Log("Updated user high score on Firebase");
            });
        await GlobalHighScoresManager.GetHighScores(puzzle).ContinueWith(task =>
        {
            if (task.IsFaulted) Debug.LogErrorFormat(
                "GetHighScores encountered an error: {0}", task.Exception);
            else if (task.IsCompleted)
            {
                List<UserScore> highScores = task.Result;
                if (ShouldSubmitToGlobalHighScore(highScore, highScores))
                    GlobalHighScoresManager.AddHighScore(puzzle, GetUserId(), highScore);
            }
        });
    }

    public static string GetUserId()
    {
        return auth.CurrentUser.UserId;
    }

    public static bool SignedIn()
    {
        return auth.CurrentUser != null;
    }

    public void SignOut()
    {
        auth.SignOut();
    }

    public static async Task<string> GetDisplayNameByUserId(string userId)
    {
        string displayName = "ERROR: NO NAME";
        await database.GetReference(GetUserNamePath(userId)).GetValueAsync().
            ContinueWith(task => displayName = (string)task.Result.Value);
        return displayName;
    }

    private static string GetUserHighScorePath(string puzzle)
    {
        return "users/" + auth.CurrentUser.UserId + "/high-scores/" + puzzle;
    }

    private static string GetUserNamePath(string userId)
    {
        return "users/" + userId + "/name";
    }

    private static bool ShouldSubmitToGlobalHighScore(
        int highScore, List<UserScore> highScores)
    {
        if (highScores.Count < GlobalHighScoresManager.limit) return true;
        return highScore > highScores[highScores.Count - 1].score;
    }
}
