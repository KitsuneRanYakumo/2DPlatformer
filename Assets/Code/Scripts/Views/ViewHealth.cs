using UnityEngine;

public abstract class ViewHealth : MonoBehaviour
{
    [SerializeField] protected Health Health;

    private void OnEnable()
    {
        Health.AmountChanged += DisplayState;
    }

    private void OnDisable()
    {
        Health.AmountChanged -= DisplayState;
    }

    protected abstract void DisplayState(float amount, float maxAmount);
}