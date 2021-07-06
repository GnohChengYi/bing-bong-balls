using UnityEngine;
using UnityEngine.UI;

public class LoginDialog : MonoBehaviour
{
    private bool shouldActive;
    private bool alertError;

    [SerializeField]
    private InputField emailField;
    [SerializeField]
    private InputField passwordField;
    [SerializeField]
    private ToastCreator toastCreator;

    private void Start()
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

    public void OnLoginClick()
    {
        string email = emailField.text;
        string password = passwordField.text;
        AccountManager.Instance.Login(email, password).ContinueWith(task => {
            bool success = task.Result;
            if (success) shouldActive = false;
            else alertError = true;
        });
    }
}
