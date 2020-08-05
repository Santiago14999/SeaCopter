using System.Collections;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    [SerializeField] private float _startDelay = 8f;
    [SerializeField] private float _spawnDelay = 16f;
    [SerializeField] private float _spawnDistance = 75f;
    [SerializeField] private float _spawnDistanceFromHuman = 20f;
    [SerializeField, Range(0, 1f)] private float _boatSpawnChance = .1f;

    private ObjectPooler _objectPooler;
    private Coroutine _spawnerCoroutine;
    private Transform _player;

    private void Awake()
    {
        _player = FindObjectOfType<HelicopterMovementController>().transform;
        GameManager.OnGameStarted += StartSpawner;
        GameManager.OnGameEnded += StopSpawner;
    }

    private void StartSpawner() => _spawnerCoroutine = StartCoroutine(SpawnerCoroutine());
    private void StopSpawner() => StopCoroutine(_spawnerCoroutine);

    private void Start() => _objectPooler = ObjectPooler.Instance;

    private void SpawnFuel()
    {
        HumanController human = FindObjectOfType<HumanController>();
        float spawnDistance = human ? _spawnDistanceFromHuman : _spawnDistance;
        Vector3 spawnPosition = OffScreenCalculator.GetRandomPositionFromOrigin(human ? human.transform : _player, spawnDistance);

        ObjectPooler.ObjectType storageType = Random.Range(0, 1f) < _boatSpawnChance ? ObjectPooler.ObjectType.FuelBoat : ObjectPooler.ObjectType.FuelBarrel;
        _objectPooler.SpawnFromPool(storageType, spawnPosition, Quaternion.Euler(0, Random.Range(0, 360f), 0));
    }

    IEnumerator SpawnerCoroutine()
    {
        yield return new WaitForSeconds(_startDelay);
        while(true)
        {
            SpawnFuel();
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
