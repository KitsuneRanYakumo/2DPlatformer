using System;
using UnityEngine;

[RequireComponent(typeof(UserInput), typeof(Jumper), typeof(Collector))]
[RequireComponent(typeof(GroundDetector), typeof(EnemyDetector))]
public class Player : Unit, IDamageble
{
    private UserInput _userInput;
    private Jumper _jumper;
    private bool _isJump;

    private Collector _collector;
    private GroundDetector _groundDetector;
    private EnemyDetector _enemyDetector;

    private int _amountCoins;

    public bool IsMoving => Mover.DirectionByX != 0;

    public bool IsFalling => _jumper.DirectionByY < 0;

    public bool IsGrounded => _groundDetector.IsTouchPlatform;

    private void Awake()
    {
        SetComponents();
    }

    private void OnEnable()
    {
        _collector.CoinTaken += IncreaseAmountCoins;
        _collector.TreatmentTaken += Heal;
        _enemyDetector.FacedWithEnemy += Attack;
        Health.AmountWasted += Destroy;
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

    private void OnDisable()
    {
        _collector.CoinTaken -= IncreaseAmountCoins;
        _collector.TreatmentTaken -= Heal;
        _enemyDetector.FacedWithEnemy -= Attack;
        Health.AmountWasted -= Destroy;
    }

    protected override void Attack(IDamageble unit)
    {
        unit.TakeDamage(Damage);
        _jumper.Jump();
    }

    private void IncreaseAmountCoins()
    {
        _amountCoins++;
    }

    private void Heal(float treatment)
    {
        Health.TakeTreatment(treatment);
    }

    private void SetComponents()
    {
        _userInput = GetComponent<UserInput>();
        Health = GetComponent<Health>();
        Mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _collector = GetComponent<Collector>();
        _groundDetector = GetComponent<GroundDetector>();
        _enemyDetector = GetComponent<EnemyDetector>();
    }

    private void Initialize()
    {
        _isJump = false;
        Health.Initialize();
        _amountCoins = 0;
    }
}