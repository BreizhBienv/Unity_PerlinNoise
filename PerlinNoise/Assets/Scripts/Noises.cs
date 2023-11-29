using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public sealed class Noises
{
    private static Noises instance;

    public static Noises GetInstance()
    {
        if (instance == null)
            instance = new Noises();

        return instance;
    }

    // Param 1 & 2: Width and height of the texture in pixels.
    // Param 3 & 4: The origin of the sampled area in the plane.
    // Param 5:     The number of cycles of the basic noise pattern that are repeated over the width and height of the texture.
    public float[] GenerateNoiseMap(int pixWidth, int pixHeight, float xOrg, float yOrg, float scale)
    {
        float[] samples = new float[pixWidth * pixHeight];

        for (float y = 0.0f; y < pixHeight; ++y)
        {
            for (float x = 0.0f; x < pixWidth; ++x)
            {
                float xCoord = xOrg + x / pixWidth * scale;
                float yCoord = yOrg + y / pixHeight * scale;
                samples[(int)y * pixWidth + (int)x] = Mathf.PerlinNoise(xCoord, yCoord);
            }
        }

        return samples;
    }
    
    public Color[] GenerateNoiseColorMap(int pixWidth, int pixHeight, float xOrg, float yOrg, float scale)
    {
        Color[] samples = new Color[pixWidth * pixHeight];

        for (float y = 0.0f; y < pixHeight; ++y)
        {
            for (float x = 0.0f; x < pixWidth; ++x)
            {
                float xCoord = xOrg + x / pixWidth * scale;
                float yCoord = yOrg + y / pixHeight * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                samples[(int)y * pixWidth + (int)x] = new Color(sample, sample, sample);
            }
        }

        return samples;
    }
}
