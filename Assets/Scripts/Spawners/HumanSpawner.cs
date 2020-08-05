using System.Collections;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private float _spawnCooldown = 12f;

    private ObjectPooler _objectPooler;
    private Coroutine _spawnerCoroutine;

    private void Awake()
    {
        GameManager.OnGameStarted += StartSpawner;
        GameManager.OnGameEnded += StopSpawner;
    }

    private void Start() => _objectPooler = ObjectPooler.Instance;

    private void StartSpawner() => _spawnerCoroutine = StartCoroutine(SpawnerCoroutine());
    private void StopSpawner() => StopCoroutine(_spawnerCoroutine);

    private void SpawnHuman()
    {
        Vector3 position = OffScreenCalculator.GetRandomPositionFromOrigin(_player, _spawnDistance);
        _objectPooler.SpawnFromPool(ObjectPooler.ObjectType.Human, position, Quaternion.identity);
    }

    IEnumerator SpawnerCoroutine()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            SpawnHuman();
            yield return new WaitForSeconds(_spawnCooldown);
        }
    }
}
