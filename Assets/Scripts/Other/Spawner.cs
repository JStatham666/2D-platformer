using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefabe;
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private int _poolCapacity = 4;
    [SerializeField] private int _poolMaxSize = 4;
    [SerializeField] private Transform[] _positions;

    private ObjectPool<Coin> _coins;

    private int _counter = -1;

    private void Awake()
    {
        _coins = new ObjectPool<Coin>(
        createFunc: () => Instantiate(_coinPrefabe),
        actionOnGet: (coin) => OnGet(coin),
        actionOnRelease: (coin) => coin.gameObject.SetActive(false),
        actionOnDestroy: (coin) => Destroy(coin.gameObject),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void Start() =>
        StartCoroutine(GetCoins());

    private IEnumerator GetCoins()
    {
        WaitForSeconds delay = new WaitForSeconds(_repeatRate);

        while (enabled)
        {
            _coins.Get();
            yield return delay;
        }
    }

    private void OnGet(Coin coin)
    {
        coin.Destroyed += ReleaseCoin;
        coin.transform.position = GetPosition();
    }

    private void ReleaseCoin(Coin coin)
    {
        coin.Destroyed -= ReleaseCoin;
        _coins.Release(coin);
    }

    private Vector2 GetPosition()
    {
        _counter++;

        if (_counter > _positions.Length - 1)
            _counter = 0;

        return new Vector2(
            _positions[_counter].position.x,
            _positions[_counter].position.y);
    }
}
