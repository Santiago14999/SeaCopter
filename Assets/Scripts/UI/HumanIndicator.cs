using System;
using UnityEngine;

[RequireComponent(typeof(HumanController))]
public class HumanIndicator : Indicator
{
    public override event Action<float> OnIndicatorChanged;

    private HumanController _humanController;

    private void Awake() => _humanController = GetComponent<HumanController>();
    private void LateUpdate() => OnIndicatorChanged(_humanController.GetPercentage());
}
