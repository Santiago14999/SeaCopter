using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    private Dictionary<Indicator, IndicatorUI> _indicators;

    private void Awake()
    {
        if (!Indicator.IsInitialized)
            Initialize();
    }

    public void Initialize()
    {
        Indicator.IsInitialized = true;
        Indicator.OnIndicatorAdded += AddIndicator;
        Indicator.OnIndicatorRemoved += RemoveIndicator;
        _indicators = new Dictionary<Indicator, IndicatorUI>();
    }

    private void OnDestroy()
    {
        Indicator.OnIndicatorAdded -= AddIndicator;
        Indicator.OnIndicatorRemoved -= RemoveIndicator;
    }

    private void AddIndicator(Indicator indicator)
    {
        IndicatorUI indicatorUI = Instantiate(indicator.IndicatorUIPrefab, transform);
        indicatorUI.SetIndicator(indicator);
        _indicators.Add(indicator, indicatorUI);
    }

    private void RemoveIndicator(Indicator indicator)
    {
        Destroy(_indicators[indicator].gameObject);
        _indicators.Remove(indicator);
    }
}
