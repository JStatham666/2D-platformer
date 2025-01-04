using UnityEngine;

public class MedicineChest : MonoBehaviour
{
    [SerializeField] private float _recoverHealth = 50f;

    public float RecoverHealth => _recoverHealth;

    public void Collect()
    {
        Destroy(gameObject);
    }
}
