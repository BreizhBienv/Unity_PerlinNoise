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
                        mapGen.GenerateNoiseMap(mapGen.drawMode);
                        break;
                    case MapGenerator.AutoUpdateType.Height:
                        mapGen.GenerateHeightMap(mapGen.drawMode);
                        break;
                }
            }
        }

        if (GUILayout.Button("Generate Noise Map"))
        {
            mapGen.GenerateNoiseMap(mapGen.drawMode);
        }

        if (GUILayout.Button("Generate Height Map"))
        {
            mapGen.GenerateHeightMap(mapGen.drawMode);
        }
    }
}