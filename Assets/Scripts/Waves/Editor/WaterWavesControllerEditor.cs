using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Noise))]
public class WaterWavesControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Noise wavesController = (Noise)target;

        if (DrawDefaultInspector())
        {
            if (wavesController.showPreview)
                wavesController.GenerateMeshPreview();
        }
    }
}
