using UnityEngine;

[RequireComponent(typeof(UserInput))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimatorData))]
[RequireComponent(typeof(GroundCollisionDetector))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private GroundCollisionDetector _groundCollisionDetector;
    [SerializeField] private PlayerAnimatorData _playerAnimatorData;
    [SerializeField] private UserInput _userInput;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;     
  
    private Rigidbody2D _rigidbody2d; 

    private void Awake()
    {
        _userInput = GetComponent<UserInput>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _playerAnimatorData = GetComponent<PlayerAnimatorData>();
        _groundCollisionDetector = GetComponent<GroundCollisionDetector>();
    }

    private void Update()
    {
        Walk();
        Flip();
        Jump();
    }

    private void Walk()
    {
        _playerAnimatorData.SetupPositionX(Mathf.Abs(_userInput.GetVectorX()));

        Vector3 position = transform.position;
        position.x += _userInput.GetVectorX() * _speed * Time.deltaTime;
        transform.position = position;
    }

    private void Flip()
    {
        Vector3 rotate = transform.eulerAngles;

        if (_userInput.GetVectorX() > 0)
        {
            rotate.y = 0;
            transform.rotation = Quaternion.Euler(rotate);
        }
        else if (_userInput.GetVectorX() < 0)
        {
            rotate.y = 180;
            transform.rotation = Quaternion.Euler(rotate);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(_userInput.SpaceButton) && _groundCollisionDetector.OnGround)
            _rigidbody2d.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    } 
}