using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.Rendering;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    private InputMaster controls;
    private float moveSpeed = 6f;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private Vector2 move;
    private float jumpHeight = 2.4f;
    private CharacterController controller;
    public Transform ground;
    public float distanceToGround = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    [Header("Button")]
    public GameObject lightorobj;
    public GameObject txtToDisplay;
    
    public Animator animator;
    public Animator animation2;

    [Header("Pick Up")]
    
    public Transform PlayerTransform;
    public GameObject Sphere;
    public bool nearGun = false;
    private bool holdGun = false;


    private IInteractable interactable;
    private Target target;
    private Transform pickUp;

    

    private void Start()
    {
        interactable = null;
        target = null;

        txtToDisplay.SetActive(false);
        Sphere.GetComponent<Rigidbody>().isKinematic = true;
    }


    private void Awake()
    {
        controls = new InputMaster();
        controller= GetComponent<CharacterController>();
    }

    void Update()
    {
        Grav();
        PlayerMovement();
        Jump();
        Interactions();
        Pickup();
    }

    public void Grav()
    {
        isGrounded = Physics.CheckSphere(ground.position, distanceToGround, groundMask);

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void PlayerMovement()
    {
        move = controls.Player.Movement.ReadValue<Vector2>();

        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
        controller.Move(movement * moveSpeed * Time.deltaTime);
        
    }

    public void Jump()
    {
        if(controls.Player.Jump.triggered && isGrounded) 
        { 
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void Touching(IInteractable i)
    {
        interactable = i;
    }

    public void Interactions()
    {
        
        if (controls.Player.Interaction.triggered)
        {

            if (interactable != null)
            {
                interactable.Activate();
                interactable.Increase();
                interactable.Decrease();
            }
        }

    }

    public void TouchingPickup(Target t)
    {
        target = t;
    }

    public void Pickup()
    {
        
        if (controls.Player.Pickup.triggered && !holdGun && target)
        {
            holdGun = true;
            Shoot();
        }
        else if (controls.Player.Pickup.triggered && holdGun && target)
        {
            holdGun = false;
            UnequipObject();
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();   
    }

    void UnequipObject()
    {
        //PlayerTransform.DetachChildren();
        target.gameObject.transform.eulerAngles = new Vector3(0,0,0);
        target.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        target.gameObject.transform.SetParent(null);
    }

    void Shoot()
    {
        EquipObject();
    }

    void EquipObject()
    {
        target.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        target.gameObject.transform.position = PlayerTransform.transform.position;
        target.gameObject.transform.rotation = PlayerTransform.transform.rotation;
        target.gameObject.transform.SetParent(PlayerTransform);
    }
}
