using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OctaveMenu : MonoBehaviour
{
    private void PlayNoteOnClick(char c) {
        char[] cArray =  Audio.lastSelectedNote.ToCharArray();
        cArray[cArray.Length - 1] = c;
        Audio.lastSelectedNote = new string(cArray);
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioClip audioClip = Audio.GetClip(Audio.lastSelectedInstrument, Audio.lastSelectedNote);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void On2Click()
    {
        PlayNoteOnClick('2');
    }

    public void On3Click()
    {
        PlayNoteOnClick('3');
    }

    public void On4Click()
    {
        PlayNoteOnClick('4');
    }

    public void On5Click()
    {
        PlayNoteOnClick('5');
    }

    public void On6Click()
    {
        PlayNoteOnClick('6');
    }

     public void OnSharpClick()
    {
        char[] cArray =  Audio.lastSelectedNote.ToCharArray();
        if (cArray.Length == 2)
        {
            char [] charArray = { cArray[0], '-', cArray[1] };
            Audio.lastSelectedNote = new string(charArray);
            GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Buttons/Sharp");
            PlayNoteOnClick(Audio.lastSelectedNote[Audio.lastSelectedNote.Length - 1]);
        }
        else
        {
            char [] charArray2 = { cArray[0], cArray[2] };
            Audio.lastSelectedNote = new string(charArray2);
            GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Buttons/Natural");
            PlayNoteOnClick(Audio.lastSelectedNote[Audio.lastSelectedNote.Length - 1]);
        }
    }
}
