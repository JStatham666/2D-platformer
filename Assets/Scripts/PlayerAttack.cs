using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UserInput))]
[RequireComponent(typeof(PlayerAnimatorData))]
[RequireComponent(typeof(GroundCollisionDetector))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField] private PlayerAnimatorData _playerAnimatorData;
    [SerializeField] private GroundCollisionDetector _groundCollisionDetector;
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private LayerMask _enemy;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _damage = 100;
    [SerializeField] private float _attackColldown = 2f;

    private WaitForSeconds _wait;
    private bool _canAttack = true;

    private void Awake()
    {
        _wait = new WaitForSeconds(_attackColldown);
        _userInput = GetComponent<UserInput>();
        _playerAnimatorData = GetComponent<PlayerAnimatorData>();
        _groundCollisionDetector = GetComponent<GroundCollisionDetector>();
    }

    private void Update()
    {
        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPosition.position, _attackRange);
    }

    private void Attack()
    {
        if (_groundCollisionDetector.OnGround && Input.GetKeyDown(_userInput.AttackButton) && _canAttack)
        {
            _playerAnimatorData.SetupAttack(_canAttack);

            Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPosition.position, _attackRange, _enemy);

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(_damage);
                }
            }

            StartCoroutine(AttackColldown());
        }
    }


    private IEnumerator AttackColldown()
    {
        _canAttack = false;
        yield return _wait;
        _canAttack = true;
    }
}