using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Way _way;

    private CheckPoint _currentPoint;
    private float _pastPositionByX;

    public float Direction => _pastPositionByX - transform.position.x;

    private void Start()
    {
        _currentPoint = _way.GetNextCheckPoint(_currentPoint);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CheckPoint point) && _currentPoint == point)
        {
            _currentPoint = _way.GetNextCheckPoint(_currentPoint);
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _pastPositionByX = transform.position.x;
        transform.position = Vector2.MoveTowards(transform.position, _currentPoint.transform.position, _moveSpeed * Time.deltaTime);
    }
}