using UnityEngine;

public abstract class HealthViewer : MonoBehaviour
{
    [SerializeField] protected Health Health;

    private void OnEnable()
    {
        Health.ValueChanged += OnHealthChanged;
        OnHealthChanged();
    }

    private void OnDisable()
    {
        Health.ValueChanged -= OnHealthChanged;
    }

    protected abstract void OnHealthChanged();
}