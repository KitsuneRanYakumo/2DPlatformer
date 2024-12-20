using UnityEngine;

public class Health
{
    public float Amount { get; private set; }

    public float MinAmount { get; private set; } = 0;

    public float MaxAmount {  get; private set; } = 100;

    public void Initialize()
    {
        Amount = MaxAmount;
    }

    public void Heal(float amount)
    {
        if (amount < 0)
            return;

        Amount = Mathf.MoveTowards(Amount, MaxAmount, amount);
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        Amount = Mathf.MoveTowards(Amount, MinAmount, damage);
    }
}