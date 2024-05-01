using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private bool leveltTrigger = false;
    [SerializeField] private bool leveltTrigger1 = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (leveltTrigger) 
            {
                SceneManager.LoadScene("Two Player Work 1");
            }
        }

        if (other.CompareTag("Player"))
        {
            if (leveltTrigger1)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
