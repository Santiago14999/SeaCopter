using System;
using UnityEngine;

public class WaterWavesTile : MonoBehaviour, IComparable
{
    [HideInInspector] public int TileIndex;

    public void Translate(float x, float y, float z) => transform.Translate(x, y, z);

    public int CompareTo(object obj)
    {
        WaterWavesTile rhs = obj as WaterWavesTile;
        if (rhs != null)
            return TileIndex.CompareTo(rhs.TileIndex);
        else
            throw new Exception("Can't compare with this object");
    }
}
