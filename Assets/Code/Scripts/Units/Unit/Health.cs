using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _minAmount = 0;
    [SerializeField] private float _maxAmount = 100;

    public event Action<float, float> AmountChanged;
    public event Action AmountWasted;

    public float Amount { get; private set; }

    public void Initialize()
    {
        Amount = _maxAmount;
        AmountChanged?.Invoke(Amount, _maxAmount);
    }

    public void TakeTreatment(float treatment)
    {
        if (treatment < 0)
            return;

        Amount = Mathf.MoveTowards(Amount, _maxAmount, treatment);
        AmountChanged?.Invoke(Amount, _maxAmount);
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        Amount = Mathf.MoveTowards(Amount, _minAmount, damage);
        AmountChanged?.Invoke(Amount, _maxAmount);

        if (Amount > 0)
            return;

        AmountWasted?.Invoke();
    }
}