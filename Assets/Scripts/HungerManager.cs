using System.Runtime.CompilerServices;
using UnityEngine;

public class HungerManager : MonoBehaviour
{
    public static HungerManager Instance;

    [SerializeField] private float maxHunger;
    [SerializeField] private float hungerRate;

    private float currentHunger;
    private float hungerPercent;

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

        //Debug.Log(HungerPercent());

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
