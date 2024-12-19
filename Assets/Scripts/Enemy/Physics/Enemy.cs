using System;
using UnityEngine;

[RequireComponent(typeof(CollisionsEnemy))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Way _way;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxDistanceToPlayer;
    [SerializeField] private float _damage = 10;

    private CollisionsEnemy _collisionsEnemy;
    private Transform _target;
    private Vector2 _direction;
    private float _pastPositionByX;
    private float _health = 100;
    private float _minHealth = 0;

    public event Action PlayerEscaped;
    public event Action DamageTaken;

    public CheckPoint CurrentPoint { get; private set; }

    public float Direction => _pastPositionByX - transform.position.x;

    private void OnEnable()
    {
        _collisionsEnemy.PlayerDetected += SetTarget;
        _collisionsEnemy.FacedWithPlayer += Attack;
    }

    private void OnDisable()
    {
        _collisionsEnemy.PlayerDetected -= SetTarget;
        _collisionsEnemy.FacedWithPlayer -= Attack;
    }

    private void Awake()
    {
        _collisionsEnemy = GetComponent<CollisionsEnemy>();
    }

    private void Start()
    {
        SelectNextCheckPoint();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (_direction.sqrMagnitude < _maxDistanceToPlayer)
            return;

        _target = CurrentPoint.transform;
        PlayerEscaped?.Invoke();
    }

    public void SelectNextCheckPoint()
    {
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

        if (_health < 0)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        _pastPositionByX = transform.position.x;
        _direction = _target.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _moveSpeed * Time.fixedDeltaTime);
    }

    private void Attack(Player player)
    {
        player.TakeDamage(_damage);
    }

    private void SetTarget(Transform transform)
    {
        _target = transform;
    }
}