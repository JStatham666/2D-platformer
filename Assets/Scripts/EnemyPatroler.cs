using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroler : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private PlayerFinder _playerFinder;
    [SerializeField] private List<Transform> _positions;
    [SerializeField] private float _speed;

    private Transform _target;

    private int _currentTarget;

    private void Awake()
    {
        _target = _positions[_currentTarget];
    }

    private void OnEnable()
    {
        _playerFinder.CollideEntered += ChangeTargetToPlayer;
        _playerFinder.CollideExit += ChangeTargetToPositions;
    }

    private void OnDisable()
    {
        _playerFinder.CollideEntered -= ChangeTargetToPlayer;
        _playerFinder.CollideExit -= ChangeTargetToPositions;
    }

    private void Update() =>
        Move();

    private void Move()
    {
        FlipTowardsTarget();

        transform.position = new Vector3(
            Mathf.MoveTowards(transform.position.x, _target.position.x, _speed * Time.deltaTime),
            transform.position.y, transform.position.z);
            

        if (transform.position.x == _positions[_currentTarget].position.x)
        {
            if (_currentTarget < _positions.Count - 1)
            {
                _currentTarget++;
            }
            else
            {
                _currentTarget = 0;
            }

            _target = _positions[_currentTarget];
        }
    }

    private void FlipTowardsTarget()
    {
        Quaternion rotationRightAngle = Quaternion.Euler(0f, 0f, 0f);
        Quaternion rotationLeftAngle = Quaternion.Euler(0f, 180f, 0f);

        if (_target.position.x > transform.position.x)
        {
            transform.rotation = rotationRightAngle;
        }
        else if (_target.position.x < transform.position.x)
        {
            transform.rotation = rotationLeftAngle;
        }
    }

    private void ChangeTargetToPlayer()
    {
        _target = _player.transform;
    }

    private void ChangeTargetToPositions()
    {
        _target = _positions[_currentTarget];
    }
}