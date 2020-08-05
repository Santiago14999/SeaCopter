using UnityEngine;

public class RescueBoatController : MonoBehaviour, IPoolable
{
    [SerializeField] private float _boatSpeed = 10f;
    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float _offsetToDestroy = .2f;
    [SerializeField] private GameObject[] _humansOnBoat;

    public bool IsLoaded { get; private set; }

    private float _currentSpeed;
    private Indicator _indicator;
    private static Camera _camera;

    private void Awake()
    {
        if (!_camera)
            _camera = FindObjectOfType<Camera>();
        _indicator = GetComponent<Indicator>();
    }

    public void OnSpawn()
    {
        _currentSpeed = 0;
        IsLoaded = false;
        _indicator.enabled = true;
        for (int i = 0; i < _humansOnBoat.Length; i++)
            _humansOnBoat[i].SetActive(false);
    }

    private void LateUpdate()
    {
        if (IsLoaded)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, _boatSpeed, Time.deltaTime * _acceleration);
            transform.Translate(Vector3.forward * _currentSpeed * Time.deltaTime);

            Vector3 viewport = _camera.WorldToViewportPoint(transform.position);
            if (viewport.x < -_offsetToDestroy || viewport.x > 1 + _offsetToDestroy ||
                viewport.y < -_offsetToDestroy || viewport.y > 1 + _offsetToDestroy)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsLoaded)
            return;

        if (other.TryGetComponent<HelicopterRescueController>(out var rescureController))
        {
            if (rescureController.HumansInHelicopter == 0)
                return;

            int humans = rescureController.UnloadHumans();
            for (int i = 0; i < humans; i++)
                _humansOnBoat[i].SetActive(true);

            IsLoaded = true;
            _indicator.enabled = false;
        }
    }
}
