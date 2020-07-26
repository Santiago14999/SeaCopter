using UnityEngine;

[RequireComponent(typeof(FuelStorageController))]
public class FuelStorageIndicator : MonoBehaviour
{
    public static event System.Action<FuelStorageIndicator> OnIndicatorAdded = delegate { };
    public static event System.Action<FuelStorageIndicator> OnIndicatorRemoved = delegate { };

    public event System.Action<float> OnFuelLevelChanged = delegate { };

    private FuelStorageController _fuelStorage;
    private float _currentFuelLevel;

    private void Awake()
    {
        _fuelStorage = GetComponent<FuelStorageController>();
        _currentFuelLevel = _fuelStorage.GetFuelPercentage();
    }

    private void OnEnable() => OnIndicatorAdded(this);

    private void OnDisable() => OnIndicatorRemoved(this);

    private void Update()
    {
        float fuelPercentage = _fuelStorage.GetFuelPercentage();
        if (_currentFuelLevel != fuelPercentage)
        {
            _currentFuelLevel = fuelPercentage;
            OnFuelLevelChanged(_currentFuelLevel);
        }
    }
}
