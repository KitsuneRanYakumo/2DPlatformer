using UnityEngine;

[RequireComponent(typeof(Player), typeof(SpriteRenderer), typeof(UserInput))]
[RequireComponent(typeof(Animator))]
public class ControllerAnimatorPlayer : MonoBehaviour
{
    private readonly int _moveAnimation = Animator.StringToHash("IsMoving");
    private readonly int _fallAnimation = Animator.StringToHash("IsFalling");
    private readonly int _jumpTrigger = Animator.StringToHash("JumpTrigger");

    private Player _player;
    private UserInput _userInput;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _userInput = GetComponent<UserInput>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        DetermineSpriteFlipX();
        _animator.SetBool(_moveAnimation, _player.IsMoving);
        _animator.SetBool(_fallAnimation, _player.IsFalling);

        if (_player.IsGrounded && _userInput.IsPressedJumpButton)
            _animator.SetTrigger(_jumpTrigger);
    }

    private void DetermineSpriteFlipX()
    {
        if (_player.Direction.x > 0)
            _spriteRenderer.flipX = false;
        else if (_player.Direction.x < 0)
            _spriteRenderer.flipX = true;
    }
}