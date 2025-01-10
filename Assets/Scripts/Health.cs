using System;
using UnityEngine;

public class Health
{
    private float _maxValue = 100f;
    private float _minValue = 1f;
    private float _currentValue = 90f;

    public event Action Died;

    public float CurrentValue => _currentValue;

    public void TakeDamage(float damage)
    {
        _currentValue -= damage;

        if (_currentValue <= 0)
        {
            Died?.Invoke();
        }
    }

    public bool TryAddValue(float recoverValue)
    {
        if (_currentValue == _maxValue) 
            return false;

        _currentValue = Mathf.Clamp(_currentValue + recoverValue, _minValue, _maxValue);
        return true;

        //if (_currentValue == _maxValue)
        //{
        //    return false;
        //}
        //else
        //{
        //    if (_currentValue + recoverValue >= _maxValue)
        //    {
        //        _currentValue = _maxValue;
        //    }
        //    else
        //    {
        //        _currentValue += recoverValue;
        //    }

        //    return true;
        //}
    }
}