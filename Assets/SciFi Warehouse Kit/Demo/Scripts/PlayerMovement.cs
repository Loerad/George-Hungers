using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement"), Tooltip("Properties for movement")]
    
    public CharacterController controller;
    private float speed = 5f;
    private const float sprintValue = 3f;
    private const float gravity = -9.81f;
    Vector3 velocity;
    Vector2 moveAmount;

    [Header("GroundCheck"), Tooltip("Properties for ground check, may be removed")]
    
    public Transform groundCheck;
    private float groundDistance = 0.45f;
    public LayerMask groundMask;
    bool isGrounded;

    [Header("Audio"), Tooltip("Properties for footsteps")]
    
    [SerializeField]
    private GroundFlavour groundFlavour;
    private AudioSource audioSource;
    
    [SerializeField]
    private AudioClip footStepSound;
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
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);    
    }

    void Update()
    {
        Vector3 motion = transform.right * moveAmount.x + transform.forward * moveAmount.y;
        controller.Move(motion.normalized * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (moveAmount.x != 0 || moveAmount.y != 0 )
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0)
            {
                if (groundFlavour.CurrentFloor() == 3) //metal layer
                {
                    RandomizeStep(metalFootsteps);
                }
                else 
                {
                    RandomizeStep(concreteFootsteps);
                }
                audioSource.PlayOneShot(footStepSound, 0.5f);
                nextFootstep += footStepDelay;
            }
        }
    }

    private void RandomizeStep(List<AudioClip> steps)
    {
        int nextSound = Random.Range(0, steps.Count - 1);
        footStepSound = steps[nextSound];
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveAmount = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            speed += sprintValue;
            footStepDelay = 0.35f;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            speed -= sprintValue;
            footStepDelay = 0.42f;
        }
        
    }
}


