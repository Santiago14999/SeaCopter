using UnityEngine;

public class Floater : MonoBehaviour
{
    public bool IsFloating;
    [Range(0.01f, 1f)]
    [SerializeField] private float _normalCheckDelta = .1f;
    [SerializeField] private float _yOffset;

    private WaterWavesController _wavesController;
    private Transform _transform;

    private void Awake() => _transform = transform;
    private void Start() => _wavesController = WaterWavesController.Instance;

    private void LateUpdate()
    {
        if (!IsFloating)
            return;

        Vector3 position = _transform.position;
        position.y = _wavesController.GetHeightAtPosition(position.x, position.z) + _yOffset;
        _transform.position = position;

        float leftAdjacentX = _wavesController.GetHeightAtPosition(position.x - _normalCheckDelta, position.z);
        float rightAdjacentX = _wavesController.GetHeightAtPosition(position.x + _normalCheckDelta, position.z);
        float leftAdjacentZ = _wavesController.GetHeightAtPosition(position.x, position.z - _normalCheckDelta);
        float rightAdjacentZ = _wavesController.GetHeightAtPosition(position.x, position.z + _normalCheckDelta);

        Vector3 xLeftPoint = new Vector3(position.x - _normalCheckDelta, leftAdjacentX, position.z);
        Vector3 xRightPoint = new Vector3(position.x + _normalCheckDelta, rightAdjacentX, position.z);
        Vector3 zLeftPoint = new Vector3(position.x, leftAdjacentZ, position.z - _normalCheckDelta);
        Vector3 zRightPoint = new Vector3(position.x, rightAdjacentZ, position.z + _normalCheckDelta);

        _transform.rotation = Quaternion.LookRotation(_transform.forward, Vector3.Cross(zRightPoint - zLeftPoint, xRightPoint - xLeftPoint));
    }
}
