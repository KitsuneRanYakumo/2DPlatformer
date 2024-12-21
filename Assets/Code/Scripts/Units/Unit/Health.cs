using System;
using UnityEngine;

[Serializable]
public class Health
{
    private float _minAmount = 0;
    private float _maxAmount = 100;

    public float Amount { get; private set; }

    public void Initialize()
    {
        Amount = _maxAmount;
    }

    public void TakeTreatment(float amount)
    {
        if (amount < 0)
            return;

        Amount = Mathf.MoveTowards(Amount, _maxAmount, amount);
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        Amount = Mathf.MoveTowards(Amount, _minAmount, damage);
    }
}