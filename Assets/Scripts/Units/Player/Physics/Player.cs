using System;
using UnityEngine;

[RequireComponent(typeof(UserInput), typeof(Rigidbody2D), typeof(CollisionsPlayer))]
public class Player : Unit
{
    [SerializeField] private float _moveSpeed = 8;
    [SerializeField] private float _forceJump = 500;
    [SerializeField] private float _damage = 10;

    private UserInput _userInput;
    private Rigidbody2D _rigidbody;
    private CollisionsPlayer _collisionPlayer;
    private bool _isJump = false;

    private float _health = 100;
    private float _maxHealth = 100;
    private float _minHealth = 0;
    private int _amountCoins = 0;

    public event Action DamageTaken;

    public bool IsMoving => Direction != 0;

    public bool IsFalling => _rigidbody.velocity.y < 0;

    public bool IsGrounded => _collisionPlayer.IsTouchPlatform;

    private void OnEnable()
    {
        _collisionPlayer.FacedWithEnemy += Attack;
        _collisionPlayer.ItemFound += PickUpItem;
    }

    private void OnDisable()
    {
        _collisionPlayer.FacedWithEnemy -= Attack;
        _collisionPlayer.ItemFound -= PickUpItem;
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
        PastPositionByX = transform.position.x;
        
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

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Attack(Enemy enemy)
    {
        enemy.TakeDamage(_damage);
        Jump();
    }

    private void Move()
    {
        Vector2 direction = _moveSpeed * Time.fixedDeltaTime * _userInput.DirectionHorizontalMovement * Vector2.right;
        transform.Translate(direction);
    }

    private void Jump()
    {
        _rigidbody.AddForce(_forceJump * Vector2.up);
    }

    private void PickUpItem(Item item)
    {
        switch (item)
        {
            case Coin coin:
                IncreaseAmountCoins(coin);
                return;

            case Treatment treatment:
                Heal(treatment);
                return;
        }
    }

    private void IncreaseAmountCoins(Coin coin)
    {
        _amountCoins++;
        coin.BecomeTaken();
    }

    private void Heal(Treatment treatment)
    {
        if (treatment.AmountHealth > 0)
            _health = Mathf.MoveTowards(_health, _maxHealth, treatment.AmountHealth);

        treatment.BecomeTaken();
    }
}