using UnityEngine;

public class GroundFlavour : MonoBehaviour
{
    int layer;
    void OnTriggerEnter(Collider other)
    {
        layer = other.gameObject.layer;     
    }

    public int CurrentFloor()
    {
        return layer;
    }
}
