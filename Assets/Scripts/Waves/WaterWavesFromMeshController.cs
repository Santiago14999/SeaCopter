using System.Collections.Generic;
using UnityEngine;

public class WaterWavesFromMeshController : MonoBehaviour
{
    [Header("Waves Settings")]
    [SerializeField] private Mesh _templateMesh;
    [SerializeField] private float _wavesSpeed = 1f;
    [SerializeField] private float _wavesDirectionInDegrees = 45f;
    [SerializeField] private float _heightMultiplier = 5f;
    [SerializeField] private Vector2 _noiseOffset;
    [SerializeField] private float _noiseScale;
    [SerializeField] private bool _appendGlobalPosition;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private Transform _transform;

    [Header("Preview Settings")]
    public bool showPreview = false;
    [SerializeField] private float _previewOffset;

    private float _currentOffset;
    private Vector2 _wavesDirection;
    private MeshData _currentMesh;

    // Editor function
    public void GenerateMeshPreview()
    {
        _wavesDirection = new Vector2(Mathf.Cos(_wavesDirectionInDegrees * Mathf.Deg2Rad), Mathf.Sin(_wavesDirectionInDegrees * Mathf.Deg2Rad));
        _wavesDirection.Normalize();
       
        MeshData meshData = new MeshData(_templateMesh);

        Vector2 offset = new Vector2(_noiseOffset.x + _previewOffset * _wavesDirection.x, _noiseOffset.y + _previewOffset * _wavesDirection.y);
        if (_appendGlobalPosition)
        {
            offset.x += transform.position.x / transform.localScale.x;
            offset.y += transform.position.z / transform.localScale.z;
        }
        _meshFilter.sharedMesh = meshData.UpdateMesh(offset, _noiseScale, _heightMultiplier);
    }

    private void Start()
    {
        _transform = transform;
        _currentMesh = new MeshData(_templateMesh);

        _wavesDirection = new Vector2(Mathf.Cos(_wavesDirectionInDegrees * Mathf.Deg2Rad), Mathf.Sin(_wavesDirectionInDegrees * Mathf.Deg2Rad));
        _wavesDirection.Normalize();
    }


    private void Update()
    {
        _currentOffset = Time.time * _wavesSpeed;
        Vector2 offset = new Vector2(_noiseOffset.x + _currentOffset * _wavesDirection.x, _noiseOffset.y + _currentOffset * _wavesDirection.y);

        if (_appendGlobalPosition)
        {
            offset.x += _transform.position.x / transform.localScale.x;
            offset.y += _transform.position.z / transform.localScale.z;
        }

        _meshFilter.sharedMesh = _currentMesh.UpdateMesh(offset, _noiseScale, _heightMultiplier);
    }


    private class MeshData
    {
        private List<Vector3> vertices;
        private List<int> triangles;
        private List<Vector2> uvs;

        private Mesh _mesh;
        private int vertexCount;

        public MeshData(Mesh templateMesh)
        {
            vertexCount = templateMesh.vertexCount;

            vertices = new List<Vector3>(vertexCount);
            uvs = new List<Vector2>(vertexCount);
            triangles = new List<int>(vertexCount * 6);

            templateMesh.GetVertices(vertices);
            templateMesh.GetUVs(0, uvs);
            templateMesh.GetTriangles(triangles, 0);

            _mesh = new Mesh();
            _mesh.SetVertices(vertices);
            _mesh.SetTriangles(triangles, 0);
            _mesh.SetUVs(0, uvs);
            _mesh.RecalculateNormals();
        }

        public Mesh UpdateMesh(Vector2 offset, float noiseScale, float heightMultiplier)
        {
            for(int i = 0; i < vertexCount; i++)
            {
                Vector3 vert = vertices[i];
                vert.y = GetHeight(vert.x, vert.z, noiseScale, offset) * heightMultiplier;
                vertices[i] = vert;
            }

            _mesh.SetVertices(vertices);
            _mesh.RecalculateNormals();
            return _mesh;
        }

        private float GetHeight(float x, float y, float noiseScale, Vector2 offset)
        {
            if (noiseScale <= 0)
                noiseScale = 1f;

            return Mathf.PerlinNoise((x + offset.x) / noiseScale, (y + offset.y) / noiseScale) * 2 - 1;
        }

    }
}
