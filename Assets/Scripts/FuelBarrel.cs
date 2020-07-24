using UnityEngine;

public class FuelBarrel : MonoBehaviour
{
    [SerializeField] private float _fuelAmount;

    private void OnTriggerEnter(Collider other)
    {
        HelicopterFuelController fuelController = other.GetComponent<HelicopterFuelController>();
        if (fuelController == null)
            return;

        fuelController.AddFuel(_fuelAmount);
        gameObject.SetActive(false);
    }
}
