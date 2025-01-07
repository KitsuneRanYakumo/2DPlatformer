using System;
using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] private Coverage _coverage;
    [SerializeField] private float Duration = 6;
    [SerializeField] private float Cooldown = 4;

    protected float CurrentDuration;
    protected float CurrentCooldown;
    private Coroutine _coroutine;

    public event Action<float, float> DurationChanged;
    public event Action<float, float> CooldownChanged;
    
    public bool IsActive { get; private set; }

    private void Start()
    {
        IsActive = false;
    }

    public void Switch()
    {
        if (IsActive == false && CurrentCooldown <= 0)
            Activate();
        else if (IsActive)
            Deactivate();
    }

    protected virtual void Activate()
    {
        IsActive = true;
        _coverage.ChangeZone(IsActive);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(StartDuration());
    }

    protected virtual void Deactivate()
    {
        IsActive = false;
        _coverage.ChangeZone(IsActive);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(StartCooldown());
    }

    private IEnumerator StartDuration()
    {
        CurrentDuration = Duration;
        DurationChanged?.Invoke(CurrentDuration, Duration);

        while (CurrentDuration > 0)
        {
            CurrentDuration -= Time.deltaTime;
            DurationChanged?.Invoke(CurrentDuration, Duration);
            yield return null;
        }

        Deactivate();
    }

    private IEnumerator StartCooldown()
    {
        while (CurrentCooldown < Cooldown)
        {
            CurrentCooldown += Time.deltaTime;
            CooldownChanged?.Invoke(CurrentCooldown, Cooldown);
            yield return null;
        }

        CurrentCooldown = 0;
    }
}