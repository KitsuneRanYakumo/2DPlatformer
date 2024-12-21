using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8;

    public float DirectionByX { get; private set; }

    public void Move(Vector2 direction)
    {
        direction = _moveSpeed * Time.deltaTime * direction;
        DirectionByX = direction.x;
        transform.Translate(direction);
    }
}