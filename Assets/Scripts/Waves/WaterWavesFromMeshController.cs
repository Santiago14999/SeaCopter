using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WaterWavesFromMeshController : MonoBehaviour
{
    [Tooltip("Mesh, copy of which will be created.")]
    [SerializeField] private Mesh _templateMesh;
    [Tooltip("Append position of this object to its offset?")]
    [SerializeField] private bool _appendPosition;

    private MeshFilter _meshFilter;
    private MeshData _currentMesh;
    private Transform _transform;
    private WaterWavesController _wavesController;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _transform = transform;
        _currentMesh = new MeshData(_templateMesh);
    }

    private void Start() => _wavesController = WaterWavesController.Instance; 


    private void Update()
    {
        float xOffset = _appendPosition ? (_transform.position.x / _transform.localScale.x) : 0;
        float zOffset = _appendPosition ? (_transform.position.z / _transform.localScale.z) : 0;

        _meshFilter.sharedMesh = _currentMesh.UpdateMesh(_wavesController, xOffset, zOffset);
    }

    private class MeshData
    {
        private List<Vector3> _vertices;
        private List<int> _triangles;
        private List<Vector2> _uvs;

        private Mesh _mesh;
        private int _vertexCount;

        public MeshData(Mesh templateMesh)
        {
            _vertexCount = templateMesh.vertexCount;

            _vertices = new List<Vector3>(_vertexCount);
            _uvs = new List<Vector2>(_vertexCount);
            _triangles = new List<int>(_vertexCount * 6);

            templateMesh.GetVertices(_vertices);
            templateMesh.GetUVs(0, _uvs);
            templateMesh.GetTriangles(_triangles, 0);

            _mesh = new Mesh();
            _mesh.SetVertices(_vertices);
            _mesh.SetTriangles(_triangles, 0);
            _mesh.SetUVs(0, _uvs);
            _mesh.RecalculateNormals();
        }

        public Mesh UpdateMesh(WaterWavesController wavesController, float xOffset, float zOffset)
        {
            for(int i = 0; i < _vertexCount; i++)
            {
                Vector3 vert = _vertices[i];
                vert.y = wavesController.GetHeightAtPosition(vert.x + xOffset, vert.z + zOffset);
                _vertices[i] = vert;
            }

            _mesh.SetVertices(_vertices);
            _mesh.RecalculateNormals();
            return _mesh;
        }

    }
}
