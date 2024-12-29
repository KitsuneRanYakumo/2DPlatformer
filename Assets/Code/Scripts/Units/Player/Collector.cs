using System;
using UnityEngine;

[RequireComponent(typeof(ItemsDetector))]
public class Collector : MonoBehaviour
{
    private ItemsDetector _itemsDetector;

    public event Action<float> TreatmentTaken;
    public event Action CoinTaken;

    private void Awake()
    {
        _itemsDetector = GetComponent<ItemsDetector>();
    }

    private void OnEnable()
    {
        _itemsDetector.ItemFound += PickUpItem;
    }

    private void OnDisable()
    {
        _itemsDetector.ItemFound -= PickUpItem;
    }

    public void PickUpItem(Item item)
    {
        switch (item)
        {
            case Coin coin:
                TakeCoins(coin);
                return;

            case Treatment treatment:
                TakeTreatment(treatment);
                return;
        }
    }

    private void TakeTreatment(Treatment treatment)
    {
        treatment.BecomeTaken();
        TreatmentTaken?.Invoke(treatment.Amount);
    }

    private void TakeCoins(Coin coin)
    {
        coin.BecomeTaken();
        CoinTaken?.Invoke();
    }
}