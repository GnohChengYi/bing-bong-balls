using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class RegisterDialog : MonoBehaviour
{
    private bool shouldActive;
    private Regex regex = new Regex("^[^ ]+@[^ ]+$");
    private bool alertExistingAccount;

    [SerializeField]
    private InputField emailField;
    [SerializeField]
    private InputField nameField;
    [SerializeField]
    private InputField passwordField;
    [SerializeField]
    private InputField confirmPasswordField;
    [SerializeField]
    private ToastCreator toastCreator;

    private void Start()
    {
        shouldActive = true;
    }

    // Only runs when GameObject is active
    private void Update()
    {
        if (!shouldActive) gameObject.SetActive(false);
        if (alertExistingAccount)
        {
            toastCreator.CreateToast("Account already exists!");
            alertExistingAccount = false;
        }
    }

    public void OnRegisterClick()
    {
        string email = emailField.text;
        string name = nameField.text;
        string password = passwordField.text;
        string confirmPassword = confirmPasswordField.text;
        if (NoSimpleError(email, name, password, confirmPassword))
            AccountManager.Instance.IsExistingAccount(email).ContinueWith(task =>
                {
                    bool isExistingAccount = task.Result;
                    if (isExistingAccount) alertExistingAccount = true;
                    else
                        AccountManager.Register(email, name, password)
                            .ContinueWith(task =>
                                {
                                    bool success = task.Result;
                                    Debug.LogFormat("Register success: {0}", success);
                                    if (success) shouldActive = false;
                                });
                });
    }

    private bool NoSimpleError(
        string email, string name, string password, string confirmPassword)
    {
        if (!regex.IsMatch(email))
            toastCreator.CreateToast("Invalid email format!");
        else if (String.IsNullOrWhiteSpace(name))
            toastCreator.CreateToast("Invalid name!");
        else if (password != confirmPassword)
            toastCreator.CreateToast("Passwords must be the same!");
        else if (password.Length < 6)
            toastCreator.CreateToast("Password must be at least 6 characters!");
        else
            return true;
        return false;
    }
}
