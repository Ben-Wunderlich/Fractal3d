using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    public float zoom = 1;
    public float maxHeight = 1;
    public int width = 10;
    public int height = 10;
    public Material material;
    private Vector2 offset = new Vector2(0, 0);
    // Start is called before the first frame update
    void Start()
    {
        Vector3 positionV3 = new Vector3(0, 0, 0);

        GameObject meshObject = new GameObject("terrain chunk");//GameObject.CreatePrimitive(PrimitiveType.Plane);
        MeshRenderer meshRenderer = meshObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = meshObject.AddComponent<MeshFilter>();
        meshRenderer.material = material;

        //my stuff
        MeshCollider meshCollider = meshObject.AddComponent<MeshCollider>();

        float[,] fractalHeightMap = FractalPoint.GenerateFractalMap(width, height, offset, 1, 2);

        Texture2D texture = TextureFromHeightMap(fractalHeightMap);
        meshRenderer.material.mainTexture = texture;
    }


    public static Texture2D TextureFromColourMap(Color[] colourMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colourMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heighMap)
    {
        int width = heighMap.GetLength(0);
        int height = heighMap.GetLength(1);


        Color[] colourMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, heighMap[x, y]);
            }
        }

        return TextureFromColourMap(colourMap, width, height);
    }

}
