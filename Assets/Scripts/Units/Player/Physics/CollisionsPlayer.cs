using System;
using System.Collections;
using UnityEngine;

public class CollisionsPlayer : MonoBehaviour
{
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private float _rayLength;
    [SerializeField] private float _delayReleaseRay = 1;

    private WaitForSeconds _wait;

    public event Action<Enemy> FacedWithEnemy;
    public event Action<Item> ItemFound;

    public bool IsTouchPlatform { get; private set; }

    private void Awake()
    {
        _wait = new WaitForSeconds(_delayReleaseRay);
    }

    private void Start()
    {
        StartCoroutine(TryTouchPlatform());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Item item))
            ItemFound?.Invoke(item);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
            FacedWithEnemy?.Invoke(enemy);
    }

    private IEnumerator TryTouchPlatform()
    {
        while (enabled)
        {
            RaycastHit2D hit = Physics2D.Raycast(_raycastPoint.position, Vector3.down, _rayLength);

            if (hit.collider)
                IsTouchPlatform = hit.collider.GetComponent<Platform>() != null;
            else
                IsTouchPlatform = false;

            yield return _wait;
        }
    }
}