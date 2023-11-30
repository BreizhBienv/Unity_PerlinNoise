using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Biomes : MonoBehaviour
{
    [Serializable]
    public struct Biome
    {
        public string name;
        public Color color;
        [Range(0f, 1f)]
        public float value;
    }

    public List<Biome> biomes = new List<Biome>();
}
