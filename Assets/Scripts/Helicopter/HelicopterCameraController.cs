using UnityEngine;

[RequireComponent(typeof(HelicopterMovementController))]
public class HelicopterCameraController : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Vector3 _cameraMainPosition;
    [SerializeField] private float _followSpeed;
    [SerializeField] private bool _testChangePosition;

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
        Vector3 requiredPosition = transform.position +_cameraMainPosition;
        if (_freezeHegiht)
            requiredPosition.y = _cameraMainPosition.y;

        _camera.position = Vector3.Lerp(_camera.position, requiredPosition, Time.deltaTime * _followSpeed);
        _camera.LookAt(transform.position);
    }

    private void UpdateFreezeHeightState(bool isGrounded) => _freezeHegiht = isGrounded;
}
