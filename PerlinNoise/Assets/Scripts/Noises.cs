using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class Noises
{
    public static int xRandRange = 10000;
    public static int yRandRange = 10000;

    // Param 1 & 2: Width and height of the texture in pixels.
    // Param 3 & 4: The origin of the sampled area in the plane.
    // Param 5:     The number of cycles of the basic noise pattern that are repeated over the width and height of the texture.
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed,  float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        if (scale <= 0)
            scale = 0.0001f;

        System.Random rand = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; ++i)
        {
            float offsetX = rand.Next(-xRandRange, xRandRange) + offset.x;
            float offsetY = rand.Next(-yRandRange, yRandRange) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        float[,] noiseMap = new float[mapWidth, mapHeight];

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2;
        float halfHeight = mapHeight / 2;

        for (int y = 0; y < mapHeight; ++y)
        {
            for (int x = 0;  x < mapWidth; ++x)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; ++i)
                {
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                    maxNoiseHeight = noiseHeight;
                else if (noiseHeight < minNoiseHeight)
                    minNoiseHeight = noiseHeight;

                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; ++y)
            for (int x = 0; x < mapWidth; ++x)
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);

        return noiseMap;
    }
}
