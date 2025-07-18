using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VampirismChargeBar : MonoBehaviour
{
    [SerializeField] private Vampirism _ability;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _ability.DurationTimerChanged += DisplayDuration;
    }

    private void OnDisable()
    {
        _ability.DurationTimerChanged -= DisplayDuration;
    }

    private void DisplayDuration(float timer, float time)
    {
        _slider.value = Mathf.Clamp01(timer / time);
    }
}