using UnityEngine;

public class RepulsiveForce : MonoBehaviour
{
    [SerializeField] private float _minInfluenceDistance = 5f;
    [SerializeField] private float _force = 10f;
    [SerializeField] private float _drag = 5f;
    [SerializeField] private bool _influenceRotation;
    [SerializeField] private float _rotationSmoothTime = 2f;
    [SerializeField] private float _minMagnitudeToRotate = 1f;

    private static Transform _helicopter;
    private Vector3 _direction;

    private void Awake()
    {
        if (!_helicopter)
            _helicopter = FindObjectOfType<HelicopterMovementController>().transform;
    }

    private void Update()
    {
        float distance = (_helicopter.position - transform.position).magnitude;
        if (distance < _minInfluenceDistance)
        {
            _direction = transform.position - _helicopter.position;
            _direction.y = 0;
            _direction.Normalize();
            if (distance == 0)
                distance = .001f;
            _direction *= Mathf.Exp((1f / distance)) * _force;
        }

        transform.Translate(_direction * Time.deltaTime, Space.World);
        _direction = Vector3.Lerp(_direction, Vector3.zero, Time.deltaTime * _drag);

        if (_influenceRotation && _direction.sqrMagnitude > _minMagnitudeToRotate)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_direction);
            Quaternion rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSmoothTime);
            rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = rotation;
        }
    }
}
