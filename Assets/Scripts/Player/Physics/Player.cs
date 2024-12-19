using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8;
    [SerializeField] private float _forceJump = 500;
    [SerializeField] private float _damage = 10;

    private UserInput _userInput;
    private Rigidbody2D _rigidbody;
    private CollisionsPlayer _collisionPlayer;
    private float _pastPositionByX;
    private bool _isJump = false;
    private float _health = 100;
    private float _maxHealth = 100;
    private float _minHealth = 0;
    private int _amountCoins = 0;

    public event Action DamageTaken;

    public float Direction => transform.position.x - _pastPositionByX;

    public bool IsMoving => Direction != 0;

    public bool IsFalling => _rigidbody.velocity.y < 0;

    public bool IsGrounded => _collisionPlayer.IsTouchPlatform;

    private void OnEnable()
    {
        _collisionPlayer.FacedWithEnemy += Attack;
    }

    private void OnDisable()
    {
        _collisionPlayer.FacedWithEnemy -= Attack;
    }

    private void Awake()
    {
        _userInput = GetComponent<UserInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionPlayer = GetComponent<CollisionsPlayer>();
    }

    private void FixedUpdate()
    {
        Move();

        if (_isJump)
        {
            Jump();
            _isJump = false;
        }
    }

    private void Update()
    {
        if (IsGrounded == false)
            return;

        if (_userInput.IsPressedJumpButton == false)
            return;

        _isJump = true;
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            _health = Mathf.MoveTowards(_health, _minHealth, damage);
            DamageTaken?.Invoke();
        }

        if (_health < 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(float amount)
    {
        Debug.Log($"Здоровье до лечения - {_health}.");

        if (amount > 0)
            _health = Mathf.MoveTowards(_health, _maxHealth, amount);

        Debug.Log($"Здоровье после лечения - {_health}.");
    }

    public void Jump()
    {
        _rigidbody.AddForce(_forceJump * Vector2.up);
    }

    public void IncreaseAmountCoins()
    {
        _amountCoins++;
    }

    private void Move()
    {
        _pastPositionByX = transform.position.x;
        Vector2 direction = _moveSpeed * Time.fixedDeltaTime * _userInput.HorizontalMovement * Vector2.right;
        transform.Translate(direction);
    }

    private void Attack(Enemy enemy)
    {
        enemy.TakeDamage(_damage);
    }
}