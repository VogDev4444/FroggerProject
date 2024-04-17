using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private static readonly string settings = "settings";
    private static readonly string songPref = "songPref";
    private static readonly string sfxPref = "sfxPref";
    private int settingsInt;

    public Slider songVol;
    public Slider sfxVol;

    private float songFloat, sfxFloat;

    public AudioSource mainAudio;
    public AudioSource[] sfxAudio;

    float newButtonPress = 0.0f; //to stop from immediately pressing start upon menu appearing

    private void Awake()
    {
        settingsInt = PlayerPrefs.GetInt(settings);
        if (settingsInt == 0)
        {
            songFloat = .80f;
            sfxFloat = .80f;
            songVol.value = songFloat;
            sfxVol.value = sfxFloat;
            PlayerPrefs.SetFloat(songPref, songFloat);
            PlayerPrefs.SetFloat(sfxPref, sfxFloat);
            PlayerPrefs.SetInt(settings, -1);
        }
        else
        {
            songFloat = PlayerPrefs.GetFloat(songPref);
            songVol.value = songFloat;
            sfxFloat = PlayerPrefs.GetFloat(sfxPref);
            sfxVol.value = sfxFloat;
        }

        newButtonPress = Time.time;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(songPref, songVol.value);
        PlayerPrefs.SetFloat(sfxPref, sfxVol.value);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveSettings();
        }
    }

    public void UpdateSound()
    {
        mainAudio.volume = songVol.value;
    }

    public void UpdateSFX()
    {
        for (int i = 0; i < sfxAudio.Length; i++)
        {
            sfxAudio[i].volume = sfxVol.value;
        }
        sfxAudio[3].Play();
    }

    public void Back()
    {
        if (Time.time > newButtonPress + 0.6f)
        {
            SceneManager.LoadScene("StartMenu");
        }
    }
}