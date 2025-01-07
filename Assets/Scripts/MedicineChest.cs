using UnityEngine;

public class MedicineChest : MonoBehaviour, ICollectable
{
    [SerializeField] private float _recoverHealth = 50f;

    public float RecoverHealth => _recoverHealth;

    public void Collect()
    {
        Destroy(gameObject);
    }
}
