using System;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public event Action<Enemy> FacedWithEnemy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
            FacedWithEnemy?.Invoke(enemy);
    }
}