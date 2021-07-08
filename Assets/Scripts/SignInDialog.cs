using UnityEngine;
using UnityEngine.UI;

public class SignInDialog : MonoBehaviour
{
    private bool shouldActive;
    private bool alertError;

    [SerializeField]
    private InputField emailField;
    [SerializeField]
    private InputField passwordField;
    [SerializeField]
    private ToastCreator toastCreator;

    private void OnEnable()
    {
        shouldActive = true;
    }

    private void Update()
    {
        if (!shouldActive) gameObject.SetActive(false);
        if (alertError)
        {
            toastCreator.CreateToast("Incorrect email or password!");
            alertError = false;
        }
    }

    public void OnSignInClick()
    {
        string email = emailField.text;
        string password = passwordField.text;
        AccountManager.SignIn(email, password).ContinueWith(task =>
        {
            bool success = task.Result;
            if (success) shouldActive = false;
            else alertError = true;
        });
    }
}
