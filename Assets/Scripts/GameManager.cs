
// Author: Lorna
/// <summary>
/// This script handles the game functions for the menus and game over
/// </summary>

using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState
{
    InGame,
    Paused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState gameState = GameState.InGame;

    private void Awake()
    {
        Application.targetFrameRate = 60;


        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Ship");
    }

    public void ExitGame()
    {
        Debug.Log("exit");
        Application.Quit();
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene("GameOver");
    }

    public void ReturnToMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene("Menu");
    }
}
