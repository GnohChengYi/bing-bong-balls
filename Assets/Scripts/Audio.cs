using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio
{
    // TODO make sure complete and filenames match these
    public static string[]
        instruments =
            new string[] {
                "Piano",
                "Guitar",
                "Strings",
                "Synth"
            };

    private static string[]
        notes =
            new string[] {
                "a-2",
                "a-3",
                "a-4",
                "a-5",
                "a-6",
                "a2",
                "a3",
                "a4",
                "a5",
                "a6",
                "b2",
                "b3",
                "b4",
                "b5",
                "b6",
                "c-2",
                "c-3",
                "c-4",
                "c-5",
                "c-6",
                "c2",
                "c3",
                "c4",
                "c5",
                "c6",
                "d-2",
                "d-3",
                "d-4",
                "d-5",
                "d-6",
                "d2",
                "d3",
                "d4",
                "d5",
                "d6",
                "e2",
                "e3",
                "e4",
                "e5",
                "e6",
                "f-2",
                "f-3",
                "f-4",
                "f-5",
                "f-6",
                "f2",
                "f3",
                "f4",
                "f5",
                "f6",
                "g-2",
                "g-3",
                "g-4",
                "g-5",
                "g-6",
                "g2",
                "g3",
                "g4",
                "g5",
                "g6"
            };

    public static string lastSelectedInstrument = "Piano";

    public static string lastSelectedNote = "a4";

    // instrument -> note -> clip
    private static Dictionary<string, Dictionary<string, AudioClip>>
        instrumentToNotes;

    public static void InitializeClips()
    {
        instrumentToNotes =
            new Dictionary<string, Dictionary<string, AudioClip>>();
        foreach (string instrument in instruments)
        {
            Dictionary<string, AudioClip> noteToClip =
                new Dictionary<string, AudioClip>();
            foreach (string note in notes)
            {
                string path = instrument + "/" + note;
                AudioClip clip = Resources.Load<AudioClip>(path);
                noteToClip.Add(note, clip);
            }
            instrumentToNotes.Add(instrument, noteToClip);
        }
    }

    public static AudioClip GetClip(string instrument, string note)
    {
        string message = "GetClip " + instrument + " " + note;
        return instrumentToNotes[instrument][note];
    }

    public static AudioClip
    ListToClip(
        List<float> audioDataList,
        int channels = 2,
        int frequency = 44100
    )
    {
        float[] audioData = audioDataList.ToArray();
        int lengthSamples = audioData.Length / channels;
        AudioClip audioClip = AudioClip.Create(
            "AudioClip", lengthSamples, channels, frequency, false);
        audioClip.SetData(audioData, 0);
        return audioClip;
    }
}
