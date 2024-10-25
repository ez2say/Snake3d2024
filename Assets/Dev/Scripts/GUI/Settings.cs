using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private Toggle _soundToggle;
    [SerializeField] private AudioSource _backgroundMusic;

    private void Start()
    {
        _soundToggle.isOn = _backgroundMusic.isPlaying;

        _soundToggle.onValueChanged.AddListener(OnSoundToggleChanged);
    }

    private void OnSoundToggleChanged(bool isOn)
    {
        if (isOn)
        {
            _backgroundMusic.Play();
        }
        else
        {
            _backgroundMusic.Stop();
        }
    }
}