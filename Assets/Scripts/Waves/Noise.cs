using UnityEngine;

public class Noise : MonoBehaviour
{
    public static void GenerateNoiseMap(float[,] arrayToFill, int mapWidth, int mapHeight, float scale, Vector2 offset)
	{
		if (scale <= 0)
			scale = 0.0001f;

		float halfWidth = mapWidth / 2f;
		float halfHeight = mapHeight / 2f;

		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				float sampleX = (x - halfWidth) / scale + offset.x;
				float sampleY = (y - halfHeight) / scale + offset.y;

				float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

				arrayToFill[x, y] = perlinValue;
			}
		}
	}
}
