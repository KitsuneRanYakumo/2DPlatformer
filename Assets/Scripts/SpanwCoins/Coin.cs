using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private PointForCoins _point;

    public event Action<Coin, PointForCoins> WasPickedUp;

    public void Initialize(PointForCoins point)
    {
        _point = point;
        transform.position = point.transform.position;
    }

    public void BecomeTaken()
    {
        WasPickedUp?.Invoke(this, _point);
    }
}