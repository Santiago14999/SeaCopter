using System;
using UnityEngine;

public class WaterWavesTile : MonoBehaviour, IComparable
{
    [HideInInspector] public int TileIndex;
    private Transform _transform;

    private void Awake() => _transform = transform;

    public void Translate(float x, float y, float z) => _transform.Translate(x, y, z);

    public int CompareTo(object obj)
    {
        WaterWavesTile rhs = obj as WaterWavesTile;
        if (rhs != null)
            return TileIndex.CompareTo(rhs.TileIndex);
        else
            throw new Exception("Can't compare with this object");
    }
}
