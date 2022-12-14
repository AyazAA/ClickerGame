using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private Canvas _hud;
        
        public event Action Happened;
          
        private void Start() =>
            _enemyHealth.HealthChanged += OnHealthChanged;

        private void OnDestroy() =>
            _enemyHealth.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (_enemyHealth.Current <= 0)
                Die();
        }

        private void Die()
        {
            _enemyHealth.HealthChanged -= OnHealthChanged;
            _hud.enabled = false;
            _animator.PlayDeath();
            StartCoroutine(DestroyTimer(0.5f));
        }

        private IEnumerator DestroyTimer(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
            Happened?.Invoke();
        }
    }
}