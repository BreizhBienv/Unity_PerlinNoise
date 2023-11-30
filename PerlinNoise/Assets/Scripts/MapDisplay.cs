using System.Data.Common;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer rend;
    public TerrainData terrainData;

    public void DrawNoiseMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colourMap = new Color[width * height];
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }
        texture.SetPixels(colourMap);
        texture.Apply();

        rend.sharedMaterial.mainTexture = texture;
        rend.transform.localScale = new Vector3(width, 1, height);
    }

    public void DrawHeightMap(float[,] noiseMap, int mapWidth, int mapHeight, int terrainHeight)
    {
        terrainData.size = new Vector3(mapWidth, terrainHeight, mapHeight);

        float[,] heightMap = new float[mapWidth, mapHeight];
        for (int y = 0; y < mapHeight; ++y)
        {
            for (int z = 0; z < mapWidth; ++z)
            {
                heightMap[z, y] = noiseMap[z, y];
            }
        }

        terrainData.SetHeights(0, 0, heightMap);
    }
}