using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    [SerializeField] private Transform _indicatorsParent;
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
        if (_indicators.ContainsKey(indicator))
        {
            _indicators[indicator].gameObject.SetActive(true);
        }
        else
        {
            IndicatorUI indicatorUI = Instantiate(indicator.IndicatorUIPrefab, _indicatorsParent);
            indicatorUI.SetIndicator(indicator);
            _indicators.Add(indicator, indicatorUI);
        }
    }

    private void RemoveIndicator(Indicator indicator)
    {
        _indicators[indicator].gameObject.SetActive(false);
        _indicators.Remove(indicator);
    }
}
