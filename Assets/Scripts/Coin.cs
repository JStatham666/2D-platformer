using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> Destroyed;

    public void Interact()
    {
        Destroyed?.Invoke(this);
    }
}