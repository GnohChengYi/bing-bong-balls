using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFirebase : MonoBehaviour
{
    private Firebase.FirebaseApp app;
    private bool firebaseReady;
    private DatabaseReference reference;
    private bool testDone;

    private ToastCreator toastCreator;

    private void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app
                firebaseReady = true;

                TestAuth();
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

    }

    private void Update()
    {
        if (firebaseReady && !testDone)
        {
            reference = FirebaseDatabase.DefaultInstance.RootReference;
            string userId = String.Format("user{0}", DateTime.Now.Ticks);
            string puzzle = "3";
            reference.Child("users").Child(userId).Child(puzzle).SetValueAsync(12345);
            testDone = true;
        }
    }

    private void TestAuth()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.GetAuth(app);
        string testEmail = "test@gmail.com";
        string testPassword = "password";
        auth.CreateUserWithEmailAndPasswordAsync(testEmail, testPassword).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                string error = "CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception;
                Debug.LogError(error);
                return;
            }
            Firebase.Auth.FirebaseUser newUser = task.Result;
            string message = String.Format(
                "Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            Debug.Log(message);
        });
    }
}