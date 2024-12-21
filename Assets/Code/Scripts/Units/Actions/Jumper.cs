using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _forceJump = 500;

    private Rigidbody2D _rigidbody;

    public float DirectionByY => _rigidbody.velocity.y;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _rigidbody.AddForce(_forceJump * Vector2.up);
    }
}