using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace CodeBase.Logic.Ads
{
    public class AdsManager : MonoBehaviour
    {
        public event Action AdWatched;
        private RewardedAd _rewardedAd;
        private readonly string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
        
        private void Start()
        {
            MobileAds.Initialize(initStatus => {});

            InitRewardedAd();
        }

        public void ShowAd()
        {
            if(_rewardedAd.IsLoaded())
                _rewardedAd.Show();
        }

        private void AdLoaded(object sender, EventArgs e) => 
            Debug.Log("Ad loaded");

        private void AdFailedToLoad(object sender, AdFailedToLoadEventArgs e) => 
            Debug.Log("Ad Failed To Load");

        private void UserEarnedReward(object sender, Reward e) => 
            AdWatched?.Invoke();

        private void AdClosed(object sender, EventArgs e) => 
            InitRewardedAd();

        private void InitRewardedAd()
        {
            _rewardedAd = new RewardedAd(_adUnitId);

            _rewardedAd.OnAdLoaded += AdLoaded;
            _rewardedAd.OnAdFailedToLoad += AdFailedToLoad;
            _rewardedAd.OnUserEarnedReward += UserEarnedReward;
            _rewardedAd.OnAdClosed += AdClosed;

            AdRequest request = new AdRequest.Builder().Build();

            _rewardedAd.LoadAd(request);
        }
    }
}