using Root.SDK;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Root.Core
{
    public class GameCore : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;

        [SerializeField] private YandexAdsProvider _yandexAdsProvider;

        public void Start()
        {
            _audioManager.Construct();

            _yandexAdsProvider.Construct();

            SceneManager.LoadScene((int)IDScene.GAMEPLAY);
        }
    }
}