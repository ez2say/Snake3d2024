using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioButton _audioButton;

    [SerializeField] private GameObject _view;

    private AudioManager _audioManager;

    public void Construct()
    {
        _audioManager = AudioManager.Instance;
    }

    public void Open()
    {
        _view?.SetActive(true);
    }

    public void Close()
    {
        _view?.SetActive(false);
    }
}
