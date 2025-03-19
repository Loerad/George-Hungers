using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement"), Tooltip("Properties for movement")]
    public CharacterController controller;
    public const float speed = 8f;
    public const float gravity = -9.81f;
    Vector3 velocity;
    Vector2 moveAmount;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public AudioClip footStepSound;
    public float footStepDelay;
    private float nextFootstep = 0;

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

        if (moveAmount.x > 0 || moveAmount.y > 0 )
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0)
            {
                GetComponent<AudioSource>().PlayOneShot(footStepSound, 0.7f);
                nextFootstep += footStepDelay;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveAmount = context.ReadValue<Vector2>();
    }
}


