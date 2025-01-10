using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public event Action<Coin> CoinCollected;
    public event Action<MedicineChest> MedicineChestCollected;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollectable collectable))
        {
            if (collectable is Coin coin)
            {
                CoinCollected?.Invoke(coin);
            }

            if (collectable is MedicineChest medicineChest)
            {
                MedicineChestCollected?.Invoke(medicineChest);
            }
        }
    }
}
