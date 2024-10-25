using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    void Start()
    {
        settingsPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level"); 
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}