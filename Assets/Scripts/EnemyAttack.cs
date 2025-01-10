using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damage = 40;
    [SerializeField] private float _attackColldown = 1f;

    private WaitForSeconds _wait;
    private Coroutine _attackCoroutine;

    private void Awake()
    {
        _wait = new WaitForSeconds(_attackColldown);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (_attackCoroutine == null)
            {
                _attackCoroutine = StartCoroutine(AttackWithCooldown(player));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
    }

    private void Attack(Player player)
    {
        player.TakeDamage(_damage);
    }

    private IEnumerator AttackWithCooldown(Player player)
    {
        while (enabled)
        {
            Attack(player);

            yield return _wait;
        }
    }
}