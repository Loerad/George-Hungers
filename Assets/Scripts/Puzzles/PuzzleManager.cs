using UnityEngine;


public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    [SerializeField] private GameObject[] puzzles;
    
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
}