using UnityEngine;
using UnityEngine.InputSystem;
//Written originally by Alex Reid
/// <summary>
/// This class handles turning off and on the map
/// </summary>

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject map;

    /// <summary>
    /// Do not call this method though classes. This is handled though a unity event
    /// </summary>
    /// <param name="context"></param>
    public void OnMapToggle(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            map.SetActive(!map.activeSelf);
        }
    }
}

