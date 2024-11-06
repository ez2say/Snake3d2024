using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio Settings")]

    [SerializeField] private Toggle _soundToggle;

    private void Start()
    {
        _soundToggle.isOn = true;

        _soundToggle.onValueChanged.AddListener(OnSoundToggleChanged);

        OnSoundToggleChanged(_soundToggle.isOn);
    }

    private void OnSoundToggleChanged(bool isOn)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetVolume(isOn);
        }
    }
}