using UnityEngine;

[RequireComponent(typeof(Unit), typeof(Collector))]
public class HealthMeter : MonoBehaviour
{
    private Unit _unit;
    private Collector _collector;

    private void OnEnable()
    {
        _unit.DamageTaken += DecreaseHealth;
        _collector.TreatmentTaken += IncreaseHealth;
    }

    private void OnDisable()
    {
        _unit.DamageTaken -= DecreaseHealth;
        _collector.TreatmentTaken -= IncreaseHealth;
    }

    private void Awake()
    {
        _unit = GetComponent<Player>();
        _collector = GetComponent<Collector>();
    }

    private void IncreaseHealth(Treatment treatment)
    {
        if (treatment.AmountHealth <= 0)
            return;

        _unit.Health.TakeTreatment(treatment.AmountHealth);
    }

    private void DecreaseHealth(float damage)
    {
        _unit.Health.TakeDamage(damage);
        
        if (_unit.Health.Amount > 0)
            return;

        Destroy(gameObject);
    }
}