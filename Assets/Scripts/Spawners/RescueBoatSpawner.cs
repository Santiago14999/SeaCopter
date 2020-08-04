using UnityEngine;

public class RescueBoatSpawner : MonoBehaviour
{
    [SerializeField] private HelicopterRescueController _rescueController;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _spawnDistance = 2f;

    private ObjectPooler _objectPooler;
    private bool _isBoatActive;

    private void Awake() => _rescueController.OnHumanLoaded += SpawnRescueBoat;
    private void Start() => _objectPooler = ObjectPooler.Instance;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnRescueBoat();
    }

    private void SpawnRescueBoat()
    {
        //if (_isBoatActive)
        //    return;

        //_isBoatActive = true;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = new Vector3(randomDirection.x, 0, randomDirection.y);
        spawnPosition = _rescueController.transform.position + spawnPosition * _spawnDistance;

        _objectPooler.SpawnFromPool(ObjectPooler.ObjectType.RescueBoat, spawnPosition, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
    }
}
