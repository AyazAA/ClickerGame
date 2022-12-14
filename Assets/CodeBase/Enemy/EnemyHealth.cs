using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;

        private float _current;
        private float _max;

        public event Action HealthChanged;
        public event Action<float> DamageCaused;

        public float Current
        {
            get => _current;
            set => _current = value;
        }

        public float Max
        {
            get => _max;
            set => _max = value;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;
            if (Current > 0)
                _animator.PlayHit();
            DamageCaused?.Invoke(damage);
            HealthChanged?.Invoke();
        }
    }
}