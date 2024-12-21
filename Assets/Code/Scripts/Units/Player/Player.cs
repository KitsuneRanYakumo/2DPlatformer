using System;
using UnityEngine;

[RequireComponent(typeof(UserInput), typeof(Jumper), typeof(Collector))]
[RequireComponent(typeof(GroundDetector), typeof(EnemyDetector))]
public class Player : Unit, IDamageble
{
    [SerializeField] private float _damage = 10;

    private UserInput _userInput;
    private Jumper _jumper;
    private bool _isJump;

    private Collector _collector;
    private GroundDetector _groundDetector;
    private EnemyDetector _enemyDetector;

    private int _amountCoins;

    public event Action<float> DamageTaken;

    public Health Health { get; private set; }

    public bool IsMoving => Mover.DirectionByX != 0;

    public bool IsFalling => _jumper.DirectionByY < 0;

    public bool IsGrounded => _groundDetector.IsTouchPlatform;

    private void OnEnable()
    {
        _enemyDetector.FacedWithEnemy += Attack;
        _collector.CoinTaken += IncreaseAmountCoins;
    }

    private void OnDisable()
    {
        _enemyDetector.FacedWithEnemy -= Attack;
        _collector.CoinTaken -= IncreaseAmountCoins;
    }

    private void Awake()
    {
        _userInput = GetComponent<UserInput>();
        Mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _collector = GetComponent<Collector>();
        _groundDetector = GetComponent<GroundDetector>();
        _enemyDetector = GetComponent<EnemyDetector>();
    }

    private void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        if (_isJump)
        {
            _jumper.Jump();
            _isJump = false;
        }
    }

    private void Update()
    {
        Vector2 direction = _userInput.DirectionHorizontalMovement * Vector2.right;
        Mover.Move(direction);

        if (IsGrounded == false)
            return;

        if (_userInput.IsPressedJumpButton == false)
            return;

        _isJump = true;
    }

    public void TakeDamage(float damage)
    {
        DamageTaken?.Invoke(damage);
    }

    private void Attack(IDamageble unit)
    {
        unit.TakeDamage(_damage);
        _jumper.Jump();
    }

    private void IncreaseAmountCoins()
    {
        _amountCoins++;
    }

    private void Initialize()
    {
        _isJump = false;
        Health = new Health();
        Health.Initialize();
        _amountCoins = 0;
    }
}