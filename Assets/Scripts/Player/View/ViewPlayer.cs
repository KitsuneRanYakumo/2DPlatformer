using UnityEngine;

public class ViewPlayer : MonoBehaviour
{
    [SerializeField] private Player _player;

    private float _rightRotationByY = 0;
    private float _leftRotationByY = 180;

    private void Update()
    {
        DetermineRotationByY();
    }

    private void DetermineRotationByY()
    {
        if (_player.Direction > 0)
            transform.rotation = Quaternion.Euler(_rightRotationByY * Vector2.up);
        else if (_player.Direction < 0)
            transform.rotation = Quaternion.Euler(_leftRotationByY * Vector2.up);
    }
}