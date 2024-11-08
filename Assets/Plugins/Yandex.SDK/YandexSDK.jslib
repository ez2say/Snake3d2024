  mergeInto(LibraryManager.library, {


    ShowFullscreenAdvExternal: function () {
      ysdk.adv.showFullscreenAdv({
        callbacks: {
          onOpen: () => {
            console.log("Show Banner!");
          },
          onClose: function(wasShown) {
            gameInstance.SendMessage('YandexAdsProvider', 'OnCloseBannerExternal');
          },
          onError: function(error) {
          // some action on error
            gameInstance.SendMessage('YandexAdsProvider', 'OnCloseBannerExternal');
          }
        }
      })
    },

    // RateGameExtern: function () {
    //   ysdk.feedback.canReview()
    //   .then(({ value, reason }) => {
    //     if (value) {
    //       ysdk.feedback.requestReview()
    //       .then(({ feedbackSent }) => {
    //         console.log(feedbackSent);
    //       })
    //     } else {
    //       console.log(reason)
    //     }
    //   })
    // },

    ShowRewardedVideoExternal: function(){
      ysdk.adv.showRewardedVideo({
        callbacks: {
          onOpen: () => {
            console.log("Video show");
          },
          onRewarded: () => {
            gameInstance.SendMessage('YandexAdsProvider', 'OnRewardedVideoExternal');
          },
          onClose: () => {
            gameInstance.SendMessage('YandexAdsProvider', 'OnCloseRewardedVideoExternal');
            console.log('Video ad closed.');
          }, 
          onError: (e) => {
            gameInstance.SendMessage('YandexAdsProvider', 'OnErrorCloseRewardedVideoExternal');  
            console.log('Error while open video ad:', e);
          }
        }
      })
    },

    GameReadyExtern: function () {
      ysdk.features.LoadingAPI.ready()
    },

  });