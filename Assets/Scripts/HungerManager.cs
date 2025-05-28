using UnityEngine;
using UnityEngine.UIElements;

// Author: Lorna
/// <summary>
/// This script manages the hunger bar
/// </summary>
public class HungerManager : MonoBehaviour
{
    public static HungerManager Instance;

    private readonly float maxHunger = 100;
    public float MaxHunger {get {return maxHunger;} }
    [SerializeField] private float hungerRate;

    private float currentHunger;
    public float CurrentHunger
    {
        get{return currentHunger;}
        set{currentHunger = value;}
    }
    private float hungerPercent;

    private VisualElement hungerBar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        VisualElement document = GetComponent<UIDocument>().rootVisualElement;
        hungerBar = document.Q<VisualElement>("Hungerbar");
    }

    void Start()
    {
        CurrentHunger = MaxHunger;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState == GameState.Paused)
        {
            return;
        }
        
        CurrentHunger -= hungerRate * Time.deltaTime;
        CurrentHunger = Mathf.Clamp(CurrentHunger, 0f, maxHunger);

        hungerBar.style.width = Length.Percent(HungerPercent() * 100);

        //TODO: if hunger reaches 0 call game over

        if (currentHunger <= 0)
        {
            GameManager.Instance.gameState = GameState.GameOver;
            GameManager.Instance.GameOver();
        }
    }

    public float HungerPercent()
    {            
        return CurrentHunger / MaxHunger;
    }
}
