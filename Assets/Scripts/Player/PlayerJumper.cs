using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(UserInput))]
public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private GroundCollisionDetector _groundCollisionDetector;
    [SerializeField] private PlayerAnimatorData _playerAnimatorData;
    [SerializeField] private float _jumpForce;

    private UserInput _userInput;
    private Rigidbody2D _rigidbody2d;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _userInput = GetComponent<UserInput>();
    }

    private void OnEnable()
    {
        _userInput.JumpKeyPressed += Jump;
        _groundCollisionDetector.Grounded += ChangeState;
    }

    private void OnDisable()
    {
        _userInput.JumpKeyPressed -= Jump;
        _groundCollisionDetector.Grounded -= ChangeState;
    }

    private void ChangeState(bool isGrounded)
    {
        _isGrounded = isGrounded;

        _playerAnimatorData.SetupIsGrounded(isGrounded);
        _playerAnimatorData.SetupAttack(false);
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rigidbody2d.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}
