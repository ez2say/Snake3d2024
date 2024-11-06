using Root.SDK.Interfaces;
using System.Runtime.InteropServices;

namespace Root.SDK
{
    public class WebGLYandexAdsStrategy : IYandexAdsStrategy
    {
        [DllImport("__Internal")]
        private static extern void ShowRewardedVideoExternal();

        [DllImport("__Internal")]
        private static extern void ShowFullscreenAdvExternal();

        public void ShowBanner()
        {
            ShowFullscreenAdvExternal();
        }

        public void ShowRewardedVideo()
        {
            ShowRewardedVideoExternal();
        }
    }
}