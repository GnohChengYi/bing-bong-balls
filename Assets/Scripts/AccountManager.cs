using Firebase;
using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
    private FirebaseApp app;
    private FirebaseAuth auth;
    private static bool firebaseReady;
    private static bool firstTimeStartMenu = true;

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
        if (!firstTimeStartMenu) CheckAndFixDependenciesAsync();
    }

    private void Update()
    {
        if (firebaseReady && firstTimeStartMenu)
        {
            if (auth.CurrentUser != null) WelcomeUser();
            else loginDialog.SetActive(true);
            firstTimeStartMenu = false;
        }
    }

    private void CheckAndFixDependenciesAsync()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                app = FirebaseApp.DefaultInstance;
                firebaseReady = true;
                auth = FirebaseAuth.GetAuth(app);
            }
            else
                Debug.LogError(string.Format(
                    "Could not resolve all Firebase dependencies: {0}",
                    dependencyStatus));
        });
    }

    private void WelcomeUser()
    {
        toastCreator.CreateToast(auth.CurrentUser.DisplayName);
    }
}
