using System;
using UnityEngine;

[RequireComponent(typeof(CollisionsEnemy))]
public class Enemy : Unit, IDamageble
{
    [SerializeField] private Way _way;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _damage = 10;

    private CollisionsEnemy _collisionsEnemy;
    private Transform _target;
    private Health _health = new Health();

    public event Action DamageTaken;

    public CheckPoint CurrentPoint { get; private set; }

    private void OnEnable()
    {
        _collisionsEnemy.PlayerDetected += SetTarget;
        _collisionsEnemy.PlayerEscaped += FollowWay;
        _collisionsEnemy.FacedWithPlayer += Attack;
        _collisionsEnemy.ComeToCheckPoint += ChooseCheckPoint;
    }

    private void OnDisable()
    {
        _collisionsEnemy.PlayerDetected -= SetTarget;
        _collisionsEnemy.PlayerEscaped -= FollowWay;
        _collisionsEnemy.FacedWithPlayer -= Attack;
        _collisionsEnemy.ComeToCheckPoint -= ChooseCheckPoint;
    }

    private void Awake()
    {
        _collisionsEnemy = GetComponent<CollisionsEnemy>();
    }

    private void Start()
    {
        _health.Initialize();
        ChooseCheckPoint(CurrentPoint);
    }

    private void Update()
    {
        Move();
    }

    private void ChooseCheckPoint(CheckPoint point)
    {
        if (CurrentPoint != point)
            return;

        CurrentPoint = _way.GetNextCheckPoint(CurrentPoint);
        _target = CurrentPoint.transform;
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

    private void Move()
    {
        PastPositionByX = transform.position.x;
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _moveSpeed * Time.fixedDeltaTime);
    }

    private void SetTarget(Transform transform)
    {
        _target = transform;
    }

    private void FollowWay()
    {
        _target = CurrentPoint.transform;
    }
}