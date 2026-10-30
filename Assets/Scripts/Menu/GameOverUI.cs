using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject GameOverMenu;
    private void OnEnable()
    {
        CharacterStat.OnPlayerDeath += EnableGameOverMenu;
    }
    private void OnDisable()
    {
        CharacterStat.OnPlayerDeath -= EnableGameOverMenu;
    }
    public void EnableGameOverMenu()
    {
        if (GameOverMenu != null)
        {
            GameOverMenu.SetActive(true);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}


