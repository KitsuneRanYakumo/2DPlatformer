using System;
using UnityEngine;

[RequireComponent(typeof(UserInput), typeof(Jumper), typeof(Collector))]
[RequireComponent(typeof(GroundDetector), typeof(EnemyDetector))]
public class Player : Unit
{
    [SerializeField] private Vampirism _vampirism;

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

    protected override void OnAwake()
    {
        _userInput = GetComponent<UserInput>();
        _jumper = GetComponent<Jumper>();
        _collector = GetComponent<Collector>();
        _groundDetector = GetComponent<GroundDetector>();
        _enemyDetector = GetComponent<EnemyDetector>();
    }

    private void OnEnable()
    {
        _collector.CoinTaken += IncreaseAmountCoins;
        _collector.TreatmentTaken += Heal;
        _enemyDetector.FacedWithEnemy += Attack;
        Health.AmountWasted += Destroy;
        _vampirism.Occurred += Heal;
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

        if (_userInput.IsPressedVampirismButton)
        {
            _vampirism.Switch();
        }

        if (IsGrounded)
        {
            if (_userInput.IsPressedJumpButton)
            {
                _isJump = true;
            }
        }
    }

    private void OnDisable()
    {
        _collector.CoinTaken -= IncreaseAmountCoins;
        _collector.TreatmentTaken -= Heal;
        _enemyDetector.FacedWithEnemy -= Attack;
        Health.AmountWasted -= Destroy;
        _vampirism.Occurred -= Heal;
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

    private void Initialize()
    {
        _isJump = false;
        Health.Initialize();
        _amountCoins = 0;
    }
}