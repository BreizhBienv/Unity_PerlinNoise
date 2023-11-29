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

    private void Awake()
    {
        // Set up the texture and a Color array to hold pixels during processing.
        noiseTex = new Texture2D(pixWidth, pixHeight);
        pix = new Color[noiseTex.width * noiseTex.height];
        rend.material.mainTexture = noiseTex;
    }

    private void Start()
    {
        float randX = Random.Range(-randRangeX, randRangeX);
        float randY = Random.Range(-randRangeY, randRangeY);

        pix = Noises.GetInstance().GenerateNoiseColorMap(
            pixWidth, pixHeight, 
            randX, randY,
            scale);

        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }
}
