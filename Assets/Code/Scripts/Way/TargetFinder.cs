using UnityEngine;

[RequireComponent(typeof(CheckPointDetector), typeof(PlayerDetector))]
public class TargetFinder : MonoBehaviour
{
    [SerializeField] private Way _way;

    private CheckPointDetector _checkPointDetector;
    private PlayerDetector _playerDetector;

    public Transform Target { get; private set; }

    public CheckPoint CurrentPoint { get; private set; }

    private void Awake()
    {
        _checkPointDetector = GetComponent<CheckPointDetector>();
        _playerDetector = GetComponent<PlayerDetector>();
    }

    private void OnEnable()
    {
        _playerDetector.PlayerDetected += SetTarget;
        _playerDetector.PlayerEscaped += FollowWay;
        _checkPointDetector.ComeToCheckPoint += ChooseCheckPoint;
    }

    private void Start()
    {
        ChooseCheckPoint(CurrentPoint);
    }

    private void OnDisable()
    {
        _playerDetector.PlayerDetected -= SetTarget;
        _playerDetector.PlayerEscaped -= FollowWay;
        _checkPointDetector.ComeToCheckPoint -= ChooseCheckPoint;
    }

    private void ChooseCheckPoint(CheckPoint point)
    {
        if (CurrentPoint != point)
            return;

        CurrentPoint = _way.GetNextCheckPoint(CurrentPoint);
        Target = CurrentPoint.transform;
    }

    private void SetTarget(Transform transform)
    {
        Target = transform;
    }

    private void FollowWay()
    {
        Target = CurrentPoint.transform;
    }
}