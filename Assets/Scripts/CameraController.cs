using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _cameraHeight;
    [SerializeField] private float _cameraDistance;
    [SerializeField] private float _followSpeed;

    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        Vector3 requiredPosition = new Vector3(_transform.position.x, _transform.position.y + _cameraHeight, _transform.position.z + _cameraDistance);
        _camera.position = Vector3.Lerp(_camera.position, requiredPosition, Time.deltaTime * _followSpeed);

        _camera.LookAt(_transform.position);
    }
}
