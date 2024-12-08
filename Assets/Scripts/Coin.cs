using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> Destroyed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))        
            Destroyed?.Invoke(this);      
    }
}