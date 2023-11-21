using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour, IInteractable
{
    public float drop = 2f;
    public PlayerController controller;

    [Header("Display Text")]
    public GameObject displayTxt;
    private bool PlayerInDistance;

    private bool hold;

    private void Start()
    {
        hold = false;
        PlayerInDistance = false;
        displayTxt.SetActive(false);
    }
    public void Commence(float amount)
    {
        drop -= amount;
        if (drop <= 0f)
        {
            Go();
        }
    }

    void Go()
    {
        Destroy(gameObject);
    }

   private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Touching");
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TouchingPickup(this);
            displayTxt.SetActive(true);
            PlayerInDistance = true;
            hold = true;
        }
        else if (collision.gameObject.CompareTag("Player") && hold == true)
        {
            collision.gameObject.GetComponent<PlayerController>().TouchingPickup(this);
            displayTxt.SetActive(false);
            PlayerInDistance=false;
        }
        
    }
   
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Not Touching");
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TouchingPickup(null);
            displayTxt.SetActive(false);
            PlayerInDistance = false;
            hold = false;
        }
    }
    public void Activate()
    {
        
    }
    public void Increase()
    {

    }
    public void Decrease() { }

 
}
