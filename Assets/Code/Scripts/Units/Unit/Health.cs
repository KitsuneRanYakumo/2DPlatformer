using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _minAmount = 0;
    [SerializeField] private float _maxAmount = 100;

    private float _amount;

    public event Action<float, float> AmountChanged;
    public event Action AmountWasted;

    public void Initialize()
    {
        _amount = _maxAmount;
        AmountChanged?.Invoke(_amount, _maxAmount);
    }

    public void TakeTreatment(float treatment)
    {
        if (treatment < 0)
            return;

        _amount = Mathf.MoveTowards(_amount, _maxAmount, treatment);
        AmountChanged?.Invoke(_amount, _maxAmount);
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        _amount = Mathf.MoveTowards(_amount, _minAmount, damage);
        AmountChanged?.Invoke(_amount, _maxAmount);

        if (_amount > 0)
            return;

        AmountWasted?.Invoke();
    }
}