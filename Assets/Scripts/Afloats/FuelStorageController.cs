using UnityEngine;

public class FuelStorageController : MonoBehaviour, IPoolable
{
    public event System.Action<float> OnFuelLevelChanged;

    [SerializeField] private float _refuelSpeed;
    [SerializeField] private bool _isInfiniteSource;
    [SerializeField] private float _fuelCapacity;

    private float _fuelLevel;

    public void OnSpawn() => _fuelLevel = _fuelCapacity;

    public float GetFuel()
    {
        if (_isInfiniteSource)
            return Time.deltaTime * _refuelSpeed;

        if (_fuelLevel > 0)
        {
            float fuel = Time.deltaTime * _refuelSpeed;
            _fuelLevel -= fuel;
            OnFuelLevelChanged(_fuelLevel / _fuelCapacity);
            return fuel;
        }

        return 0;
    }

    public float GetFuelPercentage() => _fuelLevel / _fuelCapacity;

    
}
