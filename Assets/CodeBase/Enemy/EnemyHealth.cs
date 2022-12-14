using System;
using System.Collections;
using CodeBase.Logic;
using TMPro;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHealth: MonoBehaviour, IHealth
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private TextMeshPro _damageText; 
        [SerializeField] private GameObject _damagePopup; 

        private float _current;
        private float _max;
        
        public event Action HealthChanged;

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
            Current -= damage;
            _animator.PlayHit();
            PickUp(damage);
            HealthChanged?.Invoke();
        }
        
        private void PickUp(float damage)
        {
            ShowText(damage);
            StartCoroutine(StartDestroyTimer(0.1f));
        }


        private void ShowText(float damage)
        {
            _damageText.text = $"-{ damage}";
            _damagePopup.SetActive(true);
        }

        private IEnumerator StartDestroyTimer(float destroyDelay)
        {
            yield return new WaitForSeconds(destroyDelay);
            _damagePopup.SetActive(false);
        }
    }
}