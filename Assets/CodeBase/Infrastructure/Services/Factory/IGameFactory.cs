using CodeBase.Logic;
using CodeBase.Logic.Ads;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Logic.Web;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        public GameObject CreateHud(EnemySpawner spawner);
        EnemySpawner CreateSpawner();
        GameObject CreateMonster(int currentLevel, Transform parent);
        LeaderboardNetwork CreateLeaderboardAPI();
        AdsManager CreateAdsManager();
        Timer CreateTimer(TimerUI timerUI);
    }
}
