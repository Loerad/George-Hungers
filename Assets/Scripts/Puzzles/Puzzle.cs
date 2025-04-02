using UnityEngine;

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
            }
            else
            {
                GetComponent<MeshRenderer>().material = materials[0]; //regular material
            }
        }
    }
}
