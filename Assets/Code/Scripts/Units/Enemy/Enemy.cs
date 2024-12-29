using System;
using UnityEngine;

[RequireComponent(typeof(TargetFinder), typeof(PlayerDetector))]
public class Enemy : Unit, IDamageble
{
    private TargetFinder _targetFinder;
    private PlayerDetector _playerDetector;

    public CheckPoint CurrentPoint { get; private set; }

    private void Awake()
    {
        SetComponents();
    }

    private void OnEnable()
    {
        _playerDetector.FacedWithPlayer += Attack;
        Health.AmountWasted += Destroy;
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        Vector2 direction = ((_targetFinder.Target.position.x - transform.position.x) * Vector2.right).normalized;
        Mover.Move(direction);
    }

    private void OnDisable()
    {
        _playerDetector.FacedWithPlayer -= Attack;
        Health.AmountWasted -= Destroy;
    }

    private void SetComponents()
    {
        Health = GetComponent<Health>();
        Mover = GetComponent<Mover>();
        _targetFinder = GetComponent<TargetFinder>();
        _playerDetector = GetComponent<PlayerDetector>();
    }

    private void Initialize()
    {
        Health.Initialize();
    }
}