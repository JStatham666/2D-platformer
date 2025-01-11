using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform _trackingObject;

    private void LateUpdate() =>
        transform.position = new Vector3(_trackingObject.position.x, transform.position.y, transform.position.z);
}