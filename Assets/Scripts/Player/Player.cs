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
        _collector.CoinCollected += CollectCoin;
        _collector.MedicineChestCollected += CollectMed;
    }

    private void OnDisable()
    {
        _health.Died -= Die;
        _collector.CoinCollected -= CollectCoin;
        _collector.MedicineChestCollected -= CollectMed;
    }

    private void CollectCoin(Coin coin)
    {
        coin.Collect();
    }

    private void CollectMed(MedicineChest medicineChest)
    {
        if (TryAddHealth(medicineChest.RecoverHealth) == false)
            return;

        medicineChest.Collect();
    }

    public bool TryAddHealth(float recoverHealth)
    {
        return _health.TryAddValue(recoverHealth);
    }

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