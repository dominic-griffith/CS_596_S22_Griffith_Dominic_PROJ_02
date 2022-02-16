using UnityEditor;                                  // Editor need removed class
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]                                 // Need to run in EditMode

public class PGCTerrain : MonoBehaviour
{

    public Terrain terrain;
    public TerrainData terrainData;

    public Vector2 randomHeightRange = new Vector2(0, 0.1f);
    public Vector3 heightMapScale = new Vector3(1, 1, 1);
    public float hillVariance = 20f;
    public Texture2D heightMapImage;
    public Vector2 offset = new Vector2(2f, 2f);

    private void OnEnable()                         // Like Awake but for editor
    {
        terrain = this.GetComponent<Terrain>();
        terrainData = Terrain.activeTerrain.terrainData;
    }

    public void LoadTexture()
    {
        float[,] heightMap;
        heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrainData.heightmapResolution; z++)
            {
                heightMap[x, z] = heightMapImage.GetPixel((int)(x * heightMapScale.x), (int)(z * heightMapScale.z)).grayscale * heightMapScale.y;
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }

    public void RandomTerrain()
    {
        float[,] heightMap;
        heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrainData.heightmapResolution; z++)
            {
                heightMap[x, z] = UnityEngine.Random.Range(randomHeightRange.x/10, randomHeightRange.y/10);
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }


    public void TrigFunction()
    {
        float[,] heightMap;
        heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrainData.heightmapResolution; z++)
            {
                heightMap[x, z] = ((Mathf.Cos(x/10f) * Mathf.Sin(z / 10f)) / terrainData.heightmapResolution * hillVariance);
                //heightMap[x, z] = (0.5f*(Mathf.Sin((x+z) / 10f) - Mathf.Sin((x-z) / 10f)) / terrainData.heightmapResolution * hillVariance);
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }


    public void PerlinNoise()
    {
        float[,] heightMap;
        heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrainData.heightmapResolution; z++)
            {
                //heightMap[x, z] = Mathf.PerlinNoise(((float)x / (float)terrainData.heightmapResolution) * hillVariance, ((float)z / (float)terrainData.heightmapResolution) * hillVariance) / 10f;
                heightMap[x, z] = Mathf.PerlinNoise((offset.x + (float)x / (float)terrainData.heightmapResolution) * hillVariance, offset.y + ((float)z / (float)terrainData.heightmapResolution) * hillVariance) / 10f;
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }
}
