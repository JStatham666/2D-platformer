using System;
using UnityEngine;
using System.Collections;


public class GroundCollisionDetector : MonoBehaviour
{   
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _checkRadius;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private bool _isGrounded = false;

    private float _delay = 0.1f;

    public event Action<bool> Grounded;

    private void Start() =>
        StartCoroutine(CollidedWithGroundDelay());

    private void CollidedWithGround()
    {
        _isGrounded = Physics2D.OverlapBox(_groundCheck.position, _checkRadius.localScale, 0, _ground);
        Grounded?.Invoke(_isGrounded);
    }

    private IEnumerator CollidedWithGroundDelay()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_delay);

        while (enabled)
        {
            CollidedWithGround();
            yield return waitForSeconds;
        }
    }
}
