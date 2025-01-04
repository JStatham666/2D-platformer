using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(UserInput))]
public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private GroundCollisionDetector _groundCollisionDetector;
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
    }

    private void Jump()
    {
        if (Input.GetKeyDown(_userInput.SpaceButton) && _groundCollisionDetector.OnGround)
            _rigidbody2d.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }
}
