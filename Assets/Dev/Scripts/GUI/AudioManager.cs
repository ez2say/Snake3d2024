using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]

    [SerializeField] private List<AudioSource> _audioSources;

    [SerializeField] private AudioSource _gameMusic;

    private float _gameMusicTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
        float volume = isOn ? 1f : 0f;
        foreach (var audioSource in _audioSources)
        {
            audioSource.volume = volume;
        }
        if (_gameMusic != null)
        {
            _gameMusic.volume = volume;
        }
    }

    public void SaveGameMusicTime()
    {
        if (_gameMusic != null && _gameMusic.isPlaying)
        {
            _gameMusicTime = _gameMusic.time;
        }
    }
}