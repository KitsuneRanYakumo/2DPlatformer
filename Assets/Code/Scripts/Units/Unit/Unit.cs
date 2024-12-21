using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Unit : MonoBehaviour
{
    private float _damage = 10;

    public Mover Mover { get; protected set; }

    public Health Health { get; protected set; }

    public event Action<float> DamageTaken;

    public void TakeDamage(float damage)
    {
        DamageTaken?.Invoke(damage);
    }

    protected virtual void Attack(IDamageble unit)
    {
        unit.TakeDamage(_damage);
    }
}