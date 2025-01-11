using System;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    public float Move { get; private set; }
    public bool IsJump { get; private set; }
    public bool IsAttack { get; private set; }

    private void Update()
    {
        Move = Input.GetAxisRaw(Horizontal);
        IsJump = Input.GetKeyDown(KeyCode.Space);
        IsAttack = Input.GetKeyDown(KeyCode.E);
    }

    //public float GetVectorX()
    //{
    //    Vector2 moveVector;
    //    return moveVector.x = Input.GetAxis("Horizontal");
    //}
}