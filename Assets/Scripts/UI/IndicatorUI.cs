using UnityEngine;
using UnityEngine.UI;

public class IndicatorUI : MonoBehaviour
{
    [SerializeField] private Image _indicatorImage;
    [SerializeField] private float _height = 6f;
    [SerializeField, Range(0f, .4f)] private float _borderOffset = .1f;
    [SerializeField, Range(0f, .9f)] private float _minIconSize = .4f;

    private Indicator _indicator;
    private Camera _camera;

    private void Awake() => _camera = FindObjectOfType<Camera>();

    public void SetIndicator(Indicator indicator)
    {
        _indicator = indicator;
        _indicator.OnIndicatorChanged += UpdateIndicator;
    }

    private void UpdateIndicator(float percentage) => _indicatorImage.fillAmount = percentage;

    private void LateUpdate()
    {
        Vector3 viewport = _camera.WorldToViewportPoint(_indicator.transform.position + Vector3.up * _height);

        float offScreenDistance = OffScreenDistance(viewport.x, viewport.y);

        if (viewport.y < -1 || viewport.z < 0)
        {
            Vector3 cameraRelativePos = _indicator.transform.position;
            cameraRelativePos.z = _camera.transform.position.z;
            Vector3 indicatorPos = _indicator.transform.position + Vector3.forward * (cameraRelativePos - _indicator.transform.position).magnitude;
            viewport = _camera.WorldToViewportPoint(indicatorPos);
            offScreenDistance = OffScreenDistance(viewport.x, viewport.y);
        }

        viewport.x = Mathf.Clamp(viewport.x, _borderOffset, 1 - _borderOffset);
        viewport.y = Mathf.Clamp(viewport.y, _borderOffset, 1 - _borderOffset);
        viewport.z = 0;

        transform.position = _camera.ViewportToScreenPoint(viewport);
        transform.localScale = Vector3.one * Mathf.Clamp((1f / (offScreenDistance + 1)), _minIconSize, 1f);
    }

    private float OffScreenDistance(float viewportX, float viewportY)
    {
        float xDistance = 0, yDistance = 0;

        if (viewportX > 1)
            xDistance = viewportX - 1;
        else if (viewportX < 0)
            xDistance = 0 - viewportX;

        if (viewportY > 1)
            yDistance = viewportY - 1;
        else if (viewportY < 0)
            yDistance = 0 - viewportY;

        return Mathf.Sqrt(xDistance * xDistance + yDistance * yDistance);
    }
}
