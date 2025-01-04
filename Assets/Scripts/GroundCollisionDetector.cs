using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimatorData))]
public class GroundCollisionDetector : MonoBehaviour
{
    [SerializeField] private PlayerAnimatorData _playerAnimatorData;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _checkRadius;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private bool _isGrounded;

    public bool OnGround => _isGrounded;

    private float _delay = 0.1f;

    private void Awake() =>
        _playerAnimatorData = GetComponent<PlayerAnimatorData>();

    private void Start() =>
        StartCoroutine(CollidedWithGroundDelay());

    private void CollidedWithGround()
    {
        _isGrounded = Physics2D.OverlapBox(_groundCheck.position, _checkRadius.localScale, 0, _ground);
        _playerAnimatorData.SetupIsGrounded(_isGrounded);
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
