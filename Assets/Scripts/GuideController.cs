using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO check guide quit guide again, verify guide progress correct
public class GuideController : MonoBehaviour
{
    [SerializeField]
    private GameObject guideTextObject;

    // 1-indexing
    private int guideProgress = 1;

    private void OnEnable()
    {
        guideProgress = 1;
    }

    public void ContinueGuide()
    {
        Debug.Log("ContinueGuide");
        switch (guideProgress)
        {
            case 1:
                Debug.Log("case 1");
                break;
            default:
                Debug.Log("ERROR: invalid guideProgress");
                break;
        }
    }
}
