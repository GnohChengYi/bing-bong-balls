using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// TODO check guide quit guide again, verify guide progress correct
public class GuideController : MonoBehaviour
{
    private static readonly string GUIDE_KEY = "guide";

    [SerializeField]
    private GameObject guideTextObject;
    private Text guideTextComponent;

    private int guideProgress = 0;

    [SerializeField]
    private TextAsset guidesTextAsset;
    private string[] guideTexts;

    private void OnEnable()
    {
        if (guideTextComponent == null || guideTexts == null)
        {
            guideTextComponent = guideTextObject.GetComponent<Text>();
            guideTexts = guidesTextAsset.text.Split(
                new string[] { "\r\n\r\n\r\n" }, StringSplitOptions.None);
        }
        StartGuide();
    }

    private void StartGuide()
    {
        guideProgress = 0;
        guideTextComponent.text = guideTexts[guideProgress];
    }

    public void ContinueGuide()
    {
        guideProgress++;
        if (guideProgress < guideTexts.Length)
            guideTextComponent.text = guideTexts[guideProgress];
        else gameObject.SetActive(false);

        // TODO add effects to toolbar (or even simulation panel) accordingly
        /*
        switch (guideProgress)
        {
            case 1:
                Debug.Log("case 1");
                break;
            default:
                Debug.Log("ERROR: invalid guideProgress");
                break;
        }
        */
    }

    public static bool GetGuidePref()
    {
        return PlayerPrefs.GetInt(GUIDE_KEY, 1) == 1;
    }

    public static void SetGuidePref(bool guideOn)
    {
        int pref = guideOn ? 1 : 0;
        PlayerPrefs.SetInt(GUIDE_KEY, pref);
    }
}
