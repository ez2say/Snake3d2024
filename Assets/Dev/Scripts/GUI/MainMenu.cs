using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioButton _audioButton;

    [SerializeField] private GameObject _view;

    private AudioManager _audioManager;

    private bool isActive = false;

    public void Construct()
    {
        _audioManager = AudioManager.Instance;

        _audioButton?.UpdateStatus(isActive);
    }
    public void Open()
    {
        _view?.SetActive(true);
    }

    public void Close()
    {
        _view?.SetActive(false);
    }

    public void ToggleActive()
    {
        _audioButton?.ChangeState();

        _audioManager.SetVolume(_audioButton.IsActive);
    }
}
