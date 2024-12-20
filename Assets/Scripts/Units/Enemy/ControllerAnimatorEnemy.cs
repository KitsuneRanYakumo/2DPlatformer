using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ControllerAnimatorEnemy : MonoBehaviour
{
    private readonly int _takeDamageTrigger = Animator.StringToHash("TakeDamageTrigger");

    [SerializeField] private Enemy _enemy;

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