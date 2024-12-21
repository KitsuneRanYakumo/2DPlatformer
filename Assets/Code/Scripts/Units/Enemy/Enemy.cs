using System;
using UnityEngine;

[RequireComponent(typeof(PlayerDetector), typeof(ControllerTarget))]
public class Enemy : Unit, IDamageble
{
    [SerializeField] private float _damage = 10;

    private Health _health;
    private PlayerDetector _playerDetector;
    private ControllerTarget _controllerTarget;

    public event Action DamageTaken;

    public CheckPoint CurrentPoint { get; private set; }

    private void OnEnable()
    {
        _playerDetector.FacedWithPlayer += Attack;
    }

    private void OnDisable()
    {
        _playerDetector.FacedWithPlayer -= Attack;
    }

    private void Awake()
    {
        Mover = GetComponent<Mover>();
        _playerDetector = GetComponent<PlayerDetector>();
        _controllerTarget = GetComponent<ControllerTarget>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        Vector2 direction = ((_controllerTarget.Target.position.x - transform.position.x) * Vector2.right).normalized;
        Mover.Move(direction);
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
        DamageTaken?.Invoke();

        if (_health.Amount > 0)
            return;
        
        Destroy(gameObject);
    }

    private void Attack(IDamageble unit)
    {
        unit.TakeDamage(_damage);
    }

    private void Initialize()
    {
        _health = new Health();
        _health.Initialize();
    }
}