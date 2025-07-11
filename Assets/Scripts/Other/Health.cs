using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue = 100f;
    [SerializeField] private float _minValue = 1f;
    [SerializeField] private float _currentValue = 100f;

    public event Action Died;
    public event Action ValueChanged;

    public float CurrentValue => _currentValue;
    public float MaxValue => _maxValue;

    public void TakeDamage(float damage)
    {
        _currentValue -= damage;

        if (_currentValue <= 0)
        {
            Died?.Invoke();
        }

        ValueChanged?.Invoke();
    }

    public bool TryAddValue(float recoverValue)
    {
        if (_currentValue == _maxValue) 
            return false;

        _currentValue = Mathf.Clamp(_currentValue + recoverValue, _minValue, _maxValue);
        ValueChanged?.Invoke();
        return true;
    }

    public void Reborn()
    {
        _currentValue = _maxValue;
    }
}