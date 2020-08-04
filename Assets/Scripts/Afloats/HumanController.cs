using UnityEngine;

public class HumanController : MonoBehaviour
{
    public static event System.Action OnDrown = delegate { };

    [SerializeField] private float _timeToDrown = 60f;

    private float _currentFloatingTime;
    private ObjectPooler _objectPooler;

    private void Awake() => _currentFloatingTime = _timeToDrown;
    private void Start() => _objectPooler = ObjectPooler.Instance;

    private void Update()
    {
        _currentFloatingTime -= Time.deltaTime;
        if (_currentFloatingTime < 0)
            Drown();
    }

    private void Drown()
    {
        OnDrown();
        gameObject.SetActive(false);
    }

    public float GetPercentage() => _currentFloatingTime / _timeToDrown;

    public void GrabRope()
    {
        _objectPooler.SpawnFromPool(ObjectPooler.ObjectType.Lifebuoy, transform.position + Vector3.up, transform.rotation);
        gameObject.SetActive(false);
    }
}
