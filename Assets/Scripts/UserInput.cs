using UnityEngine;

public class UserInput : MonoBehaviour
{
    public readonly KeyCode SpaceButton = KeyCode.Space;
    public readonly KeyCode AttackButton = KeyCode.E;
   
    public float GetVectorX()
    {
        Vector2 moveVector;
        return moveVector.x = Input.GetAxis("Horizontal");
    }
}