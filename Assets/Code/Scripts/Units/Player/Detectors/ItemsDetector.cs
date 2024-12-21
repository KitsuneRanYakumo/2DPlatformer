using System;
using UnityEngine;

public class ItemsDetector : MonoBehaviour
{
    public event Action<Item> ItemFound;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Item item))
            ItemFound?.Invoke(item);
    }
}