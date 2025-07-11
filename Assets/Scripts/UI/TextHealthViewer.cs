using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextHealthViewer : HealthViewer
{
    private TextMeshProUGUI _text;   

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    protected override void OnHealthChanged()
    {
        _text.text = $"{Health.CurrentValue}/{Health.MaxValue}";
    }
}
