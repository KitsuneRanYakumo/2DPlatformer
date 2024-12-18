using UnityEngine;

public class ViewEnemy : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private float _rightRotationByY = 0;
    private float _leftRotationByY = 180;

    private void Update()
    {
        DetermineRotationByY();
    }

    private void DetermineRotationByY()
    {
        if (_enemy.Direction > 0)
            transform.rotation = Quaternion.Euler(_rightRotationByY * Vector2.up);
        else if (_enemy.Direction < 0)
            transform.rotation = Quaternion.Euler(_leftRotationByY * Vector2.up);
    }
}