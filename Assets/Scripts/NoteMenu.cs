using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteMenu : MonoBehaviour
{
    [SerializeField]
    private Sprite note2;
    [SerializeField]
    private Sprite note3;
    [SerializeField]
    private Sprite note4;
    [SerializeField]
    private Sprite note5;
    [SerializeField]
    private Sprite note6;
    [SerializeField]
    private Button b2;
    [SerializeField]
    private Button b3;
    [SerializeField]
    private Button b4;
    [SerializeField]
    private Button b5;
    [SerializeField]
    private Button b6;

    public void OnCClick()
    {
        b2.image.overrideSprite = note2;
        b3.image.overrideSprite = note3;
        b4.image.overrideSprite = note4;
        b5.image.overrideSprite = note5;
        b6.image.overrideSprite = note6;
        Audio.lastSelectedNote = "c3";
    }

    public void OnDClick()
    {
        b2.image.overrideSprite = note2;
        b3.image.overrideSprite = note3;
        b4.image.overrideSprite = note4;
        b5.image.overrideSprite = note5;
        b6.image.overrideSprite = note6;
        Audio.lastSelectedNote = "d3";
    }

    public void OnEClick()
    {
        b2.image.overrideSprite = note2;
        b3.image.overrideSprite = note3;
        b4.image.overrideSprite = note4;
        b5.image.overrideSprite = note5;
        b6.image.overrideSprite = note6;
        Audio.lastSelectedNote = "e3";
    }

    public void OnFClick()
    {
        b2.image.overrideSprite = note2;
        b3.image.overrideSprite = note3;
        b4.image.overrideSprite = note4;
        b5.image.overrideSprite = note5;
        b6.image.overrideSprite = note6;
        Audio.lastSelectedNote = "f3";
    }

    public void OnGClick()
    {
        b2.image.overrideSprite = note2;
        b3.image.overrideSprite = note3;
        b4.image.overrideSprite = note4;
        b5.image.overrideSprite = note5;
        b6.image.overrideSprite = note6;
        Audio.lastSelectedNote = "g3";
    }

    public void OnAClick()
    {
        b2.image.overrideSprite = note2;
        b3.image.overrideSprite = note3;
        b4.image.overrideSprite = note4;
        b5.image.overrideSprite = note5;
        b6.image.overrideSprite = note6;
        Audio.lastSelectedNote = "a3";
    }

    public void OnBClick()
    {
        b2.image.overrideSprite = note2;
        b3.image.overrideSprite = note3;
        b4.image.overrideSprite = note4;
        b5.image.overrideSprite = note5;
        b6.image.overrideSprite = note6;
        Audio.lastSelectedNote = "b3";
    }
}
