using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerItems : MonoBehaviour
{
    [SerializeField] private Item _prefabItem;
    [SerializeField] private int _defaultAmountCoinsInPool;
    [SerializeField] private float _delaySpawn = 2;
    
    private ObjectPool<Item> _poolCoins;
    private int _maxSizePool;
    private List<PointSpawn> _vacantPoints;
    private PointSpawn _currentPoint;

    private WaitForSecondsRealtime _wait;
    private bool _isSpawning;
    
    private void Awake()
    {
        _isSpawning = true;
        _wait = new WaitForSecondsRealtime(_delaySpawn);

        _vacantPoints = GetComponentsInChildren<PointSpawn>().ToList();
        _maxSizePool = _vacantPoints.Count;

        if (_defaultAmountCoinsInPool > _maxSizePool)
            _defaultAmountCoinsInPool = _maxSizePool;
    }

    private void Start()
    {
        _poolCoins = new ObjectPool<Item>(
            createFunc: CreateForPool,
            actionOnGet: OnGetFromPool,
            actionOnRelease: OnReleaseInPool,
            actionOnDestroy: Destroy,
            collectionCheck: true,
            defaultCapacity: _defaultAmountCoinsInPool,
            maxSize: _maxSizePool);

        StartCoroutine(SpawnItems());
    }

    private Item CreateForPool()
    {
        return Instantiate(_prefabItem);
    }

    private void OnGetFromPool(Item item)
    {
        DetermineCurrentPointSpawn();
        item.Initialize(_currentPoint);
        item.gameObject.SetActive(true);
        item.WasPickedUp += TakeCoin;
        _vacantPoints.Remove(_currentPoint);
    }

    private void OnReleaseInPool(Item item)
    {
        item.gameObject.SetActive(false);
        item.WasPickedUp -= TakeCoin;
    }

    private void TakeCoin(Item item, PointSpawn point)
    {
        _poolCoins.Release(item);
        _vacantPoints.Add(point);
    }

    private void DetermineCurrentPointSpawn()
    {
        _currentPoint = _vacantPoints[Random.Range(0, _vacantPoints.Count)];
    }

    private IEnumerator SpawnItems()
    {
        while (_isSpawning)
        {
            if (_vacantPoints.Count > 0)
                _poolCoins.Get();

            yield return _wait;
        }
    }
}