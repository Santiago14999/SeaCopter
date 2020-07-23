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
    private WaterWavesController _wavesController;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _currentMesh = new MeshData(_templateMesh);
    }

    private void Start() => _wavesController = WaterWavesController.Instance; 

    private void Update()
    {
        float xOffset = _appendPosition ? transform.position.x : 0;
        float zOffset = _appendPosition ? transform.position.z : 0;

        _meshFilter.sharedMesh = _currentMesh.UpdateMesh(_wavesController, xOffset, zOffset);
    }

    // Editor function
    public void GenerateWavesPreview(WaterWavesController controller)
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        if (filter == null)
        {
            Debug.LogError($"There is no MeshFilter attached to the {name}.");
            return;
        }    
        MeshData meshData = new MeshData(_templateMesh);
        float xOffset = _appendPosition ? transform.position.x : 0;
        float zOffset = _appendPosition ? transform.position.z : 0;
        
        filter.sharedMesh = meshData.UpdateMesh(controller, xOffset, zOffset);
    }
}
