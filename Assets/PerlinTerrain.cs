using UnityEngine;

public class PerlinTerrain : MonoBehaviour
{
    public int depth = 20;
    private int width =  256;
    private int height = 256;
    public float scale =  20;

    public float offsetX = 100f;
    public float offsetY = 100f;

    private void Start()
    {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
    }

    private void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenTerrain(terrain.terrainData);

        offsetX += Time.deltaTime * .2f;
    }

    TerrainData GenTerrain (TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenHeights());
        return terrainData;
    }

    float[,] GenHeights()
    {
        float[,] heights = new float[width, height];
        for (uint x = 0; x < width; x++)
        {
            for (uint y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight((int)x, (int)y);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width  * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;
        return Mathf.PerlinNoise(xCoord, yCoord);
    }

}
