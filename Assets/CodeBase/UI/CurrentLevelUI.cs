using CodeBase.Logic.EnemySpawners;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class CurrentLevelUI : MonoBehaviour
    {
        public TextMeshProUGUI _currentLevel;
        private EnemySpawner _enemySpawner;

        public void Construct(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
            _enemySpawner.LevelChanged += UpdateLevel;
            UpdateLevel();
        }

        private void OnDestroy() => 
            _enemySpawner.LevelChanged -= UpdateLevel;

        private void UpdateLevel() => 
            _currentLevel.text = $"lvl {_enemySpawner.CurrentLevel}";
    }
}