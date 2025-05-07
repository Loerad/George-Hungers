using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//Rewritten by Rohan Anakin
/// <summary>
/// Handles player movement and step sounds
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement"), Tooltip("Properties for movement")]
    [SerializeField]
    private CharacterController controller;
    private float speed = 5f;
    private float sprintSpeed = 3f;
    private const float GRAVITY = -9.81f;
    private Vector3 velocity;
    private Vector2 moveAmount;

    [Header("Sprint"), Tooltip("Properties for sprinting")]
    public float sprintValue;
    float sprintMax = 3.0f;
    bool isRechargingSprint;
    bool isSprinting;
    bool isSprintEmpty;
    float sprintTimeout = 2.0f;
    float sprintTimer;

    [Header("Audio"), Tooltip("Properties for footsteps")]
    
    [SerializeField]
    private GroundFlavour groundFlavour;
    private const int METAL_LAYER = 3;
    private AudioSource audioSource;
    
    [SerializeField]
    private AudioClip footStepSound;
    private float volume = 0.5f; //this is arbitrary and needs an accessor for a settings panel later
    private const float WALKING_DELAY = 0.42f;
    private const float SPRINTING_DELAY = 0.35f;
    private float footStepDelay = 0.42f;
    private float nextFootstep = 0;
    
    // add another list named whatever type of footstep here
    [SerializeField]
    private List<AudioClip> concreteFootsteps = new List<AudioClip>(); 
    
    [SerializeField]
    private List<AudioClip> metalFootsteps = new List<AudioClip>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sprintValue = sprintMax;
        sprintTimer = sprintTimeout;
    }

    void Update() //this code is from the demo from SciFi Warehouse
    {
        if (GameManager.Instance.gameState == GameState.Paused)
        {
            return;
        }
        if (PuzzleManager.Instance.InPuzzle){return;}

        Vector3 motion = transform.right * moveAmount.x + transform.forward * moveAmount.y;

        controller.Move(velocity * Time.deltaTime);
        velocity.y += GRAVITY * Time.deltaTime;
        if (isSprinting)
        {
            controller.Move(motion.normalized * (speed + sprintSpeed) * Time.deltaTime);
        }
        else
        {
            controller.Move(motion.normalized * speed * Time.deltaTime);
        }

        HandleStepSound();

        if(sprintValue <= 0)
        {
            isSprinting = false;
            isSprintEmpty = true;
        }

        if (!isSprinting)
        {
            sprintTimer -= Time.deltaTime;
            if(sprintTimer <= 0)
            {
                if (isSprintEmpty)
                {
                    isRechargingSprint = true;
                    RechargeSprint();
                }
                else if (sprintValue <= sprintMax)
                {
                    RechargeSprint();
                }
            }
            if (sprintValue >= sprintMax)
            {
                isRechargingSprint = false;
                isSprintEmpty = false;
            }
        }
        else
        {
            sprintTimer = sprintTimeout;
            sprintValue -= Time.deltaTime;
        }
    }

    void RechargeSprint()
    {
        sprintValue += Time.deltaTime * 1.75f;
    }

    /// <summary>
    /// Do not call this method though classes. This is handled though a unity event
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context) //unity event check the Player Input Component for context
    {
        moveAmount = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Do not call this method though classes. This is handled though a unity event
    /// </summary>
    /// <param name="context"></param>
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.gameState == GameState.Paused || PuzzleManager.Instance.InPuzzle)
        {
            return;
        }
        if (!isRechargingSprint)
        {
            if (context.phase == InputActionPhase.Started)
            {
                isSprinting = true;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                isSprinting = false;
            } 
        }

    }
    /// <summary>
    /// Handles step sounds while walking 
    /// </summary>
    private void HandleStepSound()
    {
        if (isSprinting)
        {
            footStepDelay = SPRINTING_DELAY;
        }
        else
        {
            footStepDelay = WALKING_DELAY;
        }

        if (controller.velocity.x !>= 0.2 || controller.velocity.z !>= 0.2 ) //step behaviour 
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0)
            {
                if (groundFlavour.CurrentFloor() == METAL_LAYER) //add a layer and a if else with the list for a new step type
                {
                    RandomizeStep(metalFootsteps);
                }
                else 
                {
                    RandomizeStep(concreteFootsteps);
                }
                audioSource.PlayOneShot(footStepSound, volume);
                nextFootstep += footStepDelay;
            }
        }
    }
    /// <summary>
    /// Generates a random step from the array of steps given
    /// </summary>
    /// <param name="steps"></param>
    private void RandomizeStep(List<AudioClip> steps)
    {
        int nextSound = Random.Range(0, steps.Count - 1);
        footStepSound = steps[nextSound];
    }
}


