using UnityEngine;
using UnityEngine.UI;

public class FuelStorageBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private float _height;
    [SerializeField, Range(0, .4f)] private float _borderOffset;

    private FuelStorageIndicator _indicator;
    private Camera _camera;

    private void Awake() => _camera = FindObjectOfType<Camera>();

    public void SetIndicator(FuelStorageIndicator indicator)
    {
        _indicator = indicator;
        _indicator.OnFuelLevelChanged += UpdateBarFillAmount;
    }

    private void UpdateBarFillAmount(float percentage) => _bar.fillAmount = percentage;

    private void LateUpdate()
    {
        Vector3 viewport = _camera.WorldToViewportPoint(_indicator.transform.position + Vector3.up * _height);
        viewport.x = Mathf.Clamp(viewport.x, _borderOffset, 1 - _borderOffset);
        viewport.y = Mathf.Clamp(viewport.y, _borderOffset, 1 - _borderOffset);
        
        transform.position = _camera.ViewportToScreenPoint(viewport);
    }
}
