using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HoseController : MonoBehaviour
{
    public bool Connected { get; private set; }

    private LineRenderer _hose;
    private Transform _origin;
    private Transform _target;

    private void Awake() => _hose = GetComponent<LineRenderer>();

    private void Update()
    {
        if (Connected)
        {
            _hose.SetPosition(0, _origin.position);
            _hose.SetPosition(1, _target.position);
        }
    }

    public void Connect(Transform origin, Transform target)
    {
        Connected = true;
        _origin = origin;
        _target = target;
        _hose.SetPosition(0, _origin.position);
        _hose.SetPosition(1, _target.position);
        _hose.enabled = true;
    }

    public void Disconnect()
    {
        Connected = false;
        _hose.enabled = false;
    }
}
