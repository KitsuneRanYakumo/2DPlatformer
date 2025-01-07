using System;
using System.Collections;
using UnityEngine;

public class Coverage : MonoBehaviour
{
    [SerializeField] private float _sizeZone = 5;
    [SerializeField] private float _changeSpeedZone = 0.1f;

    private Coroutine _coroutine;
    private float _target;

    public event Action<float> SizeChanged;

    public void ChangeZone(bool isIncreased)
    {
        if (isIncreased)
            _target = _sizeZone;
        else
            _target = 0;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeSize());
    }

    private IEnumerator ChangeSize()
    {
        while (transform.localScale != _target * Vector3.one)
        {
            transform.localScale = Mathf.MoveTowards(transform.localScale.x, _target, _changeSpeedZone) * Vector3.one;
            SizeChanged?.Invoke(transform.localScale.x);
            yield return null;
        }
    }
}