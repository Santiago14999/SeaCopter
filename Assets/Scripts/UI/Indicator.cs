using UnityEngine;

public abstract class Indicator : MonoBehaviour
{
    public static event System.Action<Indicator> OnIndicatorAdded = delegate { };
    public static event System.Action<Indicator> OnIndicatorRemoved = delegate { };
    public static bool IsInitialized;

    public abstract event System.Action<float> OnIndicatorChanged;

    [SerializeField] private IndicatorUI _indicatorUIPrefab;
    public IndicatorUI IndicatorUIPrefab { get => _indicatorUIPrefab; private set => _indicatorUIPrefab = value; }

    protected void OnEnable()
    {
        if (!IsInitialized)
            FindObjectOfType<IndicatorManager>().Initialize();

        OnIndicatorAdded(this);
    }

    protected void OnDisable() => OnIndicatorRemoved(this);
}
