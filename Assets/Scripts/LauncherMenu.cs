using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherMenu : MonoBehaviour
{
    public void OnPianoClick()
    {
        Audio.lastSelectedInstrument = "Piano";
    }

    public void OnGuitarClick()
    {
        Audio.lastSelectedInstrument = "Acoustic Guitar";
    }
}
