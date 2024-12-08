using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3[] _positions;

    private int _currentTarget;

    private void Update() =>
        Move();

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _positions[_currentTarget], _speed * Time.deltaTime);

        if (transform.position == _positions[_currentTarget])
        {
            if (_currentTarget < _positions.Length - 1)
                _currentTarget++;
            else
                _currentTarget = 0;
        }
    }
}