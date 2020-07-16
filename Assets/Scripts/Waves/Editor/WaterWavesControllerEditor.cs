using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaterWavesController))]
public class WaterWavesControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("GeneratePlanes"))
        {
            var wavesController = target as WaterWavesController;
            var meshControllers = FindObjectsOfType<WaterWavesFromMeshController>();
            foreach (var meshController in meshControllers)
                meshController.GenerateWavesPreview(wavesController);
        }
    }
}
