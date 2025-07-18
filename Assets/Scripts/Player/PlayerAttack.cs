using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UserInput))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerAnimatorData _playerAnimatorData;
    [SerializeField] private GroundCollisionDetector _groundCollisionDetector;
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private LayerMask _enemy;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _damage = 50;
    [SerializeField] private float _attackColldown = 2f;

    private UserInput _userInput;
    private WaitForSeconds _wait;
    private bool _canAttack = true;
    private bool _isGrounded;

    private void Awake()
    {
        _wait = new WaitForSeconds(_attackColldown);
        _userInput = GetComponent<UserInput>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPosition.position, _attackRange);
    }

    private void OnEnable()
    {
        _userInput.AttackKeyPressed += TryAttack;
        _groundCollisionDetector.Grounded += ChangeState;
    }

    private void OnDisable()
    {
        _userInput.AttackKeyPressed -= TryAttack;
        _groundCollisionDetector.Grounded -= ChangeState;
    }

    private void ChangeState(bool isGrounded)
    {
        _isGrounded = isGrounded;
    }

    private void TryAttack()
    {
        if (_isGrounded && _canAttack)
        {
            _playerAnimatorData.SetupAttack(_canAttack);

            Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPosition.position, _attackRange, _enemy);

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(_damage);
                }
            }

            StartCoroutine(Colldown());
        }
    }

    private IEnumerator Colldown()
    {
        _canAttack = false;
        yield return _wait;
        _canAttack = true;
    }
}