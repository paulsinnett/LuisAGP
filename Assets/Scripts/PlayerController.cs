using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;

//using UnityEditor.Experimental.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
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

    public int playerNumber;
    PlayerInput playerInput;
    public MouseLook lookScript;

    private Vector2 look;

    public Vector3 Player;
    public Vector3 objectToSwap;

    /*[Header("Audio")]
    public AudioSource src;
    public AudioClip sfx;*/

    [Header("PauseMenu")]
    public bool pause = false;
    public GameObject pauseMenuPannel;

    [Header("CharacterAnim")]
    public GameObject character2;

    [Header("Crossheir")]
    public Transform crosshair;
    public LayerMask interactableLayer;
    public Material selectedMaterial;
    public Material noneSelectedMaterial;

    [Header("Abilities")]
    public Camera playerCamera;

    
    private GameObject lastInteractedObject;


    private void Start()
    {

        character2.GetComponent<Animator>();
        interactable = null;
        target = null;

        txtToDisplay.SetActive(false);
        Sphere.GetComponent<Rigidbody>().isKinematic = true;

        playerInput = GetComponent<PlayerInput>();
        playerInput.user.UnpairDevices();
        List<InputDevice> bound = new List<InputDevice>();
        InputUser user = InputUser.PerformPairingWithDevice(SettingsMenu.inputDevices[playerNumber], playerInput.user);
        bound.Add(SettingsMenu.inputDevices[playerNumber]);
        if (SettingsMenu.inputDevices[playerNumber] == Keyboard.current)
        {
            user = InputUser.PerformPairingWithDevice(Mouse.current, playerInput.user);
            bound.Add(Mouse.current);
        }
        playerInput.SwitchCurrentControlScheme(bound.ToArray());
        Debug.Log($"{user} = {playerInput.user} {user == playerInput.user}");
    }


    private void Awake()
    {
        controls = new InputMaster();
        controller= GetComponent<CharacterController>();
    }

    void Update()
    {
        Player = transform.position;
        Grav();
        PlayerMovement();
        Interactions();
        Pickup();
        Look();
        Pausing();
        SwapPositionWithInteractable();
        CheckInteractable();
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

    public void OnMovement(InputValue value)
    {
        move = value.Get<Vector2>();
        if (controls.Player.Movement.triggered) 
        {
            //src.clip = sfx;
            //src.Play();
            //src.Stop();
        }
        //Debug.Log(src.time);
        
    }

    public void OnLook(InputValue value)
    {
        //Debug.Log($"look = {value.Get<Vector2>()}");
        look = value.Get<Vector2>();
    }

    public void Look()
    {
        lookScript.Look(look);
    }

    public void PlayerMovement()
    {
        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
        controller.Move(movement * moveSpeed * Time.deltaTime);
        
        if (controls.Player.Movement.triggered) 
        {
            character2.GetComponent<Animator>().SetBool("IsMoving", true);        
        }
        else 
        {
            character2.GetComponent<Animator>().SetBool("IsMoving", false);
        }
        
    }

    public void OnJump()
    {
        if(isGrounded) 
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

    public void Pausing()
    {
        if (controls.Player.PauseMenu.triggered)
        {
            Time.timeScale = 0f;
            pauseMenuPannel.SetActive(true);
            pause = true;
        }
        
    }

    private void CheckInteractable()
    {
        Ray ray = playerCamera.ScreenPointToRay(crosshair.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                if (lastInteractedObject != null && lastInteractedObject != hit.collider.gameObject)
                {
                    lastInteractedObject.GetComponent<Renderer>().material = noneSelectedMaterial;
                }
                lastInteractedObject = hit.collider.gameObject;
                hit.collider.GetComponent<Renderer>().material = selectedMaterial;

            }
        }
        else
        {
            if (lastInteractedObject != null)
            {
                lastInteractedObject.GetComponent<Renderer>().material = noneSelectedMaterial;
                lastInteractedObject = null;
            }
        }
    }

    private void SwapPositionWithInteractable()
    {
        Ray ray = playerCamera.ScreenPointToRay(crosshair.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
        {
            
            Debug.Log("Hit");
            if (hit.collider.CompareTag("Interactable"))
            {
                Vector3 tempPosition = transform.position;
                if (controls.Player.SwapPosition.triggered)
                {
                    
                    transform.position = hit.collider.transform.position;
                    hit.collider.transform.position = tempPosition;
                }
            }

                
        }
        
    }
}
