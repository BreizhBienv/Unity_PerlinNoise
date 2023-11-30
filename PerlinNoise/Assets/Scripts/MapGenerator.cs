using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{   // Width and height of the texture in pixels.
    public int mapWidth;
    public int mapHeight;

    // The number of cycles of the basic noise pattern that are repeated
    // over the width and height of the texture.
    public float scale = 1.0F;

    [Range(0, 1)]
    public float persistance;
    [Range (0, 10)]
    public int octaves;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate = true;

    public void GenerateMap()
    {
        float[,] noiseMap = Noises.GenerateNoiseMap(mapWidth, mapHeight, seed, scale, octaves, persistance, lacunarity, offset);

        MapDisplay display = FindAnyObjectByType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }

    private void OnValidate()
    {
        if (mapWidth < 1)
            mapWidth = 1;

        if (mapHeight < 1)
            mapHeight = 1;

        if (octaves < 0)
            octaves = 0;

        if (lacunarity < 1)
            lacunarity = 1;
    }
}
