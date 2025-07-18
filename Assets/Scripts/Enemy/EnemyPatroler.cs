using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPatroler : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private PlayerFinder _playerFinder;
    [SerializeField] private List<Transform> _positions;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _skin;
    [SerializeField] private Flipper _flipper;

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
        Vector2 direction = _target.position - transform.position;
        _flipper.SetLookRotation(direction.x);

        transform.position = new Vector3(
            Mathf.MoveTowards(transform.position.x, _target.position.x, _speed * Time.deltaTime),
            transform.position.y, transform.position.z);

        if (transform.position.x == _positions[_currentTarget].position.x)
        {
            _currentTarget = ++_currentTarget % _positions.Count;

            _target = _positions[_currentTarget];
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