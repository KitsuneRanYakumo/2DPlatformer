using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Way _way;

    private CheckPoint _currentPoint;

    private void Start()
    {
        _currentPoint = _way.GetNextCheckPoint(_currentPoint);
    }

    private void FixedUpdate()
    {
        if (transform.position == _currentPoint.transform.position)
            _currentPoint = _way.GetNextCheckPoint(_currentPoint);

        transform.position = Vector3.MoveTowards(transform.position, _currentPoint.transform.position, _moveSpeed);
    }
}