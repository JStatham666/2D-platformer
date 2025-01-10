using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Collector  _collector;

    private Health _health;

    public void Update()
    {
        Debug.Log(_health.CurrentValue);
    }

    private void Awake()
    {
        _health = new Health();
    }
    private void OnEnable()
    {
        _health.Died += Die;
        //_collector.CoinCollected += Collect;
        //_collector.MedicineChestCollected += Collect;
    }

    private void OnDisable()
    {
        _health.Died -= Die;
    //  _collector.CoinCollected -= Collect;
    //  _collector.MedicineChestCollected -= Collect;
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

    public bool TryAddHealth(float recoverHealth)
    {
        return _health.TryAddValue(recoverHealth);
    }

    //private void Collect()
    //{       
    //    collectable.Collect();
    //}

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }


    private void Die()
    {
        _health.Died -= Die;
        transform.position = _startPosition.position;
        _health = new Health();
        _health.Died += Die;
    }
}