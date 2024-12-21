using System;
using UnityEngine;

[RequireComponent(typeof(ControllerTarget), typeof(PlayerDetector))]
public class Enemy : Unit, IDamageble
{
    

    private ControllerTarget _controllerTarget;
    private PlayerDetector _playerDetector;

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
        _controllerTarget = GetComponent<ControllerTarget>();
        _playerDetector = GetComponent<PlayerDetector>();
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

    private void Initialize()
    {
        Health = new Health();
        Health.Initialize();
    }
}