using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    private PointSpawn _point;

    public event Action<Item, PointSpawn> WasPickedUp;

    public void Initialize(PointSpawn point)
    {
        _point = point;
        transform.position = point.transform.position;
    }

    public void BecomeTaken()
    {
        WasPickedUp?.Invoke(this, _point);
    }
}