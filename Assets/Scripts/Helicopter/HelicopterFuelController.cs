using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HelicopterMovementController))]
public class HelicopterFuelController : MonoBehaviour
{
    public event System.Action OnOutOfFuel = delegate { };

    [SerializeField] private Image _fuelBar;
    [SerializeField] private float _maxFuelLevel = 100f;

    private HelicopterMovementController _movementController;
    private float _fuelLevel;
    private bool _isGrounded;

    private void Awake()
    {
        _movementController = GetComponent<HelicopterMovementController>();
        _movementController.OnGroundedStateChanged += UpdateGroundedState;
        _fuelLevel = _maxFuelLevel;
    }

    private void OnDestroy() => _movementController.OnGroundedStateChanged -= UpdateGroundedState;

    private void Update()
    {
        if (_isGrounded || _fuelLevel == 0)
            return;

        _fuelLevel -= Time.deltaTime;
        if (_fuelLevel < 0)
        {
            _fuelLevel = 0;
            OnOutOfFuel();
        }

        _fuelBar.fillAmount = _fuelLevel / _maxFuelLevel;
    }

    public void AddFuel(float amount)
    {
        _fuelLevel += amount;
        if (_fuelLevel > _maxFuelLevel)
            _fuelLevel = _maxFuelLevel;
    }

    private void UpdateGroundedState(bool isGrounded) => _isGrounded = isGrounded;
}
