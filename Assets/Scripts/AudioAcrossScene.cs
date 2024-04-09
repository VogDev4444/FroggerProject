using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAcrossScene : MonoBehaviour
{
    private static readonly string songPref = "songPref";
    private static readonly string sfxPref = "sfxPref";

    private float songFloat, sfxFloat;

    public AudioSource mainAudio;
    public AudioSource[] sfxAudio;

    void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        songFloat = PlayerPrefs.GetFloat(songPref, .80f);
        sfxFloat = PlayerPrefs.GetFloat(sfxPref, .80f);

        mainAudio.volume = songFloat;
        for (int i = 0; i < sfxAudio.Length; i++)
        {
            sfxAudio[i].volume = sfxFloat;
        }
    }
}
