using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health _health;

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    private void Awake()
    {
        _health = new Health();
    }

    private void Update()
    {
        if (_health.CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        _health.Died += Die;
    }

    private void OnDisable()
    {
        _health.Died -= Die;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}