using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InteractionBehaviour : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip interactSound;
    private readonly float interactDelay = 0.1f;
    private float nextInteract = 0;
    private LayerMask layerMask = LayerMask.GetMask("Default", "Garbage", "Puzzle", "George");

    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    void Update()
    {
        nextInteract -= Time.deltaTime;    
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (nextInteract <= 0)
            {
                audioSource.PlayOneShot(interactSound, 1f);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
                {
                    Debug.Log("hit");
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                    if (hit.collider.gameObject.CompareTag("George"))
                    {
                        GeorgeInteract();
                    }
                    else if (hit.collider.gameObject.CompareTag("Puzzle"))
                    {
                        PuzzleInteract();
                    }
                    else if (hit.collider.gameObject.CompareTag("Garbage"))
                    {
                        GarbageInteract();
                    }
                }
                else
                {
                    Debug.Log("Not hit");
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red); 
                }
                nextInteract = interactDelay;
            }
        }
    }

    public void GeorgeInteract()
    {
        //handle george
    }

    public void PuzzleInteract()
    {
        //handle puzzle
    }

    public void GarbageInteract()
    {
        //handle garbage
    }
}
