using UnityEngine;

[RequireComponent(typeof(Player), typeof(Collector))]
public class HealthMeter : MonoBehaviour
{
    private Player _player;
    private Collector _collector;

    private void OnEnable()
    {
        _player.DamageTaken += DecreaseHealth;
        _collector.TreatmentTaken += IncreaseHealth;
    }

    private void OnDisable()
    {
        _player.DamageTaken -= DecreaseHealth;
        _collector.TreatmentTaken -= IncreaseHealth;
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _collector = GetComponent<Collector>();
    }

    private void IncreaseHealth(Treatment treatment)
    {
        if (treatment.AmountHealth <= 0)
            return;

        _player.Health.TakeTreatment(treatment.AmountHealth);
    }

    private void DecreaseHealth(float damage)
    {
        _player.Health.TakeDamage(damage);
        

        if (_player.Health.Amount > 0)
            return;

        Destroy(gameObject);
    }
}