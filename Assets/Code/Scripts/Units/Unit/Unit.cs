using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Health))]
public abstract class Unit : MonoBehaviour, IDamageble
{
    [SerializeField] protected float Damage = 10;

    public event Action<float> DamageTaken;

    public Mover Mover { get; protected set; }

    public Health Health { get; protected set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
        Mover = GetComponent<Mover>();
        OnAwake();
    }

    public void TakeDamage(float damage)
    {
        Health.TakeDamage(damage);
        DamageTaken?.Invoke(damage);
    }

    protected abstract void OnAwake();

    protected virtual void Attack(IDamageble unit)
    {
        unit.TakeDamage(Damage);
    }

    protected void Destroy()
    {
        Destroy(gameObject);
    }
}