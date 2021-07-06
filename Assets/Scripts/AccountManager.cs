using Firebase;
using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

// TODO handle no internet situation
// TODO add forgot password function
public class AccountManager : MonoBehaviour
{
    private FirebaseApp app;
    public FirebaseAuth auth;
    private static bool firebaseReady;
    private static bool firstTimeStartMenu = true;
    private bool welcomeUser;

    [SerializeField]
    private GameObject loginDialog;
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
        Debug.Log("SetUpFirebase");
        app = FirebaseApp.DefaultInstance;
        auth = FirebaseAuth.GetAuth(app);
        auth.StateChanged += AuthStateChanged;
    }

    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        Debug.Log("AuthStateChanged");
        if (auth.CurrentUser != null)
            Debug.LogFormat("auth.CurrentUser: {0}", auth.CurrentUser.Email);
        else loginDialog.SetActive(true);
    }

    public async Task<bool> IsExistingAccount(string email)
    {
        IEnumerable<string> providers = Enumerable.Empty<string>();
        await auth.FetchProvidersForEmailAsync(email).ContinueWith((authTask) =>
            {
                if (authTask.IsCanceled) Debug.Log("Provider fetch canceled.");
                else if (authTask.IsFaulted)
                {
                    Debug.Log("Provider fetch encountered an error.");
                    Debug.Log(authTask.Exception.ToString());
                }
                else if (authTask.IsCompleted) providers = authTask.Result;
            }
        );
        return providers != null && providers.Any<string>();
    }

    public async Task<bool> Register(string email, string name, string password)
    {
        Debug.Log("Registering");
        FirebaseUser newUser = null;
        await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
            else if (task.IsFaulted)
                Debug.LogError(String.Format(
                    "CreateUserWithEmailAndPasswordAsync encountered an error: {0}",
                    task.Exception));
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

    public void WelcomeUser()
    {
        if (!String.IsNullOrEmpty(auth.CurrentUser.DisplayName))
            toastCreator.CreateToast("Welcome!\n" + auth.CurrentUser.DisplayName);
        else welcomeUser = true;
    }

}
