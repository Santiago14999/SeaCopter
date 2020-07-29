using UnityEngine;

public class FloatController : MonoBehaviour
{
    public bool IsFloating;
    [SerializeField] private float _buoyancySmoothTime = 5f;
    [SerializeField] private float _rotationSmoothTime = 5f;
    [SerializeField] private float _yOffset;

    private WaterWavesController _wavesController;

    private void Start()
    {
        _wavesController = WaterWavesController.Instance;
        if (!_wavesController)
            enabled = false;
    }

    public bool IsUnderWater(Vector3 position)
    {
        if (_wavesController.GetHeightAtPosition(position.x, position.z) >= position.y + _yOffset)
            return true;

        return false;
    }

    private void LateUpdate()
    {
        if (!IsFloating)
            return;

        // Position
        Vector3 position = transform.position;
        position.y = _wavesController.GetHeightAtPosition(position.x, position.z) - _yOffset;
        float buoyancyForce = Mathf.Abs(position.y - transform.position.y) + 1;
        transform.position = Vector3.Lerp(transform.position, position, buoyancyForce * _buoyancySmoothTime * Time.deltaTime);

        // Rotating
        float yRotation = transform.rotation.eulerAngles.y;
        Vector3 right = transform.right;
        Vector3 forward = transform.forward;

        float leftHeight = _wavesController.GetHeightAtPosition(position.x - right.x, position.z - right.z);
        float rightHeight = _wavesController.GetHeightAtPosition(position.x + right.x, position.z + right.z);
        float backHeight = _wavesController.GetHeightAtPosition(position.x - forward.x, position.z - forward.z);
        float forwardHeight = _wavesController.GetHeightAtPosition(position.x + forward.x, position.z + forward.z);

        Vector3 leftPoint = new Vector3(position.x - right.x, leftHeight, position.z - right.z);
        Vector3 rightPoint = new Vector3(position.x + right.x, rightHeight, position.z + right.z);
        Vector3 backPoint = new Vector3(position.x - forward.x, backHeight, position.z - forward.z);
        Vector3 forwardPoint = new Vector3(position.x + forward.x, forwardHeight, position.z + forward.z);

        Quaternion rotation = Quaternion.LookRotation(forwardPoint - backPoint, Vector3.Cross(forwardPoint - backPoint, rightPoint - leftPoint));
        rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSmoothTime * Time.deltaTime);
        rotation = Quaternion.Euler(rotation.eulerAngles.x, yRotation, rotation.eulerAngles.z);
        transform.rotation = rotation;
    }
}
