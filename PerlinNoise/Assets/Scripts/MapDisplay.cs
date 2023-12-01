using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer rend;
    public Terrain terrain;

    public Color GetColor(float sample)
    {
        Biomes biomesComp = GetComponent<Biomes>();
        Color currentBiome = Color.black;

        biomesComp.biomes.Sort((s1, s2) => s1.value.CompareTo(s2.value));
        foreach (var biome in biomesComp.biomes)
        {
            if (sample <= biome.value)
            {
                currentBiome = biome.color;
                break;
            }
        }

        return currentBiome;
    }

    public Texture2D DrawTextureNoise(float[,] noiseMap, MapGenerator.DrawType drawType)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colourMap = new Color[width * height];
        for (int y = 0; y < height; ++y)
            for (int x = 0; x < width; ++x)
            {
                Color color = Color.black;

                if (drawType == MapGenerator.DrawType.Color)
                    color = GetColor(noiseMap[y, x]);
                else if (drawType == MapGenerator.DrawType.Basic)
                    color = Color.Lerp(Color.white, Color.black, noiseMap[y, x]);

                colourMap[y * width + x] = color;
            }

        texture.SetPixels(colourMap);
        texture.Apply();

        return texture;
    }

    public void DrawNoiseMap(float[,] noiseMap, MapGenerator.DrawType drawType)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        rend.sharedMaterial.mainTexture = DrawTextureNoise(noiseMap, drawType);
        rend.transform.localScale = new Vector3(width, 1, height);
    }
    
    public void DrawHeightMap(float[,] noiseMap, int terrainHeight, MapGenerator.DrawType drawType)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        terrain.terrainData.heightmapResolution = width;
        terrain.terrainData.size = new Vector3(width, terrainHeight, height);
        terrain.terrainData.SetHeights(0, 0, noiseMap);
        terrain.materialTemplate.mainTexture = DrawTextureNoise(noiseMap, drawType);
    }
}