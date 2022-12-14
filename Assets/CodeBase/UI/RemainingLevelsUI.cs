using CodeBase.Logic.EnemySpawners;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class RemainingLevelsUI : MonoBehaviour
    {
        public TextMeshProUGUI _remainingLevels;
        private EnemySpawner _enemySpawner;

        public void Construct(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
            _enemySpawner.LevelChanged += UpdateLevel;
            UpdateLevel();
        }

        private void OnDestroy() => 
            _enemySpawner.LevelChanged -= UpdateLevel;

        private void UpdateLevel()
        {
            int remainingCount = _enemySpawner.LevelCount - _enemySpawner.CurrentLevel;
            _remainingLevels.text = $"remaining: {remainingCount}";
        }

    }
}