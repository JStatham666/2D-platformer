using UnityEngine;

public class RegularHealthbarViewer : Healthbar
{
    protected override void OnHealthChanged()
    {
        Slider.value = GetNormalizedHealth();
    }
}