using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _attackColldown = 2f;

    private WaitForSeconds _wait;
    private bool _canAttack = true;
    private bool _mustAttack = false;

    private void Awake()
    {
        _wait = new WaitForSeconds(_attackColldown);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) && _canAttack)
        {
            _mustAttack = true;
            StartCoroutine(AttackWithColldown(player));
            //coroutine = StartCoroutine(AttackWithColldown(player));         
        }
    }

    //Coroutine coroutine;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            //StopCoroutine(coroutine);
            _mustAttack = false;
        }
    }

    private void Attack(Player player)
    {
        player.TakeDamage(_damage);
    }

    private IEnumerator AttackWithColldown(Player player)
    {
        while (_mustAttack)
        {
            Attack(player);

            _canAttack = false;
            yield return _wait;
            _canAttack = true;
        }
    }
}