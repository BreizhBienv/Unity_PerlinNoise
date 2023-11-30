using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum AutoUpdateType
    {
        Noise,
        Height
    }

    [Header("Map Size"), Tooltip("Width and height of the texture in pixels.")]
    public int mapWidth;
    public int mapHeight;
    public int terrainHeight;

    [Header("Map Scale"), Tooltip("The number of cycles of the basic noise pattern that are repeated over the width and height of the texture.")]
    public float scale = 1.0F;

    [Header("Map Noise")]
    
    [Range(0, 1), Tooltip("The roughness of the terrain. Lower values result in smoother terrain. Higher values result in more rugged terrain.")]
    public float persistance;

    [Range(1, 8), Tooltip("The number of noise maps that are layered together.")]
    public int octaves;

    [Range(1, 8), Tooltip("The frequency of the noise maps.")]
    public float lacunarity;

    [Header("Map Seed")]
    public int seed;
    public Vector2 offset;

    [Header("Comfort")]
    [Tooltip("If true, the map will be generated automatically when the values are changed.")]
    public bool autoUpdate = true;
    [Tooltip("The type of map that will be generated automatically.")]
    public AutoUpdateType autoUpdateType;

    public void GenerateNoiseMap()
    {
        float[,] noiseMap = Noises.GenerateNoiseMap(mapWidth, mapHeight, seed, scale, octaves, persistance, lacunarity, offset);

        MapDisplay display = FindAnyObjectByType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }

    public float[,] GetNoiseMap()
    {
        return Noises.GenerateNoiseMap(mapWidth, mapHeight, seed, scale, octaves, persistance, lacunarity, offset);
    }

    public void GenerateHeightMap()
    {
        MapDisplay display = FindAnyObjectByType<MapDisplay>();
        display.DrawHeightMap(GetNoiseMap(), mapWidth, mapHeight, terrainHeight);
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
