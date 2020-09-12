using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;



public class SaveSettings : MonoBehaviour
{


    [SerializeField] public Slider volumeSliderSound;
    [SerializeField] public Slider volumeSliderMusic;
    [SerializeField] public Toggle enableFullscreen;
    [SerializeField] public TMP_Dropdown resolutionDropdown;
    [SerializeField] public TMP_Dropdown graphicsDropdown;

    #region Player Pref Key Constants

    private const string MUSIC_VOLUME_PREF = "music-volume";
    private const string SFX_VOLUME_PREF = "sound-volume";

    private const string FULLSCREEN_PREF = "fullscreen";
    private const string RESOLUTION_PREF = "resolution";
    private const string GRAPHICS_PREF = "graphics";

    #endregion

    #region Monobehaviour API

    private void Awake()
    {
        resolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(RESOLUTION_PREF, resolutionDropdown.value);
            PlayerPrefs.Save();
        }));
        graphicsDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(GRAPHICS_PREF, graphicsDropdown.value);
            PlayerPrefs.Save();
        }));
    }

    void Start()
    {
        volumeSliderMusic.value = PlayerPrefs.GetFloat(MUSIC_VOLUME_PREF, 1);
        volumeSliderSound.value = PlayerPrefs.GetFloat(SFX_VOLUME_PREF, 1);

        enableFullscreen.isOn = GetBoolPref(FULLSCREEN_PREF);
        graphicsDropdown.value = PlayerPrefs.GetInt(GRAPHICS_PREF, 3);

    }

    #endregion

    #region Volume

    public void OnChangeSoundVolume(Single value)
    {
        SetPref(SFX_VOLUME_PREF, value);
    }

    public void OnChangeMusicVolume(Single value)
    {
        SetPref(MUSIC_VOLUME_PREF, value);
    }


    #endregion


    #region ScreenSettings

    public void OnToggleFullscreen(bool state)
    {
        SetPref(FULLSCREEN_PREF, state);
    }
    public void OnChangeResolutionScreen(int value)
    {
        SetPref(RESOLUTION_PREF, value);
    }

    public void OnChangeGrahicsScreen(int value)
    {
        SetPref(GRAPHICS_PREF, value);
    }


    #endregion

    #region Pref Setters

    private void SetPref(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    private void SetPref(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    private void SetPref(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    private void SetPref(string key, bool value)
    {
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }

    private bool GetBoolPref(string key, bool defaultValue = true)
    {
        return Convert.ToBoolean(PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue)));
    }

    #endregion
}