using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public bool IsOn { get; private set; }

    [Header("Audio Sources")]

    [SerializeField] private AudioMixer _mixer;

    [SerializeField] private AudioSource _gameMusic;

    private float _gameMusicTime;

    public void Construct()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        SetVolume(true);
    }

    private void OnEnable()
    {
        if (_gameMusic != null && _gameMusicTime > 0f)
        {
            _gameMusic.time = _gameMusicTime;
            
            _gameMusic.Play();
        }
    }

    private void OnDisable()
    {
        if (_gameMusic != null && _gameMusic.isPlaying)
        {
            _gameMusicTime = _gameMusic.time;
        }
    }

    public void SetVolume(bool isOn)
    {
        float volume = isOn ? 0 : -80f;

        Debug.Log("Aduio Valume: " + volume);

        _mixer.SetFloat("Volume", volume);

        IsOn = isOn;
    }

    public void SaveGameMusicTime()
    {
        if (_gameMusic != null && _gameMusic.isPlaying)
        {
            _gameMusicTime = _gameMusic.time;
        }
    }
}