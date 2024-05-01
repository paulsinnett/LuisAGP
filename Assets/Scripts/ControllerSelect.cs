using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSelect : MonoBehaviour
{
    public int player;
    public SettingsMenu menu;

    public void Select(int index)
    {
        menu.SetScheme(index, player);
    }
}
