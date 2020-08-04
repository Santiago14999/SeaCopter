using System;
using UnityEngine;

[RequireComponent(typeof(FuelStorageController))]
public class FuelStorageIndicator : Indicator
{
    public override event Action<float> OnIndicatorChanged;

    private FuelStorageController _fuelStorage;

    private void Awake()
    {
        _fuelStorage = GetComponent<FuelStorageController>();
        _fuelStorage.OnFuelLevelChanged += UpdateIndicator;
    }

    private void OnDestroy() => _fuelStorage.OnFuelLevelChanged -= UpdateIndicator;

    private void UpdateIndicator(float percentage) => OnIndicatorChanged(percentage);
}
