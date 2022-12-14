using System.Collections;
using TMPro;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class DamageFX : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private TextMeshPro _damageText;
        [SerializeField] private GameObject _damagePopup;

        private void Start() => 
            _enemyHealth.DamageCaused += PickUp;

        private void OnDestroy() => 
            _enemyHealth.DamageCaused -= PickUp;

        private void PickUp(float damage)
        {
            ShowText(damage);
            StartCoroutine(StartDestroyTimer(0.1f));
        }

        private void ShowText(float damage)
        {
            _damageText.text = $"-{damage}";
            _damagePopup.SetActive(true);
        }

        private IEnumerator StartDestroyTimer(float destroyDelay)
        {
            yield return new WaitForSeconds(destroyDelay);
            _damagePopup.SetActive(false);
        }
    }
}