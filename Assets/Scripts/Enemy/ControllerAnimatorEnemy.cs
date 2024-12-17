using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ControllerAnimatorEnemy : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private float _pastPositionX;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        DetermineSpriteFlipX();
    }

    private void DetermineSpriteFlipX()
    {
        if (_pastPositionX - transform.position.x > 0)
            _sprite.flipX = false;
        else if (_pastPositionX - transform.position.x < 0)
            _sprite.flipX = true;

        _pastPositionX = transform.position.x;
    }
}