using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;

    [SerializeField] private GameObject _settingsPanel;
    
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        TimeStop();

        pauseMenuPanel.SetActive(true);

        isPaused = true;
    }

    private void TimeStop()
    {
        Time.timeScale = 0f;
    }

    private void TimeResume()
    {
        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        TimeResume();

        pauseMenuPanel.SetActive(false);

        isPaused = false;
    }

    public void RestartGame()
    {
        TimeResume();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenSettings()
    {
       _settingsPanel.SetActive(!_settingsPanel.activeSelf);
    }

    public void BackToMainMenu()
    {
        TimeResume();

        SceneManager.LoadScene("MainMenu");
    }
}