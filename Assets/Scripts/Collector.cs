using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public event Action CoinCollected;
    public event Action MedicineChestCollected;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollectable collectable))
        {
            if (collectable is Coin coin)
            {
                CoinCollected?.Invoke();
            }

            if (collectable is MedicineChest medicineChest)
            {
                MedicineChestCollected?.Invoke();
                //if (TryAddHealth(medicineChest.RecoverHealth) == false)
                //    return;
            }

            //collectable.Collect();
        }
    }
}
