using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Transform pointObj;
    public Transform parent;
    // Start is called before the first frame update
    public float maxSize = 2f;
    public float increments = 0.1f;
    public float expansion = 10f;

    private void MakePoint(float x, float y, float z, Transform parentObj)
    {

        Instantiate(pointObj, new Vector3(x*expansion, y*expansion, z*expansion), Quaternion.identity, parentObj);
    }

    void Start()
    {
        Transform parentObj = Instantiate(parent);
        for (float i = -maxSize; i < maxSize; i+= increments)
        {
         //   FractalPoint.FractalPointValue(i, 0);
            for(float j = -maxSize; j < maxSize; j+= increments)
            {
                MakePoint(i, FractalPoint.FractalPointValue(i, j), j, parentObj);
            }
        }
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
