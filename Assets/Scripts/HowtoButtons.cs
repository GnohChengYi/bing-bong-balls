using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowtoButtons : MonoBehaviour
{   
    [SerializeField]
    private GameObject previous;
    
    [SerializeField]
    private GameObject next;
    
    private static int page = 0;

    [SerializeField]
    private GameObject[] pages = new GameObject[5];

    public void PreviousButton()
    {
        pages[page].SetActive(false);
        if (page == 4) next.SetActive(true);
        page--;
        pages[page].SetActive(true);
        if (page == 0) previous.SetActive(false);
    }

    public void NextButton()
    {
        pages[page].SetActive(false);
        if (page == 0) previous.SetActive(true);
        page++;
        pages[page].SetActive(true);
        if (page == 4) next.SetActive(false);
    }
}
