using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

// Author: Lorna
/// <summary>
/// This script manages the hunger bar
/// </summary>
public class HungerManager : MonoBehaviour
{
    public static HungerManager Instance;

    [SerializeField] private float maxHunger;
    [SerializeField] private float hungerRate;

    private float currentHunger;
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
        currentHunger = maxHunger;
    }

    // Update is called once per frame
    void Update()
    {
        currentHunger -= hungerRate * Time.deltaTime;
        currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger);

        hungerBar.style.width = Length.Percent(HungerPercent() * 100);

        //TODO: if hunger reaches 0 call game over
    }

    public float HungerPercent()
    {
        if (maxHunger <= 0)
        {
            return 0f;
        }
            
        return currentHunger / maxHunger;
    }
}
