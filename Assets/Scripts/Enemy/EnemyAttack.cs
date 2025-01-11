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
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            if (_attackCoroutine == null)
            {
                _attackCoroutine = StartCoroutine(AttackWithCooldown(damageable));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
    }

    private void Attack(IDamageable damageable)
    {
        damageable.TakeDamage(_damage);
    }

    private IEnumerator AttackWithCooldown(IDamageable damageable)
    {
        while (enabled)
        {
            Attack(damageable);

            yield return _wait;
        }
    }
}