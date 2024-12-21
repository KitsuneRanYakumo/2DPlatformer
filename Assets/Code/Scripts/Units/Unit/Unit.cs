using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Unit : MonoBehaviour
{
    public Mover Mover { get; protected set; }
}