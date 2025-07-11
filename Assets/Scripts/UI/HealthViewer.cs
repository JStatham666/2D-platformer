using UnityEngine;

public abstract class HealthViewer : MonoBehaviour
{
    [SerializeField] protected Health Health;

    private void Start()
    {
        Health = gameObject.GetComponentInParent<Health>();
    }

    private void OnEnable()
    {
        Health.ValueChanged += OnHealthChanged;
        OnHealthChanged();
    }

    private void OnDisable()
    {
        Health.ValueChanged -= OnHealthChanged;
    }

    public void Init(Health health)
    {
        Health = health;
    }

    protected abstract void OnHealthChanged();
}