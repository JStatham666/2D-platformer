using System;
using System.Collections;
using UnityEngine;

public class SmoothHealthbarViewer : Healthbar
{
    [SerializeField] private float _fillSpeed = 10f;

    private Coroutine _fillingCoroutine;

    protected override void OnHealthChanged()
    {     
        if (_fillingCoroutine == null)
           _fillingCoroutine = StartCoroutine(FillSmothly());
    } 

    private IEnumerator FillSmothly()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        float targetFillValue = GetNormalizedHealth();
        float fillDelta = Math.Abs(targetFillValue - Slider.value) * _fillSpeed;

        while (Mathf.Approximately(Slider.value, targetFillValue) == false)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, targetFillValue, fillDelta * Time.deltaTime);

            yield return wait;
        }

        _fillingCoroutine = null;
    }
}