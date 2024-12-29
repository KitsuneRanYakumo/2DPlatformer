using UnityEngine;

public class Treatment : Item
{
    [SerializeField] private float _amount = 25;

    public float Amount => _amount;
}