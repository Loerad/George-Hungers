using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement"), Tooltip("Properties for movement")]
    [SerializeField]
    private CharacterController controller;
    private float speed = 5f;
    private const float SPRINT_VALUE = 3f;
    private const float GRAVITY = -9.81f;
    private Vector3 velocity;
    private Vector2 moveAmount;

    [Header("GroundCheck"), Tooltip("Properties for ground check")]
    [SerializeField]
    private Transform groundCheck;
    private const float GROUND_DISTANCE = 0.45f;
    [SerializeField]
    private LayerMask groundMask;
    bool isGrounded;

    [Header("Audio"), Tooltip("Properties for footsteps")]
    
    [SerializeField]
    private GroundFlavour groundFlavour;
    private const int METAL_LAYER = 3;
    private AudioSource audioSource;
    
    [SerializeField]
    private AudioClip footStepSound;
    private const float WALKING_DELAY = 0.42f;
    private const float SPRINTING_DELAY = 0.35f;
    private float footStepDelay = 0.42f;
    private float nextFootstep = 0;
    
    [SerializeField]
    private List<AudioClip> concreteFootsteps = new List<AudioClip>();
    
    [SerializeField]
    private List<AudioClip> metalFootsteps = new List<AudioClip>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, GROUND_DISTANCE, groundMask);    
    }

    void Update()
    {
        Vector3 motion = transform.right * moveAmount.x + transform.forward * moveAmount.y;
        controller.Move(motion.normalized * speed * Time.deltaTime);

        velocity.y += GRAVITY * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        HandleStepSound();
    }

    public void OnMove(InputAction.CallbackContext context) //unity event check the Player Input Component for context
    {
        moveAmount = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            speed += SPRINT_VALUE;
            footStepDelay = SPRINTING_DELAY;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            speed -= SPRINT_VALUE;
            footStepDelay = WALKING_DELAY;
        }
        
    }

    private void HandleStepSound()
    {
        if (moveAmount.x != 0 || moveAmount.y != 0 ) //step behaviour 
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0)
            {
                if (groundFlavour.CurrentFloor() == METAL_LAYER)
                {
                    RandomizeStep(metalFootsteps);
                }
                else 
                {
                    RandomizeStep(concreteFootsteps);
                }
                audioSource.PlayOneShot(footStepSound, 0.5f); //arbitrary volume 
                nextFootstep += footStepDelay;
            }
        }
    }

    private void RandomizeStep(List<AudioClip> steps)
    {
        int nextSound = Random.Range(0, steps.Count - 1);
        footStepSound = steps[nextSound];
    }
}


