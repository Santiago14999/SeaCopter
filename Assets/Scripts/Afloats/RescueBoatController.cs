using UnityEngine;

public class RescueBoatController : MonoBehaviour, IPoolable
{
    [SerializeField] private float _boatSpeed = 10f;
    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float _offsetToDestroy = .2f;

    private float _currentSpeed;
    private bool _isLoaded;
    private Camera _camera;
    private Indicator _indicator;

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
        _indicator = GetComponent<Indicator>();
    }

    public void OnSpawn()
    {
        _currentSpeed = 0;
        _isLoaded = false;
        _indicator.enabled = true;
    }

    private void Update()
    {
        if (_isLoaded)
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
        if (_isLoaded)
            return;

        if (other.TryGetComponent<HelicopterRescueController>(out var rescureController))
        {
            if (rescureController.HumansInHelicopter == 0)
                return;

            int humans = rescureController.UnloadHumans();
            _isLoaded = true;
            _indicator.enabled = false;
        }
    }
}
