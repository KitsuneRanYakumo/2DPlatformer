using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerCoins : MonoBehaviour
{
    [SerializeField] private Coin _prefabCoin;
    [SerializeField] private int _defaultAmountCoinsInPool;
    [SerializeField] private float _delaySpawn = 2;
    
    private ObjectPool<Coin> _poolCoins;
    private int _maxSizePool;
    private List<PointForCoins> _vacantPoints;
    private PointForCoins _currentPoint;

    private WaitForSecondsRealtime _wait;
    private bool _isSpawning;
    
    private void Awake()
    {
        _isSpawning = true;
        _wait = new WaitForSecondsRealtime(_delaySpawn);

        _vacantPoints = GetComponentsInChildren<PointForCoins>().ToList();
        _maxSizePool = _vacantPoints.Count;

        if (_defaultAmountCoinsInPool > _maxSizePool)
            _defaultAmountCoinsInPool = _maxSizePool;

        _poolCoins = new ObjectPool<Coin>(
            createFunc: CreateForPool,
            actionOnGet: OnGetFromPool,
            actionOnRelease: OnReleaseInPool,
            actionOnDestroy: Destroy,
            collectionCheck: true,
            defaultCapacity: _defaultAmountCoinsInPool,
            maxSize: _maxSizePool);
    }

    private void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    private Coin CreateForPool()
    {
        return Instantiate(_prefabCoin);
    }

    private void OnGetFromPool(Coin coin)
    {
        DetermineCurrentPointSpawn();
        coin.Initialize(_currentPoint);
        coin.gameObject.SetActive(true);
        coin.WasPickedUp += TakeCoin;
        _vacantPoints.Remove(_currentPoint);
    }

    private void OnReleaseInPool(Coin coin)
    {
        coin.gameObject.SetActive(false);
        coin.WasPickedUp -= TakeCoin;
    }

    private void TakeCoin(Coin coin, PointForCoins point)
    {
        _poolCoins.Release(coin);
        _vacantPoints.Add(point);
    }

    private void DetermineCurrentPointSpawn()
    {
        _currentPoint = _vacantPoints[Random.Range(0, _vacantPoints.Count)];
    }

    private IEnumerator SpawnCoins()
    {
        while (_isSpawning)
        {
            if (_vacantPoints.Count > 0)
            {
                _poolCoins.Get();
            }

            yield return _wait;
        }
    }
}