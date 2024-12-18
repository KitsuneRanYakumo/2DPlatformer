using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8;
    [SerializeField] private float _forceJump = 500;

    private UserInput _userInput;
    private Rigidbody2D _rigidbody;
    private CollisionsPlayer _collisionPlayer;
    private float _pastPositionByX;
    private bool _isJump = false;
    private int _amountCoins = 0;

    public float Direction => transform.position.x - _pastPositionByX;

    public bool IsMoving => Direction != 0;

    public bool IsFalling => _rigidbody.velocity.y < 0;

    public bool IsGrounded => _collisionPlayer.IsTouchPlatform;

    private void Awake()
    {
        _userInput = GetComponent<UserInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionPlayer = GetComponent<CollisionsPlayer>();
    }

    private void FixedUpdate()
    {
        if (_isJump)
        {
            Jump();
            _isJump = false;
        }
    }

    private void Update()
    {
        Move();

        if (IsGrounded == false)
            return;

        if (_userInput.IsPressedJumpButton == false)
            return;

        _isJump = true;
    }

    public void IncreaseAmountCoins()
    {
        _amountCoins++;
    }

    private void Move()
    {
        _pastPositionByX = transform.position.x;
        Vector2 direction = _moveSpeed * Time.deltaTime * _userInput.HorizontalMovement * Vector2.right;
        transform.Translate(direction);
    }

    private void Jump()
    {
        _rigidbody.AddForce(_forceJump * Vector2.up);
    }
}