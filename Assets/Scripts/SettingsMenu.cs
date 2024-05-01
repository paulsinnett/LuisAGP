using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
//using UnityEditor.PackageManager;
using UnityEngine.InputSystem.LowLevel;
using UnityEditor.ShaderGraph;

public class SettingsMenu : MonoBehaviour
{
    public InputActionAsset input;

    public AudioMixer audioMix;

    //Resolution Settings
    Resolution[] resolutions;
    public Dropdown resDropdown;

    //Device Settiings
    public Dropdown [] DevDropdown;
    static List<InputDevice> devices = new List<InputDevice>();

    static public InputDevice [] inputDevices = new InputDevice[2];

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]


    static void SetupControllers()
    {
        Debug.Log("Setup controllers...");
        inputDevices[0] = Keyboard.current;
        Debug.Log($"Input device 0 = {inputDevices[0]}");
        inputDevices[1] = Gamepad.current;
        Debug.Log($"Input device 1 = {inputDevices[1]}");
    }


    private void Start()
    {
        //Resolution
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

        //Devices
        foreach (Dropdown dropdown in DevDropdown)
        {
            dropdown.ClearOptions();
        }

        List<string> deviceNames = new List<string>();
        Debug.Log(input);
        foreach (InputControlScheme scheme in input.controlSchemes)
        {
            Debug.Log(scheme.name);
            foreach (InputDevice device in InputSystem.devices)
            {
                if (scheme.SupportsDevice(device))
                {
                    if (deviceNames.Contains(device.displayName))
                    {
                        deviceNames.Add(device.displayName + " 2");
                    }
                    else
                    {
                        deviceNames.Add(device.displayName);
                    }
                    devices.Add(device);
                }
            }
        }
        foreach (Dropdown dropdown in DevDropdown)
        {
            dropdown.AddOptions(deviceNames);
            dropdown.value = devices.IndexOf(inputDevices[0]);
            dropdown.RefreshShownValue();
        }

        //int 
    }

    public void SetScheme(int schemeIndex, int player)
    {
        InputDevice device = devices[schemeIndex];
        Debug.Log($"Set current device {device} to player {player}");
        inputDevices[player] = device;
        //inputDevices[1] = null;
        //foreach (InputDevice d in devices)
        //{
        //    if (d != inputDevices[0])
        //    {
        //        inputDevices[1] = d;
        //        break;
        //    }
        //}
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
