using System;
using UnityEngine;

public class CheckPointDetector : MonoBehaviour
{
    public event Action<CheckPoint> ComeToCheckPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CheckPoint point))
            ComeToCheckPoint?.Invoke(point);
    }
}