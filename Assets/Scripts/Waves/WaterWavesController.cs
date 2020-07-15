using UnityEngine;

public class WaterWavesController : MonoBehaviour
{
	[SerializeField] private float _wavesSpeed = 1f;
	[SerializeField] private float _wavesDirectionInDegrees = 45f;
	[SerializeField] private float _heightMultiplier = 1f;
	[SerializeField] private float _noiseScale = 2f;

	private Vector2 _wavesDirection;
    private float _currentOffset;

    public static WaterWavesController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is another WaterWavesController on the scene");

        Instance = this;

        _wavesDirection = new Vector2(Mathf.Cos(_wavesDirectionInDegrees * Mathf.Deg2Rad), Mathf.Sin(_wavesDirectionInDegrees * Mathf.Deg2Rad));
        _wavesDirection.Normalize();
    }

    private void Update() => _currentOffset = Time.time * _wavesSpeed;

    public float GetHeightAtPosition(float x, float z)
    {
        if (_noiseScale <= 0)
            _noiseScale = 1f;

        return (Mathf.PerlinNoise((x + _currentOffset) * _wavesDirection.x / _noiseScale, (z + _currentOffset) * _wavesDirection.y / _noiseScale) -  0.5f) * _heightMultiplier;
    }
}
