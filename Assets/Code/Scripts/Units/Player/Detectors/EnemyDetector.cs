using System;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public event Action<Enemy> FacedWithEnemy;
    public event Action<Enemy> EnemyEnteredZone;
    public event Action<Enemy> EnemyEscapedZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
            EnemyEnteredZone?.Invoke(enemy);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
            EnemyEscapedZone?.Invoke(enemy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
            FacedWithEnemy?.Invoke(enemy);
    }
}