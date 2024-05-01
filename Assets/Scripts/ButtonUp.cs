using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUp : MonoBehaviour, IInteractable
{
    public Transform line;
    public InputMaster controls;
    public bool PlayerInZone;
    public GameObject txtToDisplay;

    public float widthChange = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInZone = false;
        txtToDisplay.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().Touching(this);
            PlayerInZone = true;
            txtToDisplay.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().Touching(this);
            PlayerInZone = false;
            txtToDisplay.SetActive(false);
        }
    }
    public void Increase()
    {
        if (PlayerInZone)
        {
            
            Vector3 currentScale = line.localScale;
            currentScale.y += widthChange;
            line.localScale = currentScale;
            

            if (currentScale.y < 0)
            {
                currentScale.x = 0;
                line.localScale = currentScale;
            }
        }

    }

    public void Activate()
    {

    }

    public void Decrease()
    { }
}

