using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(UserInput))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private float _damageInterval;
    [SerializeField] private CircleCollider2D _radiusCollider;
    [SerializeField] private Transform _radiusTransform;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _enemyLayer;

    private Health _health;
    private UserInput _input;
    private bool _isReady = true;

    public event Action<float, float> DurationTimerChanged;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _input = GetComponent<UserInput>();
    }

    private void OnEnable()
    {
        _input.VampirismKeyPressed += OnClickVampirismButton;
    }

    private void OnDisable()
    {
        _input.VampirismKeyPressed -= OnClickVampirismButton;
    }

    private void OnClickVampirismButton()
    {
        if (_isReady)     
            ApplyCast();       
    }

    private void ApplyCast()
    {
        StartCoroutine(ExecuteCast());
    }

    private IEnumerator ExecuteCast()
    {
        _isReady = false;

        Coroutine damageCoroutine = StartCoroutine(ExecuteDamage());

        WaitForEndOfFrame tick = new WaitForEndOfFrame();
        float timer = _duration;

        while (timer > 0)
        {
            yield return tick;

            timer = Mathf.Clamp(timer - Time.deltaTime, 0, _duration);
            DurationTimerChanged?.Invoke(timer, _duration);
        }

        StopCoroutine(damageCoroutine);
        StartCoroutine(ExecuteCooldown());
    }

    private IEnumerator ExecuteCooldown()
    {
        WaitForEndOfFrame tick = new WaitForEndOfFrame();
        float timer = 0;

        while (timer < _cooldownTime)
        {
            yield return tick;

            timer = Mathf.Clamp(timer + Time.deltaTime, 0, _cooldownTime);
            DurationTimerChanged?.Invoke(timer, _cooldownTime);
        }

        _isReady = true;
    }

    private IEnumerator ExecuteDamage()
    {
        WaitForSeconds tick = new WaitForSeconds(_damageInterval);
        int enemyCount = 2;

        while (enabled)
        {
            Collider2D[] colliders = new Collider2D[enemyCount];

            float radius = _radiusCollider.radius * _radiusTransform.localScale.x;

            int count = Physics2D.OverlapCircleNonAlloc(transform.position, radius, colliders, _enemyLayer);

            Enemy nearestEnemy = FindNearestEnemy(colliders.Take(count).ToArray());

            if (nearestEnemy != null)
            {
                _health.TryAddValue(_damage);
                nearestEnemy.TakeDamage(_damage);
            }

            yield return tick;
        }
    }

    private Enemy FindNearestEnemy(Collider2D[] colliders)
    {
        float minDistance = float.MaxValue;
        Enemy nearestEnemy = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Enemy enemy) && (enemy.transform.position - transform.position).magnitude < minDistance)
            {
                minDistance = (enemy.transform.position - transform.position).magnitude;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}