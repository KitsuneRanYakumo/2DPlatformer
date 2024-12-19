using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class CollisionsPlayer : MonoBehaviour
{
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private float _rayLength;
    [SerializeField] private float _delayReleaseRay = 1;

    private Player _player;
    private WaitForSeconds _wait;

    public event Action<Enemy> FacedWithEnemy;

    public bool IsTouchPlatform { get; private set; }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _wait = new WaitForSeconds(_delayReleaseRay);
    }

    private void Start()
    {
        StartCoroutine(TryTouchPlatform());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Item item))
            PickUpItem(item);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
            FacedWithEnemy?.Invoke(enemy);

        if (collision.gameObject.GetComponent<Enemy>())
            _player.Jump();
    }

    private IEnumerator TryTouchPlatform()
    {
        while (enabled)
        {
            RaycastHit2D hit = Physics2D.Raycast(_raycastPoint.position, Vector3.down, _rayLength);

            if (hit.collider)
            {
                if (hit.collider.GetComponent<Platform>() != null)
                    IsTouchPlatform = true;
                else
                    IsTouchPlatform = false;
            }
            else
            {
                IsTouchPlatform = false;
            }

            yield return _wait;
        }
    }

    private void PickUpItem(Item item)
    {
        switch (item)
        {
            case Coin coin:
                coin.BecomeTaken();
                _player.IncreaseAmountCoins();
                return;
            case Treatment treatment:
                treatment.BecomeTaken();
                _player.Heal(treatment.AmountHealth);
                return;
        }
    }
}