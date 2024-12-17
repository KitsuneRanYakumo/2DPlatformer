using UnityEngine;

[RequireComponent(typeof(UserInput), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _forceJump = 3;
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private float _rayLength;

    private UserInput _userInput;
    private Rigidbody2D _rigidbody;
    private int _amountCoins;

    public Vector2 Direction {  get; private set; }

    public bool IsMoving => Direction.x != 0;

    public bool IsFalling => _rigidbody.velocity.y < 0;

    public bool IsGrounded => TryTouchPlatform();

    private void Awake()
    {
        _userInput = GetComponent<UserInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();

        if (IsGrounded && _userInput.IsPressedJumpButton)
            Jump();
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Coin coin))
            PickUpCoin(coin);
    }

    private void Move()
    {
        Direction = new Vector2(_userInput.HorizontalMovement, 0);
        transform.Translate(_moveSpeed * Time.deltaTime * Direction);
    }

    private void Jump()
    {
        _rigidbody.AddForce(_forceJump * Vector2.up);
    }

    private bool TryTouchPlatform()
    {
        RaycastHit2D hit = Physics2D.Raycast(_raycastPoint.position, Vector3.down, _rayLength);

        if (hit.collider == null)
            return false;

        if (hit.collider.GetComponent<Platform>() != null)
            return true;

        return false;
    }

    private void PickUpCoin(Coin coin)
    {
        coin.BecomeTaken();
        _amountCoins++;
        Log();
    }

    private void Log()
    {
        Debug.Log($"Количество монет - {_amountCoins}.");
    }
}