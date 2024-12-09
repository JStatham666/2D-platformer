using UnityEngine;

public class UserInput : MonoBehaviour
{
    public readonly string Horizontal = "Horizontal";

    public readonly KeyCode SpaceButton = KeyCode.Space;

    private Vector2 _moveVector; 
    
    public float GetVectorX() =>
         _moveVector.x = Input.GetAxis(Horizontal);
}