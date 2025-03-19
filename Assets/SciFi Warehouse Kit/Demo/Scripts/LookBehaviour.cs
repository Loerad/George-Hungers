using UnityEngine;
using UnityEngine.InputSystem;

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
        float lookX = lookVector.x * lookSensitivityX * Time.deltaTime;
        float lookY = lookVector.y * lookSensitivityY * Time.deltaTime;
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * lookX);
    }

    public void OnLook (InputAction.CallbackContext context)
    {
        lookVector = context.ReadValue<Vector2>();
    }
}
