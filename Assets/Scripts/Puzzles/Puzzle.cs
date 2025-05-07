using UnityEngine;
using UnityEngine.UI;
//Written originally by Alex Reid
/// <summary>
/// Handles setting a puzzle's material when activated or deactivated.
/// Will also contain functionality for opening puzzle menu when interacted with.
/// </summary>

public class Puzzle : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    [SerializeField] private GameObject puzzleCanvas;
    [SerializeField] private Button fixButton;
    [SerializeField] private Toggle[] toggles;

    private bool active;
    public bool Active
    {
        get{return active;}
        set
        {
            active = value;

            if (active)
            {
                GetComponent<MeshRenderer>().material = materials[1]; //active material
                GetComponentInChildren<SpriteRenderer>().color = Color.red;
                SetToggles();
            }
            else
            {
                GetComponent<MeshRenderer>().material = materials[0]; //regular material
                GetComponentInChildren<SpriteRenderer>().color = Color.green;
            }
        }
    }

    void Update()
    {
        bool completed = true;
        foreach (Toggle t in toggles)
        {
            if(!t.isOn){ completed = false;}
        }
        fixButton.interactable = completed;
    }

    public void OnInteract()
    {
        puzzleCanvas.SetActive(true);
        PuzzleManager.Instance.InPuzzle = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void CompletePuzzle()
    {
        puzzleCanvas.SetActive(false);
        PuzzleManager.Instance.InPuzzle = false;
        Cursor.lockState = CursorLockMode.Locked;
        Active = false;
    }

    public void close()
    {
        puzzleCanvas.SetActive(false);
        PuzzleManager.Instance.InPuzzle = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SetToggles()
    {
        fixButton.interactable = false;
        foreach(Toggle t in toggles)
        {
            t.isOn = Random.value < 0.25f;
        }
    }
}
