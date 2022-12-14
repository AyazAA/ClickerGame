using System;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services.Factory;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
    public class EnemySpawner : MonoBehaviour
    {
        public event Action LevelChanged; 
        public event Action AllMonstersDied; 
        private IGameFactory _factory;
        private ClickDetector _clickDetector;
        private int _levelsCount;
        private int _currentLevel = 1;
        private bool _monstersDied = false;

        public int LevelCount => _levelsCount; 
        public int CurrentLevel => _currentLevel; 

        public void Construct(IGameFactory factory, int levelsCount)
        {
            _factory = factory;
            _levelsCount = levelsCount;
        }

        public void Spawn()
        {
            GameObject monster = _factory.CreateMonster(_currentLevel, this.transform);
            EnemyDeath _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += EnemyDead;
            _clickDetector = monster.GetComponent<ClickDetector>();
        }

        public void ClickDetectorOff() => 
            _clickDetector.CanTakeDamage = false;
        
        public void ClickDetectorOn() => 
            _clickDetector.CanTakeDamage = true;

        private void EnemyDead()
        {
            if (_currentLevel >= _levelsCount)
            {
                AllMonstersDied?.Invoke();
                _monstersDied = true;
                return;
            }
            
            _currentLevel++;
            LevelChanged?.Invoke();
            Spawn();
        }

        public void Reset()
        {
            if(!_monstersDied)
                Destroy(_clickDetector.gameObject);
            _currentLevel = 1;
            LevelChanged?.Invoke();
            _monstersDied = false;
        }
    }
}