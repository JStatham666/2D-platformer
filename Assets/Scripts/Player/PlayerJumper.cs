using UnityEngine;

[RequireComponent(typeof(PlayerAnimatorData))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(UserInput))]
public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private GroundCollisionDetector _groundCollisionDetector;
    [SerializeField] private PlayerAnimatorData _playerAnimatorData;
    [SerializeField] private UserInput _userInput;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody2d;
    private bool _isJump;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _userInput = GetComponent<UserInput>();
    }

    private void Update()
    {
        if (_userInput.IsJump && _groundCollisionDetector.OnGround)
            _isJump = true;

        ChangeState();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void OnEnable()
    {
        _groundCollisionDetector.Grounded += ChangeState;
    }

    private void OnDisable()
    {
        _groundCollisionDetector.Grounded -= ChangeState;
    }

    //private void ChangeState(bool grounded)
    //{
    //    _playerAnimatorData.SetupIsGrounded(grounded);
    //}

    private void ChangeState()
    {
        _playerAnimatorData.SetupIsGrounded(_groundCollisionDetector.OnGround);
    }

    private void Jump()
    {
        if (_isJump)
        {
            _rigidbody2d.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            _isJump = false;
        }
    }
}
