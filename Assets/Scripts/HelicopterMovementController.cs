using System;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class HelicopterMovementController : MonoBehaviour
{
    public event Action<bool> OnGroundedStateChanged = delegate { };

    [Tooltip("Model of the helicopter which will be tilted.")]
    [SerializeField] private Transform _helicopterModel;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _tiltAngle;
    [SerializeField] private float _movementSmoothTime;
    [SerializeField] private float _tiltSmoothTime;
    [SerializeField] private float _maxHeight;
    [SerializeField] private Transform _groundCheckOrigin;

    private InputHandler _input;
    private Transform _transform;
    private Vector3 _currentMoveVector;
    private Vector3 _currentTiltVector;

    private bool _isGrounded = true;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        CheckGround();
        HandleMovementSmooth(_input.GetMoveInput());
    }

    private void HandleMovementSmooth(Vector3 requiredMoveVector)
    {
        // Movement
        if (_transform.position.y + requiredMoveVector.y > _maxHeight)
            requiredMoveVector.y = 0;

        if (_isGrounded)
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
        _transform.Translate(_currentMoveVector * _movementSpeed * Time.deltaTime, Space.World);

        // Tilt
        Vector3 requiredTiltVector = new Vector3(requiredMoveVector.z, 0, -requiredMoveVector.x) * _tiltAngle;
        _currentTiltVector = Vector3.Lerp(_currentTiltVector, requiredTiltVector, Time.deltaTime * _tiltSmoothTime);
        _helicopterModel.rotation = Quaternion.Euler(_currentTiltVector);
    }

    private void CheckGround()
    {
        // TODO: Compare position.y with Waves Perlin Noise Value

        if (_groundCheckOrigin.position.y > 0)
        {
            if (_isGrounded)
                OnGroundedStateChanged(false);

            _isGrounded = false;
        }
        else
        {
            if (!_isGrounded)
                OnGroundedStateChanged(true);

            _isGrounded = true;
        }
    }
}
