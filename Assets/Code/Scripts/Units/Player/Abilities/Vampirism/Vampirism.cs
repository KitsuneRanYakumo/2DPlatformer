using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CalculatorDistancesToTarget))]
public class Vampirism : Ability
{
    [SerializeField] private float _recoverableAmount = 3;
    
    private CalculatorDistancesToTarget _calculatorDistances;

    public event Action<float> Occurred;

    private void Awake()
    {
        _calculatorDistances = GetComponent<CalculatorDistancesToTarget>();
    }

    protected override void Activate()
    {
        base.Activate();
        StartCoroutine(StealLife());
    }

    private IEnumerator StealLife()
    {
        while (IsActive)
        {
            Enemy nearestEnemy = _calculatorDistances.NearestEnemy;

            if (nearestEnemy != null)
            {
                float currentHealth = nearestEnemy.Health.Amount;
                nearestEnemy.TakeDamage(_recoverableAmount);
                Occurred?.Invoke(currentHealth - nearestEnemy.Health.Amount);
            }

            yield return null;
        }
    }
}