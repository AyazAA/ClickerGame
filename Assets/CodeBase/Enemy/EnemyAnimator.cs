using UnityEngine;

namespace CodeBase.Enemy
{
  public class EnemyAnimator : MonoBehaviour
  {
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Die = Animator.StringToHash("Die");

    [SerializeField] private Animator _animator;

    public void PlayHit() => _animator.SetTrigger(Hit);
    public void PlayDeath() => _animator.SetTrigger(Die);
  }
}