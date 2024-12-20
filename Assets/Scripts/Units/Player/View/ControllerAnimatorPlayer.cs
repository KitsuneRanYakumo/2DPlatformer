using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ControllerAnimatorPlayer : MonoBehaviour
{
    private readonly int _moveAnimation = Animator.StringToHash("IsMoving");
    private readonly int _fallAnimation = Animator.StringToHash("IsFalling");
    private readonly int _jumpTrigger = Animator.StringToHash("JumpTrigger");
    private readonly int _takeDamageTrigger = Animator.StringToHash("TakeDamageTrigger");

    [SerializeField] private Player _player;
    [SerializeField] private UserInput _userInput;

    private Animator _animator;

    private void OnEnable()
    {
        _player.DamageTaken += SetDamageTrigger;
    }

    private void OnDisable()
    {
        _player.DamageTaken -= SetDamageTrigger;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(_moveAnimation, _player.IsMoving);
        _animator.SetBool(_fallAnimation, _player.IsFalling);

        if (_player.IsGrounded == false)
            return;

        if (_userInput.IsPressedJumpButton == false)
            return;

        _animator.SetTrigger(_jumpTrigger);
    }

    private void SetDamageTrigger()
    {
        _animator.SetTrigger(_takeDamageTrigger);
    }
}