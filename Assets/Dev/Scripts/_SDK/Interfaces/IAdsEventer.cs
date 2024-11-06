using System;

namespace Root.SDK
{
    public interface IAdsEventer
    {
        event Action OnShowBanner;

        event Action OnCloseBanner;

        event Action OnShowRewardVideo;

        event Action OnCloseRewardVideo;

        event Action OnErrorCloseRewardVideo;
    }
}