using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaterWavesController))]
public class WaterWavesControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WaterWavesController wavesController = (WaterWavesController)target;

        if (DrawDefaultInspector())
        {
            if (wavesController.showPreview)
                wavesController.GenerateMeshPreview();
        }
    }
}
