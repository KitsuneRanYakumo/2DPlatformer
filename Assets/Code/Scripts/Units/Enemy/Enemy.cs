using System;
using UnityEngine;

[RequireComponent(typeof(TargetFinder), typeof(PlayerDetector))]
public class Enemy : Unit
{
    private TargetFinder _targetFinder;
    private PlayerDetector _playerDetector;

    protected override void OnAwake()
    {
        _targetFinder = GetComponent<TargetFinder>();
        _playerDetector = GetComponent<PlayerDetector>();
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

    private void Initialize()
    {
        Health.Initialize();
    }
}