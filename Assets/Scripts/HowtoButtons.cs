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
    
    private static int page;

    [SerializeField]
    private GameObject[] pages = new GameObject[5];
    
    void Start()
    {
        page = 1;
        pages[0].SetActive(true);
        pages[1].SetActive(false);
        pages[2].SetActive(false);
        pages[3].SetActive(false);
        pages[4].SetActive(false);
        previous.SetActive(false);
        next.SetActive(true);
    }

    public void PreviousButton()
    {
        pages[page].SetActive(false);
        page--;
        pages[page].SetActive(true);
        if (page == 1) previous.SetActive(false);
        if (page == 4) next.SetActive(true);
    }

    public void NextButton()
    {
        pages[page].SetActive(false);
        page++;
        pages[page].SetActive(true);
        if (page == 5) next.SetActive(false);
        if (page == 2) previous.SetActive(true);
    }
}
