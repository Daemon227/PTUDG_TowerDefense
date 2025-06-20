
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGameManager : MonoBehaviour
{
    public Button pauseButton;
    public GameObject pausePanel;
    public Button playButton;
    public Button replayButton;
    public Button backHomeButton;

    private void Start()
    {
        pauseButton.onClick.AddListener(OpenPausePanel);
        playButton.onClick.AddListener(PlayGame);
        replayButton.onClick.AddListener(ReplayGame);
        backHomeButton.onClick.AddListener(BackToHome);
    }
    public void OpenPausePanel()
    {
        pausePanel.SetActive(true);
        CombatManager.Instance.IsPauseGame = true;
    }

    public void PlayGame()
    {
        pausePanel.SetActive(false);
        CombatManager.Instance.IsPauseGame = false;
    }

    public void ReplayGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    public void BackToHome()
    {
        SceneManager.LoadScene(0);
    }
    
}
