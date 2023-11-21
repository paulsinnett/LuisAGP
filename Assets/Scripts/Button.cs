using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;


public class Button : MonoBehaviour, IInteractable
{
    public GameObject txtToDisplay;
    private bool PlayerInZone;
    public GameObject lightorobj;
    public InputMaster controls;

    private void Start()
    {
        PlayerInZone = false;
        txtToDisplay.SetActive(false);
    }
    public void Activate()
    {
        if (PlayerInZone)
        {
            lightorobj.SetActive(!lightorobj.activeSelf);
            gameObject.GetComponent<Animator>().Play("switch");
            gameObject.GetComponent<Animator>().Play("Potentiometer");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().Touching(this);
            txtToDisplay.SetActive(true);
            PlayerInZone= true;
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
    }
    public void Increase()
    { }
    public void Decrease()
    {
        
    }
}
