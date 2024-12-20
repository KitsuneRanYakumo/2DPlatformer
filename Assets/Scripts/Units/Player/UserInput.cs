using UnityEngine;

public class UserInput : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    [SerializeField] private KeyCode _jumpButton = KeyCode.Space;

    public float DirectionHorizontalMovement => Input.GetAxis(Horizontal);

    public bool IsPressedJumpButton => Input.GetKeyUp(_jumpButton);
}