using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
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
    [SerializeField]
    private int garbageCount;
    [SerializeField]
    private float garbageValue = 5;
    [SerializeField]
    private TMP_Text garbageCountText;

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
    /// Do not call this method through classes. This is handled though a unity event
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
                if (Physics.Raycast(transform.position, transform.forward, out hit, 3.5f, layerMask))
                {
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
        if (garbageCount > 0)
        {
            if (HungerManager.Instance.CurrentHunger >= HungerManager.Instance.MaxHunger)
            {
                HungerManager.Instance.CurrentHunger = HungerManager.Instance.MaxHunger;
            }
            else
            {
                HungerManager.Instance.CurrentHunger += garbageValue;
            }
            garbageCount--;
        }
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
       garbageCount++;
       Destroy(hitObject);
    }
}
