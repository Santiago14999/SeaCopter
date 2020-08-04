using TMPro;
using UnityEngine;

[RequireComponent(typeof(HelicopterRescueController))]
public class HelicopterRescueUI : MonoBehaviour
{
    [SerializeField] private Transform _canvas;
    [SerializeField] private float _height;
    [SerializeField] private TextMeshProUGUI _humansInHelicopterText;

    private HelicopterRescueController _rescueController;
    private Camera _camera;

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
        _rescueController = GetComponent<HelicopterRescueController>();
        _rescueController.OnHumanLoaded += UpdateUI;
        _rescueController.OnHumanUnloaded += UpdateUI;
        UpdateUI();
    }

    private void UpdateUI()
    {
        string s = $"{_rescueController.HumansInHelicopter}";
        s += $"/{ _rescueController.HelicopterCapacity}";
        _humansInHelicopterText.text = s;
    }

    private void LateUpdate() => _canvas.position = _camera.WorldToScreenPoint(transform.position + Vector3.up * _height);
}
