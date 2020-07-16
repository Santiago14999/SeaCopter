using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MeshData
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
        for (int i = 0; i < _vertexCount; i++)
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