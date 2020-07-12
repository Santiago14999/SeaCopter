using UnityEngine;

public class WaterWavesController : MonoBehaviour
{
    [Header("Waves Settings")]
    [SerializeField] private float _wavesSpeed = 1f;
    [SerializeField] private float _wavesDirectionInDegrees = 45f;
    [SerializeField] private int _planeWidth = 100;
    [SerializeField] private int _planeHeight = 100;
    [SerializeField] private float _heightMultiplier = 5f;
    [SerializeField] private float _noiseScale = 25f;
    [SerializeField] private Vector2 _noiseOffset;

    [SerializeField] private MeshFilter _meshFilter;

    [Header("Preview Settings")]
    public bool showPreview = false;
    [SerializeField] private float _previewOffset;

    private float _currentOffset;
    private Vector2 _wavesDirection;
    private MeshData _currentMesh;
    private float[,] _heightsMap;


    // Editor function
    public void GenerateMeshPreview()
    {
        _heightsMap = new float[_planeWidth, _planeHeight];

        _wavesDirection = new Vector2(Mathf.Cos(_wavesDirectionInDegrees * Mathf.Deg2Rad), Mathf.Sin(_wavesDirectionInDegrees * Mathf.Deg2Rad));
        _wavesDirection.Normalize();
       
        MeshData meshData = new MeshData(_planeWidth, _planeHeight);

        Vector2 offset = new Vector2(_noiseOffset.x + _previewOffset * _wavesDirection.x, _noiseOffset.y + _previewOffset * _wavesDirection.y);
        Noise.GenerateNoiseMap(_heightsMap, _planeWidth, _planeHeight, _noiseScale, offset);


        _meshFilter.sharedMesh = meshData.UpdateMesh(_heightsMap, _heightMultiplier);
    }

    private void OnValidate()
    {
        if (_planeWidth < 1)
            _planeWidth = 1;

        if (_planeHeight < 1)
            _planeHeight = 1;
    }


    private void Start()
    {
        _heightsMap = new float[_planeWidth, _planeHeight];

        _currentMesh = new MeshData(_planeWidth, _planeHeight);

        _wavesDirection = new Vector2(Mathf.Cos(_wavesDirectionInDegrees * Mathf.Deg2Rad), Mathf.Sin(_wavesDirectionInDegrees * Mathf.Deg2Rad));
        _wavesDirection.Normalize();
    }


    private void Update()
    {
        _currentOffset += Time.deltaTime * _wavesSpeed;
        Vector2 offset = new Vector2(_noiseOffset.x + _currentOffset * _wavesDirection.x, _noiseOffset.y + _currentOffset * _wavesDirection.y);

        Noise.GenerateNoiseMap(_heightsMap, _planeWidth, _planeHeight, _noiseScale, offset);
        _meshFilter.sharedMesh = _currentMesh.UpdateMesh(_heightsMap, _heightMultiplier);
    }


    private class MeshData
    {
        private Vector3[] vertices;
        private int[] triangles;
        private Vector2[] uvs;

        private int _triangleIndex;

        private Mesh _mesh;
        private int _meshWidth;
        private int _meshHeight;

        public MeshData(int meshWidth, int meshHeight)
        {
            _meshWidth = meshWidth;
            _meshHeight = meshHeight;

            vertices = new Vector3[meshWidth * meshHeight];
            uvs = new Vector2[meshWidth * meshHeight];
            triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];

            GenerateMesh();
        }

        public void AddTriangle(int a, int b, int c)
        {
            triangles[_triangleIndex] = a;
            triangles[_triangleIndex + 1] = b;
            triangles[_triangleIndex + 2] = c;
            _triangleIndex += 3;
        }

        private void GenerateMesh()
        {
            int vertexIndex = 0;

            float topLeftX = (_meshWidth - 1) / -2f;
            float topLeftZ = (_meshHeight - 1) / 2f;


            for (int y = 0; y < _meshHeight; y++)
            {
                for (int x = 0; x < _meshWidth; x++)
                {
                    vertices[vertexIndex] = new Vector3(topLeftX + x, 0, topLeftZ - y);
                    uvs[vertexIndex] = new Vector2(x / (float)_meshWidth, y / (float)_meshHeight);

                    if (x < _meshWidth - 1 && y < _meshHeight - 1)
                    {
                        AddTriangle(vertexIndex, vertexIndex + _meshWidth + 1, vertexIndex + _meshWidth);
                        AddTriangle(vertexIndex + _meshWidth + 1, vertexIndex, vertexIndex + 1);
                    }

                    vertexIndex++;
                }
            }

            CreateMesh();
        }

        public Mesh UpdateMesh(float[,] heightMap, float heightMultiplier)
        {
            int vertexIndex = 0;

            for (int y = 0; y < _meshHeight; y++)
            {
                for (int x = 0; x < _meshWidth; x++)
                {

                    vertices[vertexIndex].y = heightMap[x, y] * heightMultiplier;
                    vertexIndex++;
                }
            }

            _mesh.vertices = vertices;
            _mesh.RecalculateNormals();
            return _mesh;
        }

        public void CreateMesh()
        {
            _mesh = new Mesh();
            _mesh.vertices = vertices;
            _mesh.triangles = triangles;
            _mesh.uv = uvs;
            _mesh.RecalculateNormals();
        }
    }
}
