using System;
using System.Collections;
using UnityEngine;

public class CollisionsEnemy : MonoBehaviour
{
    [SerializeField] private float _viewingRange = 10;
    [SerializeField] private float _viewingDelay = 0.2f;

    private Player _huntedPlayer;
    private bool _isSeePlayer;
    private WaitForSeconds _wait;
    private Vector2 _directionRay;
    private float _pastPositionByX;

    public event Action<Transform> PlayerDetected;
    public event Action PlayerEscaped;
    public event Action<Player> FacedWithPlayer;
    public event Action<CheckPoint> ComeToCheckPoint;

    private void Awake()
    {
        _wait = new WaitForSeconds(_viewingDelay);
    }

    private void Start()
    {
        StartCoroutine(HuntPlayer());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CheckPoint point))
            ComeToCheckPoint?.Invoke(point);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            FacedWithPlayer?.Invoke(player);
    }

    private void Update()
    {
        DetermineDirectionRay();
        _pastPositionByX = transform.position.x;
    }

    private IEnumerator HuntPlayer()
    {
        Player player = null;
        _isSeePlayer = false;

        while (_isSeePlayer == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _directionRay, _viewingRange);

            if (hit.collider)
                _isSeePlayer = hit.collider.gameObject.TryGetComponent(out player);

            yield return _wait;
        }

        _huntedPlayer = player;
        PlayerDetected?.Invoke(player.transform);
        StartCoroutine(CalculateSqrDistanceToPlayer());
    }

    private IEnumerator CalculateSqrDistanceToPlayer()
    {
        Vector2 directionToPlayer;
        _isSeePlayer = true;

        while (_isSeePlayer)
        {
            directionToPlayer = _huntedPlayer.transform.position - transform.position;
            _isSeePlayer = directionToPlayer.sqrMagnitude < _viewingRange * _viewingRange;
            yield return _wait;
        }

        PlayerEscaped?.Invoke();
        StartCoroutine(HuntPlayer());
    }

    private void DetermineDirectionRay()
    {
        if (transform.position.x - _pastPositionByX > 0)
            _directionRay = Vector2.right;
        else if (transform.position.x - _pastPositionByX < 0)
            _directionRay = Vector2.left;
    }
}