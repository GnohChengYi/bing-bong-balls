using UnityEngine;
using UnityEngine.UI;

public class AccountMenu : MonoBehaviour
{
    [SerializeField]
    private Text nameTextComponent;

    private void OnEnable()
    {
        nameTextComponent.text = AccountManager.GetName();
    }
}
