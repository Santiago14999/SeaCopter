using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HelicopterMovementController))]
public class HelicopterFuelController : MonoBehaviour
{
    public event System.Action<bool> OnFuelStateChanged = delegate { };

    [SerializeField] private Image _fuelBar;
    [SerializeField] private float _maxFuelLevel = 100f;
    [SerializeField] private HoseController _hose;

    private HelicopterMovementController _movementController;
    private float _fuelLevel;
    private bool _isGrounded;
    private FuelStorageController _currentFuelStorage;
    private List<FuelStorageController> _fuelStorages;

    private void Awake()
    {
        _movementController = GetComponent<HelicopterMovementController>();
        _movementController.OnGroundedStateChanged += UpdateGroundedState;
        _fuelLevel = _maxFuelLevel;
        _fuelStorages = new List<FuelStorageController>();
    }

    private void OnDestroy() => _movementController.OnGroundedStateChanged -= UpdateGroundedState;

    private void Update()
    {
        if (_currentFuelStorage != null)
            Refill();

        if (!_isGrounded && _fuelLevel != 0)
            _fuelLevel -= Time.deltaTime;

        if (_fuelLevel < 0)
        {
            _fuelLevel = 0;
            OnFuelStateChanged(false);
        }

        _fuelBar.fillAmount = _fuelLevel / _maxFuelLevel;
    }

    public void AddFuel(float amount)
    {
        if (_fuelLevel == 0 && _fuelLevel + amount > 0)
            OnFuelStateChanged(true);
        _fuelLevel += amount;
        if (_fuelLevel > _maxFuelLevel)
            _fuelLevel = _maxFuelLevel;
    }

    private void UpdateGroundedState(bool isGrounded) => _isGrounded = isGrounded;

    private void Refill()
    {
        if (!_isGrounded)
        {
            if (_hose.Connected)
                _hose.Disconnect();
            return;
        }
        else if (!_hose.Connected)
            _hose.Connect(transform, _currentFuelStorage.transform);

        if (_fuelLevel < _maxFuelLevel)
        {
            float fuelAmount = _currentFuelStorage.GetFuel();
            if (fuelAmount == 0)
                DisconnectFuelStorage(_currentFuelStorage);
            else
                AddFuel(fuelAmount);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FuelStorageController>(out var fuelStorage))
            ConnectFuelStorage(fuelStorage);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<FuelStorageController>(out var fuelStorage))
            DisconnectFuelStorage(fuelStorage);
    }

    private void ConnectFuelStorage(FuelStorageController fuelStorage)
    {
        if (_currentFuelStorage == null)
            _currentFuelStorage = fuelStorage;
        else
            _fuelStorages.Add(fuelStorage);
    }

    private void DisconnectFuelStorage(FuelStorageController fuelStorage)
    {
        if (_fuelStorages.Count != 0)
        {
            if (fuelStorage == _currentFuelStorage)
            {
                int index = _fuelStorages.Count - 1;
                _currentFuelStorage = _fuelStorages[index];
                _fuelStorages.RemoveAt(index);
                _hose.Connect(transform, _currentFuelStorage.transform);
            }
            else
                _fuelStorages.Remove(fuelStorage);
        }
        else
        {
            _currentFuelStorage = null;
            _hose.Disconnect();
        }
    }
}
