using System.Collections;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private float _rayLength = 1.02f;
    [SerializeField] private float _delayReleaseRay = 0.2f;

    private WaitForSeconds _wait;

    public bool IsTouchPlatform { get; private set; }

    private void Awake()
    {
        _wait = new WaitForSeconds(_delayReleaseRay);
    }

    private void Start()
    {
        StartCoroutine(TryTouchPlatform());
    }

    private IEnumerator TryTouchPlatform()
    {
        while (enabled)
        {
            RaycastHit2D hit = Physics2D.Raycast(_raycastPoint.position, Vector3.down, _rayLength);

            if (hit.collider)
                IsTouchPlatform = hit.collider.GetComponent<Platform>() != null;
            else
                IsTouchPlatform = false;

            Debug.Log(IsTouchPlatform);

            yield return _wait;
        }
    }
}