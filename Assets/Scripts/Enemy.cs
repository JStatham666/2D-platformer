using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3[] _positions;

    private int _currentTarget;

    void Update()
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