using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    private bool isActive = false;

    void Start()
    {
        settingsPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level"); 
    }

    public void ToggleActive()
    {
        isActive = !isActive;
        settingsPanel.SetActive(isActive);
    }

    public void OpenSettings()
    {
        ToggleActive();
    }
}