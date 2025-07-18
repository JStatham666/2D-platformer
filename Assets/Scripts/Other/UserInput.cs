using System;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    public float Move { get; private set; }

    private KeyCode _jumpKey = KeyCode.Space;
    private KeyCode _attackKey = KeyCode.E;
    private KeyCode _vampirismKey = KeyCode.V;

    public event Action JumpKeyPressed;
    public event Action AttackKeyPressed;
    public event Action VampirismKeyPressed;

    private void Update()
    {
        Move = Input.GetAxisRaw(Horizontal);

        if (Input.GetKeyDown(_jumpKey))
        {
            JumpKeyPressed?.Invoke();
        }

        if (Input.GetKeyDown(_attackKey))
        {
            AttackKeyPressed?.Invoke();
        }

        if (Input.GetKeyDown(_vampirismKey))
        {
            VampirismKeyPressed?.Invoke();
        }
    }
}