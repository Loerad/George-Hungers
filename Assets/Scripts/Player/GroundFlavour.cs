using UnityEngine;
//Written originally by Rohan Anakin
/// <summary>
/// Checks the collider the trigger attached to this script enters 
/// </summary>
public class GroundFlavour : MonoBehaviour
{
    int layer;
    void OnTriggerEnter(Collider other) //this should be changed to work with tags
    {
        layer = other.gameObject.layer;     
    }

    public int CurrentFloor()
    {
        return layer;
    }
}
