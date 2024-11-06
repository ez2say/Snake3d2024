using Root.SDK.Interfaces;
using UnityEngine;

namespace Root.SDK
{
    public class UnityYandexAdsStrategy : IYandexAdsStrategy
    {
        private readonly YandexAdsProvider _yandexAdsProvider;

        public UnityYandexAdsStrategy(YandexAdsProvider yandexAdsProvider)
        {
            _yandexAdsProvider = yandexAdsProvider;
        }

        public void ShowBanner()
        {
            Debug.Log("Show Banner!");

            _yandexAdsProvider.OnCloseBannerExternal();
        }

        public void ShowRewardedVideo()
        {
            Debug.Log("Show RewardedVideo!");

            _yandexAdsProvider.OnCloseRewardedVideoExternal();
        }
    }
}