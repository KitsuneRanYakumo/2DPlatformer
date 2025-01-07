using UnityEngine;
using UnityEngine.UI;

public class ViewTimeAbility : MonoBehaviour
{
    [SerializeField] private Ability _ability;
    [SerializeField] private Slider _timeBar;

    private void OnEnable()
    {
        _ability.DurationChanged += ChangeValueBar;
        _ability.CooldownChanged += ChangeValueBar;
    }

    private void OnDisable()
    {
        _ability.DurationChanged -= ChangeValueBar;
        _ability.CooldownChanged -= ChangeValueBar;
    }

    private void ChangeValueBar(float value, float maxValue)
    {
        _timeBar.maxValue = maxValue;
        _timeBar.value = value;
    }
}