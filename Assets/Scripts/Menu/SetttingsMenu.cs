using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SetttingsMenu : MonoBehaviour
{
    public AudioMixer audioSoundsMixer;
    public GameObject background;
    Image backgroundImage;
    public AudioMixer audioMusicMixer;
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    private const string RESOLUTION_PREF = "resolution";
    private const string BRIGHTNESS_PREF = "brightness-volume";

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        backgroundImage = background.GetComponent<Image>();
        SetBrightness(PlayerPrefs.GetFloat("brightness-volume"));
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i< resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt(RESOLUTION_PREF, currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();
    }
    public void SetMusicVolume(float volume)
    {
        audioMusicMixer.SetFloat("volume", volume);
    }
    public void SetSoundsVolume(float volume)
    {
        audioSoundsMixer.SetFloat("volume", volume);
    }
    public void SetBrightness(float brightness)
    {
        brightness = brightness * 0.5f;
        backgroundImage.color = new Color(brightness,brightness,brightness, 100);
    }
    public void SetSensivity(float sensivity)
    {
        
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void SetResoulution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height, Screen.fullScreen);
    }
}
