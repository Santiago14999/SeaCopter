using UnityEngine;

[RequireComponent(typeof(HelicopterMovementController))]
public class HelicopterCameraController : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _cameraHeight;
    [SerializeField] private float _cameraDistance;
    [SerializeField] private float _followSpeed;

    private HelicopterMovementController _movementController;
    private bool _freezeHegiht;


    private void Awake()
    {
        _movementController = GetComponent<HelicopterMovementController>();
        _movementController.OnGroundedStateChanged += UpdateFreezeHeightState;
    }

    private void OnDestroy() => _movementController.OnGroundedStateChanged -= UpdateFreezeHeightState;

    private void LateUpdate()
    {
        Vector3 requiredPosition = new Vector3(transform.position.x, _cameraHeight + transform.position.y, transform.position.z + _cameraDistance);
        if (_freezeHegiht)
            requiredPosition.y = _cameraHeight;

        _camera.position = Vector3.Lerp(_camera.position, requiredPosition, Time.deltaTime * _followSpeed);
        _camera.LookAt(transform.position);
    }

    private void UpdateFreezeHeightState(bool isGrounded) => _freezeHegiht = isGrounded;
}
