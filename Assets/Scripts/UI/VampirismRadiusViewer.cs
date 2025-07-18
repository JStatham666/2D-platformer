using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class VampirismRadiusViewer : MonoBehaviour
{
    [SerializeField] private Vampirism _ability;
    private Renderer _radius;

    private void Awake()
    {
        _radius = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _ability.CastStarted += ShowRadius;
        _ability.CastEnded += HideRadius;
        HideRadius();
    }

    private void OnDisable()
    {
        _ability.CastStarted -= ShowRadius;
        _ability.CastEnded -= HideRadius;
    }

    private void ShowRadius()
    {
        _radius.enabled = true;
    }

    private void HideRadius()
    {
        _radius.enabled = false;
    }
}
