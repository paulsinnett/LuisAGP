using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : MonoBehaviour, IInteractable
{
    public GameObject txtToDisplay;
    public bool PlayerInZone;
    public GameObject door;
    public InputMaster controls;
    public bool doorOpen = false;
    public GameObject door2;

    [Header("Audio")]
    public AudioSource src;
    public AudioSource src2;
    public AudioClip sfx,sfx2;


    private void Start()
    {
        
        PlayerInZone = false;
        txtToDisplay.SetActive(false);
    }
    public void Activate()
    {
        if (PlayerInZone)
        {
            door.GetComponent<Animator>().Play("DoorOpen");
            door2.GetComponent<Animator>().Play("DoorOpen");
            src.clip= sfx;
            src.Play();
            src.clip = sfx2;
            src.Play();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().Touching(this);
            txtToDisplay.SetActive(true);
            PlayerInZone = true;
        }
        if (other.gameObject.tag == "Player 2")
        {
            other.GetComponent<PlayerController1>().Touching(this);
            txtToDisplay.SetActive(true);
            PlayerInZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().Touching(null);
            PlayerInZone = false;
            txtToDisplay.SetActive(false);
        }
        if (other.gameObject.tag == "Player 2")
        {
            other.GetComponent<PlayerController1>().Touching(null);
            PlayerInZone = false;
            txtToDisplay.SetActive(false);
        }
    }
    public void Increase()
    { }
    public void Decrease() { }
}
