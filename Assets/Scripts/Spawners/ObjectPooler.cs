using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public enum ObjectType { Human, FuelBoat, FuelBarrel, RescueBoat, Lifebuoy }

    [System.Serializable]
    public class Pool
    {
        public ObjectType objectType;
        public GameObject prefab;
        public int size;
    }

    #region singleton
    public static ObjectPooler Instance { get; private set; }
    private void Awake() => Instance = this;
    #endregion

    [SerializeField] private List<Pool> _pools;
    private Dictionary<ObjectType, Queue<GameObject>> _poolDictionary;

    private void Start()
    {
        _poolDictionary = new Dictionary<ObjectType, Queue<GameObject>>();

        foreach(Pool pool in _pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.objectType, objectPool);
        }
    }

    public GameObject SpawnFromPool(ObjectType objectType, Vector3 position, Quaternion rotation)
    {
        GameObject objectToSpawn = _poolDictionary[objectType].Dequeue();
        
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        if (objectToSpawn.TryGetComponent<IPoolable>(out var poolable))
            poolable.OnSpawn();

        _poolDictionary[objectType].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}