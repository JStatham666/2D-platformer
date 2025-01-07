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
    //[SerializeField] private int _attackImpulse = 200;
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private LayerMask _enemy;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _damage = 100;
    [SerializeField] private float _attackColldown = 2f;

    private WaitForSeconds _wait;
    private bool _canAttack = true;

    //private Rigidbody2D _rigidbody2d;

    private void Awake()
    {
        _wait = new WaitForSeconds(_attackColldown);
        _userInput = GetComponent<UserInput>();
        //_rigidbody2d = GetComponent<Rigidbody2D>();
        _playerAnimatorData = GetComponent<PlayerAnimatorData>();
        _groundCollisionDetector = GetComponent<GroundCollisionDetector>();
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (_groundCollisionDetector.OnGround && Input.GetKeyDown(_userInput.AttackButton) && _canAttack)
        {           
            _playerAnimatorData.SetupAttack(_canAttack);

            //_rigidbody2d.velocity = new Vector2(0, 0);

            //Quaternion rotationLeftAngle = Quaternion.Euler(0f, -180f, 0f);

            //if (_rigidbody2d.transform.rotation == rotationLeftAngle)
            //{
            //    _rigidbody2d.AddForce(Vector2.left * _attackImpulse);
            //}
            //else
            //{
            //    _rigidbody2d.AddForce(Vector2.right * _attackImpulse);
            //}

            Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPosition.position, _attackRange, _enemy);

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(_damage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPosition.position, _attackRange);
    }

    private IEnumerator AttackColldown()
    {
        _canAttack = false;
        yield return _wait;
        _canAttack = true;
    }
}