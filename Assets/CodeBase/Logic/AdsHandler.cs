using CodeBase.Logic.Ads;
using CodeBase.Logic.EnemySpawners;
using CodeBase.UI;

namespace CodeBase.Logic
{
    public class AdsHandler
    {
        private readonly AdsManager _adsManager;
        private readonly Timer _timer;
        private readonly EnemySpawner _enemySpawner;
        private readonly LoseWindowUIController _loseWindow;

        public AdsHandler(AdsManager adsManager, Timer timer, EnemySpawner enemySpawner,
            LoseWindowUIController loseWindow)
        {
            _adsManager = adsManager;
            _timer = timer;
            _enemySpawner = enemySpawner;
            _loseWindow = loseWindow;
            Subscribe();
        }
        
        ~AdsHandler()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            _adsManager.AdWatched += OnAdWatched;
            _loseWindow.AdButtonClicked += OnAdButtonClicked;
        }

        private void UnSubscribe()
        {
            _adsManager.AdWatched -= OnAdWatched;
            _loseWindow.AdButtonClicked -= OnAdButtonClicked;
        }

        private void OnAdWatched()
        {
            _loseWindow.Hide();
            _enemySpawner.ClickDetectorOn();
            _timer.AddTime();
        }

        private void OnAdButtonClicked() => 
            _adsManager.ShowAd();
    }
}