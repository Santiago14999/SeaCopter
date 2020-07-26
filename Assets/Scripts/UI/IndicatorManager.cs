using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    [SerializeField] private FuelStorageBar _indicatorPrefab;
    private Dictionary<FuelStorageIndicator, FuelStorageBar> _indicators;

    private void Awake()
    {
        FuelStorageIndicator.OnIndicatorAdded += AddIndicator;
        FuelStorageIndicator.OnIndicatorRemoved += RemoveIndicator;
        _indicators = new Dictionary<FuelStorageIndicator, FuelStorageBar>();
    }

    private void OnDestroy()
    {
        FuelStorageIndicator.OnIndicatorAdded -= AddIndicator;
        FuelStorageIndicator.OnIndicatorRemoved -= RemoveIndicator;
    }

    private void AddIndicator(FuelStorageIndicator indicator)
    {
        var bar = Instantiate(_indicatorPrefab, transform);
        bar.SetIndicator(indicator);
        _indicators.Add(indicator, bar);
    }

    private void RemoveIndicator(FuelStorageIndicator indicator)
    {
        Destroy(_indicators[indicator].gameObject);
        _indicators.Remove(indicator);
    }
}
