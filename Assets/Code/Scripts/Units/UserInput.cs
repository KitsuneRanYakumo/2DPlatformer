using UnityEngine;

public class UserInput : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    [SerializeField] private KeyCode _jumpButton = KeyCode.Space;
    [SerializeField] private KeyCode _vampirismButton = KeyCode.V;

    public float DirectionHorizontalMovement => Input.GetAxis(Horizontal);

    public bool IsPressedJumpButton => Input.GetKeyUp(_jumpButton);

    public bool IsPressedVampirismButton => Input.GetKeyUp(_vampirismButton);
}