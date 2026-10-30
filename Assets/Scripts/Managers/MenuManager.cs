using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(100)]
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    public GameObject gameOverText;
    public GameObject logo;
    public GameObject startButton;
    public GameObject quitButton;

    private void Awake() {
        Instance = this;

        gameOverText.SetActive(false);
        if (PlayerMovement.Instance == null) {
            logo.SetActive(true);
            startButton.SetActive(true);
            quitButton.SetActive(true);
        } else {
            logo.SetActive(false);
            startButton.SetActive(false);
            quitButton.SetActive(false);
        }
    }

    public void EnableGameOverMenu() {
        gameOverText.SetActive(true);
        startButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Main Game", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
