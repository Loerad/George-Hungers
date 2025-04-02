using UnityEngine;
//Written originally by Alex Reid
/// <summary>
/// Handles setting a puzzle's material when activated or deactivated.
/// Will also contain functionality for opening puzzle menu when interacted with.
/// </summary>

public class Puzzle : MonoBehaviour
{
    [SerializeField] private Material[] materials;
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
            }
            else
            {
                GetComponent<MeshRenderer>().material = materials[0]; //regular material
                GetComponentInChildren<SpriteRenderer>().color = Color.green;
            }
        }
    }
}
