using System;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public event Action<Coin> Destroyed;

    public void Collect() =>
        Destroyed?.Invoke(this);
}