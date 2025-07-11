using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UserInput))]
//[RequireComponent(typeof(PlayerAnimatorData))]
//[RequireComponent(typeof(GroundCollisionDetector))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField] private PlayerAnimatorData _playerAnimatorData;
    [SerializeField] private GroundCollisionDetector _groundCollisionDetector;
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private LayerMask _enemy;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _damage = 50;
    [SerializeField] private float _attackColldown = 2f;

    private WaitForSeconds _wait;
    private bool _canAttack = true;
    private bool _isGrounded;

    private void Awake()
    {
        _wait = new WaitForSeconds(_attackColldown);
        _userInput = GetComponent<UserInput>();
        //_playerAnimatorData = GetComponent<PlayerAnimatorData>();
    }

    private void Update()
    {
        TryAttack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPosition.position, _attackRange);
    }

    private void OnEnable()
    {
        _groundCollisionDetector.Grounded += ChangeState;
    }

    private void OnDisable()
    {
        _groundCollisionDetector.Grounded -= ChangeState;
    }

    private void ChangeState(bool isGrounded)
    {
        _isGrounded = isGrounded;
    }

    private void TryAttack()
    {
        if (_isGrounded && _userInput.IsAttack && _canAttack)
        {
            _playerAnimatorData.SetupAttack(_canAttack);

            Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPosition.position, _attackRange, _enemy);

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(_damage);
                    Debug.Log("враг получил " + _damage + " урона");
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