using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour, IDamageable
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Died += Die;
    }

    private void OnDisable()
    {
        _health.Died -= Die;
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}