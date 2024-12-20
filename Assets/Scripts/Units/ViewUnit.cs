using UnityEngine;

public class ViewUnit : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    private float _rightRotationByY = 0;
    private float _leftRotationByY = 180;

    private void Update()
    {
        DetermineRotationByY();
    }

    private void DetermineRotationByY()
    {
        if (_unit.Direction > 0)
            transform.rotation = Quaternion.Euler(_rightRotationByY * Vector2.up);
        else if (_unit.Direction < 0)
            transform.rotation = Quaternion.Euler(_leftRotationByY * Vector2.up);
    }
}