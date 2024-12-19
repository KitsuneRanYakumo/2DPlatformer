using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class CollisionsEnemy : MonoBehaviour
{
    [SerializeField] private float _viewingRange = 10;
    [SerializeField] private float _viewingDelay = 0.2f;

    private Enemy _enemy;
    private WaitForSeconds _wait;
    private bool _isSeePlayer = false;
    private Vector2 _directionRay;

    public event Action<Transform> PlayerDetected;
    public event Action<Player> FacedWithPlayer;

    private void OnEnable()
    {
        _enemy.PlayerEscaped += StartHuntPlayer;
    }

    private void OnDisable()
    {
        _enemy.PlayerEscaped -= StartHuntPlayer;
    }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _wait = new WaitForSeconds(_viewingDelay);
    }

    private void Start()
    {
        StartCoroutine(HuntPlayer());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CheckPoint point) && _enemy.CurrentPoint == point)
            _enemy.SelectNextCheckPoint();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            FacedWithPlayer?.Invoke(player);
    }

    private void StartHuntPlayer()
    {
        StartCoroutine(HuntPlayer());
    }

    private IEnumerator HuntPlayer()
    {
        _isSeePlayer = false;

        while (_isSeePlayer == false)
        {
            DetermineDirectionRay();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _directionRay, _viewingRange);

            if (hit.collider)
            {
                if (hit.collider.gameObject.TryGetComponent(out Player player))
                {
                    PlayerDetected?.Invoke(player.transform);
                    _isSeePlayer = true;
                }
            }

            yield return _wait;
        }
    }

    private void DetermineDirectionRay()
    {
        if (_enemy.Direction > 0)
            _directionRay = Vector2.left;
        else if (_enemy.Direction < 0)
            _directionRay = Vector2.right;
    }
}