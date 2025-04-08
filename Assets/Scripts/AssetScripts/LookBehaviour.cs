using UnityEngine;
using UnityEngine.InputSystem;
//Rewritten by Rohan Anakin
/// <summary>
/// Handles moving the camera when the player moves their look vector controller
/// </summary>
public class LookBehaviour : MonoBehaviour
{
    public float lookSensitivityX = 100f;
    public float lookSensitivityY = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    Vector2 lookVector;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState == GameState.Paused)
        {
            Cursor.lockState = CursorLockMode.Confined;
            return;
        }
        float lookX = lookVector.x * lookSensitivityX * Time.deltaTime;
        float lookY = lookVector.y * lookSensitivityY * Time.deltaTime;
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * lookX);
    }
    /// <summary>
    /// Do not call this method though classes. This is handled though a unity event
    /// </summary>
    /// <param name="context"></param>
    public void OnLook (InputAction.CallbackContext context)
    {
        lookVector = context.ReadValue<Vector2>();
    }
}
