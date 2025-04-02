using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    private void Awake()
    {
        Application.targetFrameRate = 60;

        Instance = this;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Ship");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
