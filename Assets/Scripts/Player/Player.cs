using UnityEngine;

[RequireComponent(typeof(UserInput), typeof(SpriteRenderer), typeof(Animator))]
public class Player : MonoBehaviour
{
    private readonly int _moveTrigger = Animator.StringToHash("IsMoving");

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _forceJump;
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private float _rayLength;

    private UserInput _userInput;
    private SpriteRenderer _sprite;
    private Animator _animator;

    private Vector2 _moveDirection;
    private Vector2 _gravityDirection = Vector2.down;
    private int _amountCoins;

    private bool _isMoving => _moveDirection.x != 0;

    private bool _isGrounded => TryTouchPlatform();

    private void Awake()
    {
        _userInput = GetComponent<UserInput>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        SetMoveDirection();

        if (_isGrounded && _userInput.IsPressedJumpButton)
            SetJumpDirection();

        _moveDirection += _gravityDirection;

        if (_isMoving)
            AnimateMove();

        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            PickUpCoin(coin);
        }
    }

    private void Move()
    {
        transform.Translate(_moveSpeed * _moveDirection);
    }

    private void SetMoveDirection()
    {
        _moveDirection = new Vector2(_userInput.HorizontalMovement, 0);
    }

    private void AnimateMove()
    {
        DetermineSpriteFlipX();
        _animator.SetBool(_moveTrigger, _isMoving);
    }

    private void DetermineSpriteFlipX()
    {
        if (_moveDirection.x > 0)
            _sprite.flipX = false;
        else if (_moveDirection.x < 0)
            _sprite.flipX = true;
    }

    private void SetJumpDirection()
    {
        _moveDirection += _forceJump * Vector2.up;
    }

    private bool TryTouchPlatform()
    {
        RaycastHit2D hit = Physics2D.Raycast(_raycastPoint.position, Vector3.down, _rayLength);

        if (hit.collider != null && hit.collider.GetComponent<Planform>() != null)
            return true;

        return false;
    }

    private void PickUpCoin(Coin coin)
    {
        coin.BecomeTaken();
        _amountCoins++;
    }
}