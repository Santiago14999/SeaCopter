using UnityEngine;

public class FuelStorageController : MonoBehaviour
{
    [SerializeField] private float _refuelSpeed;
    [SerializeField] private bool _infiniteSource;
    [SerializeField] private float _fuelCapacity;

    private float _fuelLevel;

    private void Awake() => _fuelLevel = _fuelCapacity;

    public float GetFuel()
    {
        if (_infiniteSource)
            return Time.deltaTime * _refuelSpeed;

        if (_fuelLevel > 0)
        {
            float fuel = Time.deltaTime * _refuelSpeed;
            _fuelLevel -= fuel;
            return fuel;
        }

        return 0;
    }

    public float GetFuelPercentage() => _fuelLevel / _fuelCapacity;
}
