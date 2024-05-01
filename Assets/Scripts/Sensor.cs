using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private Animator myDoor2 = null;

    //[SerializeField] private bool OpenTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    [SerializeField] private string doorClose = "LabDoorClose";
    [SerializeField] private string closeDoor = "LabCloseDoor";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (closeTrigger)
            {
                myDoor.Play(doorClose, 0, 0.0f);
                gameObject.SetActive(false);
            }
        }
        if (other.CompareTag("Player 2"))
        {
            if (closeTrigger)
            {
                myDoor2.Play(closeDoor, 0, 0.0f);
                gameObject.SetActive(false);
            }
        }
    }
}
