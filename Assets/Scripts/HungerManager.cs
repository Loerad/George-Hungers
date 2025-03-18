using UnityEngine;

public class HungerManager : MonoBehaviour
{
    public static HungerManager Instance;

    [SerializeField] private float maxHunger = 100f;
    [SerializeField] private float hungerRate = 1f;

    private float currentHunger;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentHunger = maxHunger;
    }

    // Update is called once per frame
    void Update()
    {
        currentHunger -= hungerRate * Time.deltaTime;
        
        Debug.Log(currentHunger);

        //TODO: if hunger reaches 0 call game over
    }
}
