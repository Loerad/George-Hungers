using UnityEngine;
//Written originally by Alex Reid
//Written originally by Alex Reid
/// <summary>
/// Handles activating puzzles on a timer
/// </summary>

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    [SerializeField] private Puzzle[] puzzles;

    private const float MAX_TIME = 30f; //Max time between activating puzzles
    private const float MIN_TIME = 10f; //Min time between activating puzzles
    private float currentTime = 0f; //Time since last puzzle activated
    private float timeToActivate = 0f; //Time to activate the next puzzle

    public bool InPuzzle = false;

    void Awake()
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
        timeToActivate = Random.Range(MIN_TIME,MAX_TIME);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeToActivate)
        {
            ActivatePuzzle();
        }
    }

    private void ActivatePuzzle()
    {
        bool activated = false;

        while (!activated && !AllPuzzlesActive())
        {
            int puzzleToActivate = Random.Range(0,puzzles.Length);

            if (!puzzles[puzzleToActivate].Active)
            {
                puzzles[puzzleToActivate].Active = true;
                Debug.Log($"Activated puzzle {puzzleToActivate}");
                activated = true;
            }
        }

        currentTime = 0f;
        timeToActivate = Random.Range(MIN_TIME,MAX_TIME);
    }

    private bool AllPuzzlesActive() //check to make sure at least one puzzle is available
    {
        for (int i = 0; i < puzzles.Length; i++)
        {
            if (!puzzles[i].Active)
            {
                return false;
            }
        }
        return true;
    }
}