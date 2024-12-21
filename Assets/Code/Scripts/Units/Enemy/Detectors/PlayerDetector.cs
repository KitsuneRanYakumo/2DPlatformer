using System;
using System.Collections;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private float _viewingRange = 10;
    [SerializeField] private float _viewingDelay = 0.2f;

    private Player _huntedPlayer;
    private bool _canSeePlayer;
    private WaitForSeconds _wait;
    private Vector2 _directionRay;
    private float _pastPositionByX;

    public event Action<Transform> PlayerDetected;
    public event Action PlayerEscaped;
    public event Action<Player> FacedWithPlayer;

    private void Awake()
    {
        _wait = new WaitForSeconds(_viewingDelay);
    }

    private void Start()
    {
        _pastPositionByX = transform.position.x;
        StartCoroutine(HuntPlayer());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            FacedWithPlayer?.Invoke(player);
    }

    private IEnumerator HuntPlayer()
    {
        Player player = null;
        _canSeePlayer = false;

        while (_canSeePlayer == false)
        {
            DetermineDirectionRay();
            _pastPositionByX = transform.position.x;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _directionRay, _viewingRange);

            if (hit.collider)
                _canSeePlayer = hit.collider.gameObject.TryGetComponent(out player);

            yield return _wait;
        }

        _huntedPlayer = player;
        PlayerDetected?.Invoke(player.transform);
        StartCoroutine(CalculateSqrDistanceToPlayer());
    }

    private IEnumerator CalculateSqrDistanceToPlayer()
    {
        Vector2 directionToPlayer;
        _canSeePlayer = true;

        while (_canSeePlayer)
        {
            directionToPlayer = _huntedPlayer.transform.position - transform.position;
            _canSeePlayer = directionToPlayer.sqrMagnitude < _viewingRange * _viewingRange;
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