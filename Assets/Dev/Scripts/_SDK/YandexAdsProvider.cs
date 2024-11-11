using Root.SDK.Interfaces;
using System;
using UnityEngine;

namespace Root.SDK
{
    public class YandexAdsProvider : MonoBehaviour, IAdsProvider, IAdsEventer
    {
        public IAdsProvider AdsCallable => _instance;

        public IAdsEventer AdsEventer => _instance;

        private static YandexAdsProvider _instance;

        public event Action OnShowBanner;

        public event Action OnCloseBanner;

        public event Action OnShowRewardVideo;

        public event Action OnRewardedVideo;

        public event Action OnCloseRewardVideo;

        public event Action OnErrorCloseRewardVideo;

        private IYandexAdsStrategy _currentStrategy;

        public void Construct()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            { return; }


#if UNITY_EDITOR || UNITY_ANDROID
            _currentStrategy = new UnityYandexAdsStrategy(this);
#elif UNITY_WEBGL
            _currentStrategy = new WebGLYandexAdsStrategy();
#endif

            DontDestroyOnLoad(gameObject);
        }

        public void ShowBanner()
        {
            OnShowBanner?.Invoke();

            _currentStrategy.ShowBanner();
        }

        public void ShowRewardedVideo()
        {
            OnShowRewardVideo?.Invoke();

            _currentStrategy.ShowRewardedVideo();
        }

        #region Java Script Methods Handlers
        public void OnCloseBannerExternal()
        {
            OnCloseBanner?.Invoke();
        }

        public void OnCloseRewardedVideoExternal()
        {
            OnCloseRewardVideo?.Invoke();
        }

        public void OnRewardedVideoExternal()
        {

        }

        public void OnErrorCloseRewardedVideoExternal()
        {
            OnErrorCloseRewardVideo?.Invoke();
        }
        #endregion
    }
}