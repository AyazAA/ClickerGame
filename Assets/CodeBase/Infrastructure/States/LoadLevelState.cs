using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Logic;
using CodeBase.Logic.Ads;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Logic.Web;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticData;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _staticData = staticData;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            EnemySpawner spawner = InitSpawners();
            GameObject hud = InitHud(spawner);

            LeaderboardNetwork leaderboard = InitLeaderboardAPI();

            AdsManager adsManager = InitAdsManager();

            Timer timer = InitTimer(hud);

            InitGameControllers(adsManager, hud, spawner, leaderboard, timer);
        }

        private EnemySpawner InitSpawners() =>
            _gameFactory.CreateSpawner();

        private GameObject InitHud(EnemySpawner spawner) =>
            _gameFactory.CreateHud(spawner);

        private LeaderboardNetwork InitLeaderboardAPI() =>
            _gameFactory.CreateLeaderboardAPI();

        private AdsManager InitAdsManager() =>
            _gameFactory.CreateAdsManager();

        private Timer InitTimer(GameObject hud)
        {
            TimerUI timerUI = hud.GetComponentInChildren<TimerUI>();
            return _gameFactory.CreateTimer(timerUI);
        }

        private static void InitGameControllers(AdsManager adsManager, GameObject hud, EnemySpawner spawner,
            LeaderboardNetwork leaderboardNetwork, Timer timer)
        {
            WinWindowUIController winWindow = hud.GetComponentInChildren<WinWindowUIController>();
            LoseWindowUIController loseWindow = hud.GetComponentInChildren<LoseWindowUIController>();
            StartGameButton startGameButton = hud.GetComponentInChildren<StartGameButton>();
            GameStarter gameStarter = new GameStarter(timer, spawner, winWindow, loseWindow, startGameButton);
            GameOver gameOver = new GameOver(timer, spawner, leaderboardNetwork, winWindow, loseWindow);
            AdsHandler adsHandler = new AdsHandler(adsManager, timer, spawner, loseWindow);
        }
    }
}