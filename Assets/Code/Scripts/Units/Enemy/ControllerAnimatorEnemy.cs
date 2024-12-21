using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ControllerAnimatorEnemy : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private readonly int _takeDamageTrigger = Animator.StringToHash("TakeDamageTrigger");

    private Animator _animator;

    private void OnEnable()
    {
        _enemy.DamageTaken += SetDamageTrigger;
    }

    private void OnDisable()
    {
        _enemy.DamageTaken -= SetDamageTrigger;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void SetDamageTrigger()
    {
        _animator.SetTrigger(_takeDamageTrigger);
    }
}