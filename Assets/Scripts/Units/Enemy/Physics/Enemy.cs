using System;
using UnityEngine;

[RequireComponent(typeof(CollisionsEnemy))]
public class Enemy : Unit
{
    [SerializeField] private Way _way;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _damage = 10;

    private CollisionsEnemy _collisionsEnemy;
    private Transform _target;
    private float _health = 100;
    private float _minHealth = 0;

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
        ChooseCheckPoint(CurrentPoint);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        PastPositionByX = transform.position.x;
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

    private void Attack(Player player)
    {
        player.TakeDamage(_damage);
    }

    private void Move()
    {
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