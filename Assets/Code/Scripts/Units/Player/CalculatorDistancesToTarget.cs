using System.Collections.Generic;
using UnityEngine;

public class CalculatorDistancesToTarget : MonoBehaviour
{
    [SerializeField] private EnemyDetector _enemyDetector;

    private List<Enemy> _enemiesInZone;

    public Enemy NearestEnemy { get; private set; }

    private void OnEnable()
    {
        _enemyDetector.EnemyEnteredZone += AddEnemy;
        _enemyDetector.EnemyEscapedZone += RemoveEnemy;
    }

    private void Start()
    {
        _enemiesInZone = new List<Enemy>();
    }

    private void Update()
    {
        FindNearestEnemy();
    }

    private void OnDisable()
    {
        _enemyDetector.EnemyEnteredZone -= AddEnemy;
        _enemyDetector.EnemyEscapedZone -= RemoveEnemy;
    }

    private void AddEnemy(Enemy enemy)
    {
        _enemiesInZone.Add(enemy);
    }

    private void RemoveEnemy(Enemy enemy)
    {
        if (enemy != null && _enemiesInZone.Contains(enemy))
            _enemiesInZone.Remove(enemy);
    }

    private void FindNearestEnemy()
    {
        if (_enemiesInZone.Count > 0)
        {
            NearestEnemy = _enemiesInZone[0];

            for (int i = 1; i < _enemiesInZone.Count; i++)
            {
                Vector3 directionToNearestEnemy = NearestEnemy.transform.position - transform.position;
                Vector3 directionToEnemy = _enemiesInZone[i].transform.position - transform.position;

                if (directionToNearestEnemy.sqrMagnitude > directionToEnemy.sqrMagnitude)
                    NearestEnemy = _enemiesInZone[i];
            }
        }
        else
        {
            NearestEnemy = null;
        }
    }
}