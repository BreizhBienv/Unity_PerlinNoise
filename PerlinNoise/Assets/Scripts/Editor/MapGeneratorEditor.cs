using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;

        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                switch (mapGen.autoUpdateType)
                {
                    case MapGenerator.AutoUpdateType.Noise:
                        mapGen.GenerateNoiseMap();
                        break;
                    case MapGenerator.AutoUpdateType.Height:
                        mapGen.GenerateHeightMap();
                        break;
                }

            }
        }

        if (GUILayout.Button("Generate Noise Map"))
        {
            mapGen.GenerateNoiseMap();
        }

        if (GUILayout.Button("Generate Height Map"))
        {
            mapGen.GenerateHeightMap();
        }
    }
}