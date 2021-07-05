using UnityEngine;
using UnityEngine.UI;

// Attach this script to GameObjects that need to create toast
// Remember to attach the toastPrefab to script in the GameObject
public class ToastCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject toastPrefab;

    public void CreateToast(string message)
    {
        Debug.Log("CreateToast: " + message);
        GameObject newToast = (GameObject)Instantiate(toastPrefab);
        Text toastTextComponent = newToast.GetComponentInChildren<Text>();
        toastTextComponent.text = message;
    }
}
