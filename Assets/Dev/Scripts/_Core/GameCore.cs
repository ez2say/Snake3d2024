using Root.SDK;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Root.Core
{
    public class GameCore : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;

        [SerializeField] private YandexAdsProvider _yandexAdsProvider;

        public static bool IsLoad = false;

        public void Start()
        {
            if (IsLoad) return;

            _audioManager.Construct();

            _yandexAdsProvider.Construct();

            DontDestroyOnLoad(gameObject);

            IsLoad = true;  

            SceneManager.LoadScene((int)IDScene.GAMEPLAY);
        }
    }
}