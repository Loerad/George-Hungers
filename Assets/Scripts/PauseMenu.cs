using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    InputAction pause;
    [SerializeField] private Canvas pauseUI;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pause = InputSystem.actions.FindAction("Pause");
    }

    public void OnPause(InputAction.CallbackContext context)
    {

        pauseUI.enabled = !pauseUI.enabled;
        if (pauseUI.enabled)
        {
            GameManager.Instance.gameState = GameState.Paused;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            GameManager.Instance.gameState = GameState.InGame;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ContinueGame()
    {
        GameManager.Instance.gameState = GameState.InGame;
        Cursor.lockState = CursorLockMode.Locked;
        pauseUI.enabled = false;
    }
}
