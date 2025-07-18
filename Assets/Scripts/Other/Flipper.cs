using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Flipper : MonoBehaviour
{
    private Transform _spriteTransform;

    Quaternion rotationRightAngle = Quaternion.Euler(0f, 0f, 0f);
    Quaternion rotationLeftAngle = Quaternion.Euler(0f, 180f, 0f);

    private void Awake()
    {
        _spriteTransform = GetComponent<Transform>();
    }

    public void SetLookRotation(float directionX)
    {
        if (directionX > 0)
        {
            _spriteTransform.rotation = rotationRightAngle;
        }
        else if (directionX < 0)
        {
            _spriteTransform.rotation = rotationLeftAngle;
        }
    }
}
