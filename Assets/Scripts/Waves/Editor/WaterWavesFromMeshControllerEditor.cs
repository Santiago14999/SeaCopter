using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaterWavesFromMeshController))]
public class WaterWavesFromMeshControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WaterWavesFromMeshController wavesController = (WaterWavesFromMeshController)target;

        if (DrawDefaultInspector())
        {
            if (wavesController.showPreview)
                wavesController.GenerateMeshPreview();
        }
    }
}
