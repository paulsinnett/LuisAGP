using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    public Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}
