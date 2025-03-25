using UnityEngine;
using UnityEngine.InputSystem;
//Written originally by Rohan Anakin
/// <summary>
/// This class handles turning off and on the Flashlight
/// </summary>
public class FlashlightBehaviour : MonoBehaviour
{
    private bool lightOn = true;
    private new Light light;

    void Start()
    {
        light = GetComponentInChildren<Light>();
    }
    
    /// <summary>
    /// Do not call this method though classes. This is handled though a unity event
    /// </summary>
    /// <param name="context"></param>
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            lightOn = !lightOn;
            light.enabled = lightOn;
        }
    }
}
