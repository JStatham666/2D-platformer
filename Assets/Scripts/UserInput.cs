using UnityEngine;

public class UserInput : MonoBehaviour
{
    public readonly KeyCode SpaceButton = KeyCode.Space;
    public readonly KeyCode AttackButton = KeyCode.E;

    private Vector2 _moveVector; 
    
    public float GetVectorX() =>
         _moveVector.x = Input.GetAxis("Horizontal");
}