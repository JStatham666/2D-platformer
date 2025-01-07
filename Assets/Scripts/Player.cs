using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollectable collectable))
        {
            if (collectable is MedicineChest medicineChest)
            {
                if (TryAddHealth(medicineChest.RecoverHealth) == false)                
                    return;               
            }

            collectable.Collect();
        }
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    public bool TryAddHealth(float recoverHealth)
    {
        return _health.TryAddHealth(recoverHealth);
    }

    private void Die()
    {
        _health.Died -= Die;
        transform.position = _startPosition.position;
        _health = new Health();
        _health.Died += Die;
    }
}