using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class SettingsMenu : MonoBehaviour
{
    public InputActionAsset input;
    public AudioMixer audioMix;
    Resolution[] resolutions;
    public Dropdown resDropdown;

    public Dropdown DevDropdown;


    private void Start()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++) 
        { 
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resDropdown.AddOptions(options);

        resDropdown.value= currentResolutionIndex;
        resDropdown.RefreshShownValue();


        DevDropdown.ClearOptions();

        List<string> devices = new List<string>();
        Debug.Log(input);
        Debug.Log(input.devices);
        foreach (InputControlScheme scheme in input.controlSchemes)
        foreach (InputDevice device in InputSystem.devices)
        {
            if (scheme.SupportsDevice(device))
            {
                devices.Add(device.description.deviceClass);
            }
        }
        DevDropdown.AddOptions(devices);

        //int 
    }

    public void SetScheme(int schemeIndex)
    {

    }

    public void SetRes(int resolutionIndex)
    {
        Resolution resolution= resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume(float volume)
    {
        audioMix.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
