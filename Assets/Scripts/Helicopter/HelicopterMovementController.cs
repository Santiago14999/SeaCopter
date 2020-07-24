using UnityEngine;

[RequireComponent(typeof(InputHandler), typeof(FloatController), typeof(HelicopterFuelController))]
public class HelicopterMovementController : MonoBehaviour
{
    public event System.Action<bool> OnGroundedStateChanged = delegate { };

    [Tooltip("Model of the helicopter which will be tilted.")]
    //[SerializeField] private Transform _helicopterModel;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _tiltSpeed;
    [SerializeField] private float _tiltAngle;
    [SerializeField] private float _movementSmoothTime;
    [SerializeField] private float _tiltSmoothTime;
    [SerializeField] private float _maxHeight;
    [SerializeField] private Transform _groundCheckOrigin;

    private FloatController _floatController;
    private InputHandler _input;
    private Vector3 _currentMoveVector;
    private Quaternion _currentTilt;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
        _floatController = GetComponent<FloatController>();
    }

    private void Update()
    {
        CheckGround();
        HandleMovementSmooth(_input.GetMoveInput());
    }

    private void HandleMovementSmooth(Vector3 requiredMoveVector)
    {
        // Movement
        if (transform.position.y + requiredMoveVector.y > _maxHeight)
            requiredMoveVector.y = 0;

        if (_floatController.IsFloating)
        {
            requiredMoveVector.x = 0;
            requiredMoveVector.z = 0;
            if (requiredMoveVector.y < 0)
            {
                requiredMoveVector.y = 0;
                _currentMoveVector.y = 0;
            }
        }

        _currentMoveVector = Vector3.Lerp(_currentMoveVector, requiredMoveVector, Time.deltaTime * _movementSmoothTime);
        transform.Translate(_currentMoveVector * _movementSpeed * Time.deltaTime, Space.World);

        // Tilt
        if (!_floatController.IsFloating)
        {
            Quaternion requiredTilt = Quaternion.Euler(new Vector3(requiredMoveVector.z, 0, -requiredMoveVector.x) * _tiltAngle);
            _currentTilt = Quaternion.Lerp(_currentTilt, requiredTilt, Time.deltaTime * _tiltSmoothTime);
            transform.rotation = _currentTilt;
        }
        else
            _currentTilt = transform.rotation;
    }

    private void CheckGround()
    {
        if (_floatController.IsUnderWater(_groundCheckOrigin.position))
        {
            if (!_floatController.IsFloating)
                OnGroundedStateChanged(true);

            _floatController.IsFloating = true;
        }
        else
        {
            if (_floatController.IsFloating)
                OnGroundedStateChanged(false);

            _floatController.IsFloating = false;
            
        }
    }
}
