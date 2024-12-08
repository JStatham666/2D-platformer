using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public readonly string Horizontal = "Horizontal";
    public readonly string moveX = "moveX";
    public readonly string onGround = "onGround";

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _onGround;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _checkRadius;
    [SerializeField] private LayerMask _ground;

    private Rigidbody2D _rigidbody2d;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Vector2 _moveVector;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Walk();
        Flip();
        Jump();
        CheckingGround();           
    }

    private void Walk() 
    {
        Vector3 position = transform.position;
        _moveVector.x = Input.GetAxis(Horizontal);
        _animator.SetFloat(moveX, Mathf.Abs(_moveVector.x));

        position.x += _moveVector.x * _speed * Time.deltaTime;
        transform.position = position;
    } 

    private void Flip()
    {
        if (_moveVector.x > 0)
            _spriteRenderer.flipX = false;
        else if (_moveVector.x < 0)
            _spriteRenderer.flipX = true;
    }

    private void Jump()
    {     
        if (Input.GetKeyDown(KeyCode.Space) && _onGround)       
            _rigidbody2d.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void CheckingGround()
    {     
        _onGround = Physics2D.OverlapBox(_groundCheck.position, _checkRadius.localScale, 0,  _ground);     
        _animator.SetBool(onGround, _onGround);
    }
}