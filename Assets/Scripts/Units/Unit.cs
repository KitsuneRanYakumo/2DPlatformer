using UnityEngine;

public class Unit : MonoBehaviour
{
    protected float PastPositionByX;

    public float Direction => transform.position.x - PastPositionByX;
}