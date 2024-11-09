using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioButton _audioButton;

    private bool isActive = false;

    private void Start()
    {
        _audioButton?.UpdateStatus(isActive);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void ToggleActive()
    {
        _audioButton?.ChangeState();
    }
}
