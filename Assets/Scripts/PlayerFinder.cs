using System;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    public event Action CollideEntered;
    public event Action CollideExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
        {
            CollideEntered?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
        {
            CollideExit?.Invoke();
        }
    }
}