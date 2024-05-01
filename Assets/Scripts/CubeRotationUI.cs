using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeRotationUI : MonoBehaviour
{
    private InputMaster control;

    public Animator animator;
    private int rotationIndex = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    private void Awake()
    {
        control = new InputMaster();
    }

    private void Update()
    {
        Rotation();
    }

    public void Rotation()
    {
        if (control.CubeMenu.LeftRotation.triggered)
        {
            rotationIndex--;
            if (rotationIndex < 1) 
            {
                rotationIndex = 4; 
            }
            animator.SetInteger("RotationIndex", rotationIndex);

        }
        else if (control.CubeMenu.RightRotation.triggered)
        {

            rotationIndex++;
            if (rotationIndex > 4)
            {
                rotationIndex = 1;
            }
            animator.SetInteger("RotationIndex", rotationIndex);
        }
    }

    private void OnEnable()
    {
        control.Enable();
    }

    private void OnDisable()
    {
        control.Disable();
    }
}
