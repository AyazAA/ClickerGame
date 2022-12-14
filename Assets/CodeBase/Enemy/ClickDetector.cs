using UnityEngine;

namespace CodeBase.Enemy
{
    public class ClickDetector : MonoBehaviour
    {
        public bool CanTakeDamage = true;
        [SerializeField] private EnemyHealth _enemyHealth;
        private float _damage;

        public void Construct(float damage)
        {
            _damage = damage;
        }

        private void OnMouseDown()
        {
            if(Application.isMobilePlatform && Input.touchCount != 1)
                return;
            
            if(!CanTakeDamage)
                return;
            
            _enemyHealth.TakeDamage(_damage);
        }
    }
}