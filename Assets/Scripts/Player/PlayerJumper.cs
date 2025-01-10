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

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _userInput = GetComponent<UserInput>();
    }

    private void Update()
    {
        Jump();
        ChangeState();
    }

    private void OnEnable()
    {
        _groundCollisionDetector.Grounded += ChangeState;
    }

    private void OnDisable()
    {
        _groundCollisionDetector.Grounded -= ChangeState;
    }

    private void ChangeState()
    {
        _playerAnimatorData.SetupIsGrounded(_groundCollisionDetector.OnGround);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(_userInput.SpaceButton) && _groundCollisionDetector.OnGround)
            _rigidbody2d.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }
}
