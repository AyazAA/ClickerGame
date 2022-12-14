using CodeBase.Enemy;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Logic;
using CodeBase.Logic.Ads;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Logic.Web;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAsset _asset;
        private readonly IStaticDataService _staticDataService;

        public GameFactory(IAsset asset, IStaticDataService staticDataService)
        {
            _asset = asset;
            _staticDataService = staticDataService;
        }

        public AdsManager CreateAdsManager() =>
            _asset.Instantiate(AssetPath.AdsPath).GetComponent<AdsManager>();

        public LeaderboardNetwork CreateLeaderboardAPI() =>
            _asset.Instantiate(AssetPath.LeaderboardPath).GetComponent<LeaderboardNetwork>();

        public Timer CreateTimer(TimerUI timerUI)
        {
            float startTime = _staticDataService.ForGame().StartTime;
            float rewardTime = _staticDataService.ForGame().RewardTime;
            Timer timer = _asset.Instantiate(AssetPath.TimerPath).GetComponent<Timer>();
            timer.Construct(timerUI, startTime, rewardTime);
            return timer;
        }

        public GameObject CreateHud(EnemySpawner spawner)
        {
            GameObject hud = _asset.Instantiate(AssetPath.HudPath);
            hud.GetComponentInChildren<CurrentLevelUI>().Construct(spawner);
            hud.GetComponentInChildren<RemainingLevelsUI>().Construct(spawner);
            return hud;
        }

        public EnemySpawner CreateSpawner()
        {
            EnemySpawner spawner = _asset.Instantiate(AssetPath.Spawner).GetComponent<EnemySpawner>();
            int levelsCount = _staticDataService.ForGame().LevelsCount;
            spawner.Construct(this, levelsCount);
            return spawner;
        }

        public GameObject CreateMonster(int currentLevel, Transform parent)
        {
            MonsterStaticData monsterData = _staticDataService.ForMonster();
            GameObject monster =
                GameObject.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);

            EnemyHealth health = monster.GetComponent<EnemyHealth>();
            health.Current = monsterData.StartHp + monsterData.DeltaNextLevelHp * (currentLevel - 1);
            health.Max = health.Current;

            ClickDetector clickDetector = monster.GetComponent<ClickDetector>();
            clickDetector.Construct(monsterData.ReceivedDamage);
            monster.SetActive(true);

            monster.GetComponent<ActorUI>().Construct(health);

            return monster;
        }
    }
}