using UnityEngine;
using UnityEngine.InputSystem;
//Written originally by Rohan Anakin
/// <summary>
/// Handles the interaction between the different objects
/// </summary>
public class InteractionBehaviour : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip interactSound;
    private readonly float interactDelay = 0.1f;
    private float nextInteract = 0;
    private LayerMask layerMask;

    void Start()
    {
        layerMask = LayerMask.GetMask("Default", "Garbage", "Puzzle", "George");

        audioSource = GetComponentInParent<AudioSource>();
    }

    void Update()
    {
        nextInteract -= Time.deltaTime;    
    }
    /// <summary>
    /// Do not call this method though classes. This is handled though a unity event
    /// </summary>
    /// <param name="context"></param>
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (nextInteract <= 0)
            {
                audioSource.PlayOneShot(interactSound, 1f);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
                {
                    Debug.Log("hit");
                    Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);

                    GameObject g = hit.collider.gameObject;

                    if (g.CompareTag("George"))
                    {
                        GeorgeInteract(g);
                    }
                    else if (g.CompareTag("Puzzle"))
                    {
                        PuzzleInteract(g);
                    }
                    else if (g.CompareTag("Garbage"))
                    {
                        GarbageInteract(g);
                    }
                }
                nextInteract = interactDelay;
            }
        }
    }
    /// <summary>
    /// Handles putting garbage collected into george
    /// </summary>
    public void GeorgeInteract(GameObject hitObject)
    {
        //handle george
    }
    /// <summary>
    /// Handles opening a puzzle to fix a system
    /// </summary>
    public void PuzzleInteract(GameObject hitObject)
    {
        //handle puzzle
    }
    /// <summary>
    /// Handles picking up garbage to be added to the player
    /// </summary>
    public void GarbageInteract(GameObject hitObject)
    {
        //handle garbage
    }
}
