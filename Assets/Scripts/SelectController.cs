using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class SelectController : MonoBehaviour
{

    public InputActionAsset input;
    //public GameObject ControllerSelectionPannel;

    //Device Setup
    public Dropdown[] DevDropdown;
    static List<InputDevice> devices = new List<InputDevice>();

    static public InputDevice[] inputDevices = new InputDevice[2];

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]

    static void SetupControllers()
    {
        Debug.Log("Setup controllers...");
        inputDevices[0] = Keyboard.current;
        Debug.Log($"Input device 0 = {inputDevices[0]}");
        inputDevices[1] = Gamepad.current;
        Debug.Log($"Input device 1 = {inputDevices[1]}");
    }



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;

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
}
