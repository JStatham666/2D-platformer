using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GroundCollisionDetector _groundCollisionDetector;
    [SerializeField] private PlayerAnimatorData _playerAnimatorData;
    [SerializeField] private UserInput _userInput;
    [SerializeField] private int _attackImpulse = 200;
    [SerializeField] private bool _LockAttack = false;

    [SerializeField] private Transform _attackPosition;
    [SerializeField] private LayerMask _enemy;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _damage = 100;

    private Rigidbody2D _rigidbody2d;

    private void Awake()
    {
        _userInput = GetComponent<UserInput>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _playerAnimatorData = GetComponent<PlayerAnimatorData>();
        _groundCollisionDetector = GetComponent<GroundCollisionDetector>();
    }

    private void Update()
    {
        AnimateAttack();
    }

    private void AnimateAttack()
    {
        if (_groundCollisionDetector.OnGround && Input.GetKeyDown(_userInput.AttackButton) && !_LockAttack)
        {
            _LockAttack = true;
            Invoke("AttackLock", 2f);

            _playerAnimatorData.SetupAttack(_LockAttack);

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
                enemies[i].GetComponent<Enemy>().TakeDamage(_damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPosition.position, _attackRange);
    }
 

    private void AttackLock()
    {
        _LockAttack = false;
    }
}
