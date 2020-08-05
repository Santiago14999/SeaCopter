using UnityEngine;

public static class OffScreenCalculator
{
    public static Vector3 GetRandomPositionFromOrigin(Transform origin, float distance)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = new Vector3(randomDirection.x, 0, randomDirection.y);
        spawnPosition = origin.position + spawnPosition * distance;

        return spawnPosition;
    }
}
