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
    private static FirebaseDatabase database;
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

    // TODO check leave Start Menu then enter again
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
                        newUser.UpdateUserProfileAsync(profile);
                        Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                            newUser.DisplayName, newUser.UserId);
                        welcomeUser = true;
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
                    Debug.LogFormat("Signed In successfully: {0}, ({1})",
                        user.DisplayName, user.UserId);
                    welcomeUser = true;
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
                else if (task.IsCompleted) Debug.Log("SetValueAsync completed");
            });
        // TODO update global high score if required
    }

    public static bool SignedIn()
    {
        return auth.CurrentUser != null;
    }

    public void SignOut()
    {
        auth.SignOut();
    }

    private static string GetUserHighScorePath(string puzzle)
    {
        return "users/" + auth.CurrentUser.UserId + "/high-scores/" + puzzle;
    }
}
