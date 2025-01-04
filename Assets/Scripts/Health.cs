using System;
using UnityEngine;

public class Health
{
    [SerializeField] private float _maxHealth = 100f;

    [SerializeField] private float _currentHealth = 100f;

    public float CurrentHealth => _currentHealth;

    public event Action Died;

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Died?.Invoke();
        }
    }

    public bool TryAddHealth(float recoverHealth)
    {
        if (_currentHealth + recoverHealth > _maxHealth)
        {
            return false;
        }
        else
        {
            _currentHealth += recoverHealth;
            return true;
        }
    }
}