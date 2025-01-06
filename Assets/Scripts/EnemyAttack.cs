using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
  
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Attack(player);
        }
    }

    private void Attack(Player player)
    {
        player.TakeDamage(_damage);
    }
}