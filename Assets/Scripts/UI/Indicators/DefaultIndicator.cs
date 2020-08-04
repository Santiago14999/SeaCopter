using System;

public class DefaultIndicator : Indicator
{
    public override event Action<float> OnIndicatorChanged = delegate { };
}
