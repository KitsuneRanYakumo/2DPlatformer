using UnityEngine;

public class Way : MonoBehaviour
{
    private CheckPoint[] _checkPoints;

    private void Awake()
    {
        _checkPoints = GetComponentsInChildren<CheckPoint>();
    }

    public CheckPoint GetNextCheckPoint(CheckPoint currentCheckPoint)
    {
        if (currentCheckPoint == null)
            return _checkPoints[0];

        for (int i = 0; i < _checkPoints.Length; i++)
        {
            if (currentCheckPoint == _checkPoints[i])
            {
                int numberNextCheckPoint = i++ % _checkPoints.Length;
                return _checkPoints[numberNextCheckPoint];
            }
        }

        return null;
    }
}