using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;

    private Health _health;
    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    public bool TryAddHealth(float recoverHealth)
    {
        if (_health.TryAddHealth(recoverHealth))
        {
            return true;
        }

        return false;
    }

    private void Awake()
    {
        _health = new Health();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
            coin.Interact();

        if (collision.gameObject.TryGetComponent(out MedicineChest medicineChest))
        {
            if (TryAddHealth(medicineChest.RecoverHealth) == false)
            {
                return;
            }

            medicineChest.Collect();
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
        _health.Died -= Die;
        transform.position = _startPosition.position;
        _health = new Health();
        _health.Died += Die;
    }
}