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

    private float _delay = 0.1f;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(CheckingGroundDelay());
    }

    private void Update()
    {
        Walk();
        Flip();
        Jump();
    }

    private void Walk()
    {
        _moveVector.x = Input.GetAxis(Horizontal);
        _animator.SetFloat(moveX, Mathf.Abs(_moveVector.x));

        Vector3 position = transform.position;
        position.x += _moveVector.x * _speed * Time.deltaTime;
        transform.position = position;
    }

    private void Flip()
    {
        Vector3 rotate = transform.eulerAngles;

        if (_moveVector.x > 0)
        {
            rotate.y = 0;
            transform.rotation = Quaternion.Euler(rotate);
        }
        else if (_moveVector.x < 0)
        {
            rotate.y = 180;
            transform.rotation = Quaternion.Euler(rotate);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _onGround)
            _rigidbody2d.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void CheckingGround()
    {
        _onGround = Physics2D.OverlapBox(_groundCheck.position, _checkRadius.localScale, 0, _ground);
        _animator.SetBool(onGround, _onGround);
    }

    private IEnumerator CheckingGroundDelay()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_delay);

        while (true)
        {
            CheckingGround();
            yield return waitForSeconds;
        }
    }
}