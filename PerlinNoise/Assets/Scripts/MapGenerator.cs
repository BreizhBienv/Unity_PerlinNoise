using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Range(0, 5000)]
    public float randRangeX;
    [Range(0, 5000)]
    public float randRangeY;

    // Width and height of the texture in pixels.
    public int pixWidth;
    public int pixHeight;

    // The number of cycles of the basic noise pattern that are repeated
    // over the width and height of the texture.
    public float scale = 1.0F;

    private Texture2D noiseTex;
    private Color[] pix;
    public Renderer rend;

    public bool autoUpdate = true;

    public void GenerateMap()
    {
        float randX = Random.Range(-randRangeX, randRangeX);
        float randY = Random.Range(-randRangeY, randRangeY);

        pix = new Color[pixWidth * pixHeight];
        pix = Noises.GetInstance().GenerateNoiseColorMap(
            pixWidth, pixHeight,
            randX, randY,
            scale);

        Texture2D texture = new Texture2D(pixWidth, pixHeight);

        texture.SetPixels(pix);
        texture.Apply();


        if (rend != null)
            rend.sharedMaterial.mainTexture = texture;
    }
}
