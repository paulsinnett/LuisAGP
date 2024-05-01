using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSelect : MonoBehaviour
{
    public int player;

    public SelectController controller;

    public void Select(int index)
    {
        controller.SetScheme(index, player);
    }
}
