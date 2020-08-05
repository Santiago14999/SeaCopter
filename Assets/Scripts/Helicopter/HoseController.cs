using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HoseController : MonoBehaviour
{
    [SerializeField] private float _heightOffest = .2f;
    public bool Connected { get; private set; }

    private LineRenderer _hose;
    private Transform _origin;
    private Transform _target;
    private WaterWavesController _wavesController;

    private void Awake() => _hose = GetComponent<LineRenderer>();
    private void Start() => _wavesController = WaterWavesController.Instance;

    private void Update()
    {
        if (Connected)
        {
            int lastIndex = _hose.positionCount - 1;

            _hose.SetPosition(0, _origin.position);
            _hose.SetPosition(lastIndex, _target.position);
            if (lastIndex > 1)
            {
                Vector3 directionToNextVertex = (_target.position - _origin.position) / lastIndex;
                
                for (int i = 1; i < lastIndex; i++)
                {
                    Vector3 vertexPosition = _origin.position + directionToNextVertex * i;
                    vertexPosition.y = _wavesController.GetHeightAtPosition(vertexPosition.x, vertexPosition.z) + _heightOffest;
                    _hose.SetPosition(i, vertexPosition);
                }
            }
        }
    }

    public void Connect(Transform origin, Transform target)
    {
        Connected = true;
        _origin = origin;
        _target = target;
        _hose.SetPosition(0, _origin.position);
        _hose.SetPosition(_hose.positionCount - 1, _target.position);
        _hose.enabled = true;
    }

    public void Disconnect()
    {
        Connected = false;
        _hose.enabled = false;
    }
}
