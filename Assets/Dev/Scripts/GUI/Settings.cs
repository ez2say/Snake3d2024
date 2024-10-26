using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio Settings")]

    [SerializeField] private Toggle _soundToggle;

    [SerializeField] private List<AudioSource> _audioSources;

    private void Start()
    {
        _soundToggle.isOn = _audioSources.Exists(audioSource => audioSource.isPlaying);

        _soundToggle.onValueChanged.AddListener(OnSoundToggleChanged);
        
        OnSoundToggleChanged(_soundToggle.isOn);
    }

    private void OnSoundToggleChanged(bool isOn)
    {
        foreach (var audioSource in _audioSources)
        {
            audioSource.volume = isOn ? 1f : 0f;
        }
    }
}