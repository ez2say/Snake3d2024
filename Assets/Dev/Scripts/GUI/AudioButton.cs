using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] private Sprite _turnOn;

    [SerializeField] private Sprite _turnOff;
    
    private AudioManager _audioManager; 

    private Image _render;

    private void Awake()
    {
        _audioManager = AudioManager.Instance;

        _render = GetComponent<Image>();

        UpdateStatus();
    }

    public void ChangeState()
    {
        _audioManager.SetVolume(!_audioManager.IsOn);

        UpdateStatus();
    }

    public void UpdateStatus()
    {
        if (_audioManager.IsOn)
            _render.sprite = _turnOn;
        else
            _render.sprite = _turnOff;
    }
}