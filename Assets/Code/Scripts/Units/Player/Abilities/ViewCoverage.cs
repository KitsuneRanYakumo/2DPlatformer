using UnityEngine;

public class ViewCoverage : MonoBehaviour
{
    [SerializeField] private Coverage _coverage;

    private void OnEnable()
    {
        _coverage.SizeChanged += ChangeSize;
    }

    private void OnDisable()
    {
        _coverage.SizeChanged -= ChangeSize;
    }

    private void ChangeSize(float size)
    {
        transform.localScale = size * Vector3.one;
    }
}