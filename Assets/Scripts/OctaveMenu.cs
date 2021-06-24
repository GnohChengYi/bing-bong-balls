using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OctaveMenu : MonoBehaviour
{
    public void On2Click()
    {
        // char[] cArray =  Audio.lastSelectedNote.ToCharArray();
        // cArray[cArray.Length - 1] = '2';
        // Audio.lastSelectedNote = new string(cArray);
        // AudioSource audioSource = GetComponent<AudioSource>();
        // AudioClip audioClip = Audio.GetClip("Piano", Audio.lastSelectedNote);
        // audioSource.clip = audioClip;
        // audioSource.Play();
    }

    public void On3Click()
    {
        char[] cArray =  Audio.lastSelectedNote.ToCharArray();
        cArray[cArray.Length - 1] = '3';
        Audio.lastSelectedNote = new string(cArray);
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioClip audioClip = Audio.GetClip("Piano", Audio.lastSelectedNote);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void On4Click()
    {
        char[] cArray =  Audio.lastSelectedNote.ToCharArray();
        cArray[cArray.Length - 1] = '4';
        Audio.lastSelectedNote = new string(cArray);
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioClip audioClip = Audio.GetClip("Piano", Audio.lastSelectedNote);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void On5Click()
    {
        char[] cArray =  Audio.lastSelectedNote.ToCharArray();
        cArray[cArray.Length - 1] = '5';
        Audio.lastSelectedNote = new string(cArray);
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioClip audioClip = Audio.GetClip("Piano", Audio.lastSelectedNote);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void On6Click()
    {
        // char[] cArray =  Audio.lastSelectedNote.ToCharArray();
        // cArray[cArray.Length - 1] = '6';
        // Audio.lastSelectedNote = new string(cArray);
        // AudioSource audioSource = GetComponent<AudioSource>();
        // AudioClip audioClip = Audio.GetClip("Piano", Audio.lastSelectedNote);
        // audioSource.clip = audioClip;
        // audioSource.Play();
    }

     public void OnSharpClick()
    {
        char[] cArray =  Audio.lastSelectedNote.ToCharArray();
        if (cArray.Length == 2)
        {
            char [] charArray = { cArray[0], '-', cArray[1] };
            Audio.lastSelectedNote = new string(charArray);
            GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Buttons/Sharp");
            AudioSource audioSource = GetComponent<AudioSource>();
            AudioClip audioClip = Audio.GetClip("Piano", Audio.lastSelectedNote);
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            char [] charArray2 = { cArray[0], cArray[2] };
            Audio.lastSelectedNote = new string(charArray2);
            GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Buttons/Natural");
            AudioSource audioSource = GetComponent<AudioSource>();
            AudioClip audioClip = Audio.GetClip("Piano", Audio.lastSelectedNote);
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}
