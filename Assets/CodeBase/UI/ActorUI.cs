using CodeBase.Enemy;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
        private EnemyHealth _heroHealth;

        public void Construct(EnemyHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.HealthChanged += UpdateHpBar;
        }

        private void OnDestroy() => 
            _heroHealth.HealthChanged -= UpdateHpBar;

        private void UpdateHpBar() => 
            _hpBar.SetValue(_heroHealth.Current,_heroHealth.Max);
    }
}