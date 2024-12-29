using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Health))]
public class Unit : MonoBehaviour
{
    [SerializeField] protected float Damage = 10;

    public event Action<float> DamageTaken;

    public Mover Mover { get; protected set; }

    public Health Health { get; protected set; }

    public void TakeDamage(float damage)
    {
        Health.TakeDamage(damage);
        DamageTaken?.Invoke(damage);
    }

    protected virtual void Attack(IDamageble unit)
    {
        unit.TakeDamage(Damage);
    }

    protected void Destroy()
    {
        Destroy(gameObject);
    }
}