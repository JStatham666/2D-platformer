using UnityEngine;

[RequireComponent(typeof(UserInput))]
[RequireComponent(typeof(PlayerAnimatorData))]
public class PlayerMover : MonoBehaviour
{  
    [SerializeField] private PlayerAnimatorData _playerAnimatorData;
    [SerializeField] private UserInput _userInput;
    [SerializeField] private float _speed;
   
    private void Awake()
    {
        _userInput = GetComponent<UserInput>();
        _playerAnimatorData = GetComponent<PlayerAnimatorData>();
    }

    private void Update()
    {
        Walk();           
    }

    private void Walk()
    {
        _playerAnimatorData.SetupPositionX(Mathf.Abs(_userInput.GetVectorX()));

        Vector3 position = transform.position;
        position.x += _userInput.GetVectorX() * _speed * Time.deltaTime;
        transform.position = position;

        Flip();
    }

    private void Flip()
    {
        Quaternion rotationRightAngle = Quaternion.Euler(0f, 0f, 0f);
        Quaternion rotationLeftAngle = Quaternion.Euler(0f, 180f, 0f);

        if (_userInput.GetVectorX() > 0)
        {
            transform.rotation = rotationRightAngle;
        }
        else if (_userInput.GetVectorX() < 0)
        {
            transform.rotation = rotationLeftAngle;
        }
    }
}