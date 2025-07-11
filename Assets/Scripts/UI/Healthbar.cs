using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class Healthbar : HealthViewer
{
    protected Slider Slider;  

    private void Awake()
    {
        Slider = GetComponent<Slider>();
    }

    protected float GetNormalizedHealth()
    {
        return (float)Health.CurrentValue/Health.MaxValue;
    }
}