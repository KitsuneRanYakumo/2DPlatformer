using UnityEngine;

[RequireComponent(typeof(UserInput), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _forceJump;

    private UserInput _userInput;
    private Rigidbody2D _rigidbody;
    private int _amountCoins;

    private void Awake()
    {
        _userInput = GetComponent<UserInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            PickUpCoin(coin);
        }
    }

    private void Move()
    {
        Vector3 direction = new Vector3(_userInput.HorizontalMovement, 0, 0);

        transform.Translate(_moveSpeed * direction);
    }

    private void Jump()
    {
        if (_userInput.IsPressedJumpButton)
        {
            _rigidbody.AddForce(_forceJump * Vector2.up);
        }
    }

    private void PickUpCoin(Coin coin)
    {
        coin.BecomeTaken();
        _amountCoins++;
    }
}