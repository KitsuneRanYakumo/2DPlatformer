using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] private KeyCode _jumpButton = KeyCode.Space;

    private const string Horizontal = "Horizontal";

    public float DirectionHorizontalMovement => Input.GetAxis(Horizontal);

    public bool IsPressedJumpButton => Input.GetKeyUp(_jumpButton);
}