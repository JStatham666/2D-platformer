using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health _health;

    private void Awake()
    {
        _health = new Health();
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