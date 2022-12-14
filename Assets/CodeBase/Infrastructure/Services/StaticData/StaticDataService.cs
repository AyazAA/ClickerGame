using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private MonsterStaticData _monsterData;
        private GameStaticData _gameData;

        public void Load()
        {
            _monsterData = Resources.Load<MonsterStaticData>("StaticData/Monsters/Enemy");
            _gameData = Resources.Load<GameStaticData>("StaticData/GameData");
        }

        public MonsterStaticData ForMonster() => 
            _monsterData != null 
                ? _monsterData 
                : null;
        
        public GameStaticData ForGame() => 
            _gameData != null 
                ? _gameData 
                : null;
    }
}