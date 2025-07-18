using UnityEngine;

[RequireComponent(typeof(UserInput))]
public class PlayerMover : MonoBehaviour
{  
    [SerializeField] private PlayerAnimatorData _playerAnimatorData;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _skin;

    private UserInput _userInput;
    private float _direction = 0f;
   
    private void Awake()
    {
        _userInput = GetComponent<UserInput>();
    }

    private void Update()
    {
        _direction = _userInput.Move;          
    }

    private void FixedUpdate() 
    {
        Walk();
    }

    private void Walk()
    {
        _playerAnimatorData.SetupPositionX(Mathf.Abs(_direction));

        Vector3 position = transform.position;
        position.x += _direction * _speed * Time.fixedDeltaTime;
        transform.position = position;

        Flip();
    }

    private void Flip()
    {
        Quaternion rotationRightAngle = Quaternion.Euler(0f, 0f, 0f);
        Quaternion rotationLeftAngle = Quaternion.Euler(0f, 180f, 0f);

        if (_direction > 0)
        {
            _skin.rotation = rotationRightAngle;
        }
        else if (_direction < 0)
        {
            _skin.rotation = rotationLeftAngle;
        }
    }
}