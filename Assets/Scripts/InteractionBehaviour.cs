using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
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
    private int garbageCount;
    protected int GarbageCount 
    { 
        set 
        {
            garbageCount = value;
            garbageCountText.text = garbageCount.ToString();
        }
        get 
        {
            return garbageCount;
        }
    }
    [SerializeField]
    private float garbageValue = 5;
    [SerializeField]
    private TMP_Text garbageCountText;
    [SerializeField]
    private Image crosshair;
    private RaycastHit crosshairCheck;

    void Start()
    {
        layerMask = LayerMask.GetMask("Default", "Garbage", "Puzzle", "George");
        GarbageCount = 0;
        audioSource = GetComponentInParent<AudioSource>();
    }

    void Update()
    {
        nextInteract -= Time.deltaTime;    
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, out crosshairCheck, 3.5f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.forward * crosshairCheck.distance, Color.yellow);

            GameObject g = crosshairCheck.collider.gameObject;

            if (g.CompareTag("George") || g.CompareTag("Puzzle") || g.CompareTag("Garbage"))
            {
                crosshair.color = Color.green;
            }
        }
        else 
        {
            crosshair.color = Color.red;
        }
}
    /// <summary>
    /// Do not call this method through classes. This is handled though a unity event
    /// </summary>
    /// <param name="context"></param>
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (PuzzleManager.Instance.InPuzzle || GameManager.Instance.gameState == GameState.Paused){return;}
        if (context.phase == InputActionPhase.Started)
        {
            if (nextInteract <= 0)
            {
                audioSource.PlayOneShot(interactSound, 1f);
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 3.5f, layerMask))
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
        if (GarbageCount > 0)
        {
            if (HungerManager.Instance.CurrentHunger >= HungerManager.Instance.MaxHunger)
            {
                HungerManager.Instance.CurrentHunger = HungerManager.Instance.MaxHunger;
            }
            else
            {
                HungerManager.Instance.CurrentHunger += garbageValue;
            }
            GarbageCount--;
        }
    }
    /// <summary>
    /// Handles opening a puzzle to fix a system
    /// </summary>
    public void PuzzleInteract(GameObject hitObject)
    {
        //handle puzzle
        if (hitObject.GetComponent<Puzzle>().Active)
        {
            hitObject.GetComponent<Puzzle>().OnInteract();
        }
    }
    /// <summary>
    /// Handles picking up garbage to be added to the player
    /// </summary>
    public void GarbageInteract(GameObject hitObject)
    {
       GarbageCount++;
       Destroy(hitObject);
    }
}
