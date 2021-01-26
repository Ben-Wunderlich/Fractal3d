using UnityEngine;

public static class FractalPoint
{
    private static readonly int boundary = 20;
    private static readonly float cVal = 0.4f;
    private static readonly float expansion = 1f;
    private static readonly float darkness = 15;

    private static float RangeScale(float val, float Tmin, float Tmax, float Rmin, float Rmax)
    {
        float temp = (val - Rmin) / (Rmax - Rmin);
        return temp * (Tmax - Tmin) + Tmin;
    }

    private static bool WithinBounds(float x, float y)
    {
        //return (squareNum(x) + squareNum(y) < 4);//circle
        //return (x < xMax && x > xMin) && (y < yMax && y > yMin);//field of view, stupid me
        return (x < boundary && x > -boundary) && (y < boundary && y > -boundary);//enlightened version
    }

    private static bool KeepIterating(int i, float x, float y)
    {
        return ((i < darkness) && WithinBounds(x, y));
    }

    private static float SquareNum(float num)
    {
        return num * num;
    }
    
    public static float FractalPointValue(float x, float y)
    {
            float xTemp;

            int i = 0;
            while (i == 0 || KeepIterating(i, x, y))
            {
                xTemp = SquareNum(x) - SquareNum(y);
                y = (2 * x * y) + cVal;
                x = xTemp + cVal;

                //user defined expression here
                //xTemp = x;yTemp =y;

                //x = x / Mathf.Cos(x);
               // y = 1/Mathf.Sin(y);
                
            //if (x != 0 && y != 0)

                //if(x != 0)
                //{
                //    x = 1 / x;
                //    y = y / x;
                //}

                x *= expansion;
                y *= expansion;
                i++;
            }
            float result = RangeScale(i, 0, 1, 0, darkness);
            //Debug.Log(result);
            return result;
    }


    public static float[,] GenerateFractalMap(int mapWidth, int mapHeight, Vector2 offset, float zoom, float maxHeight)
    {
        float[,] fractalMap = new float[mapWidth, mapHeight];

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float xFeed = (offset.x + x) * zoom;
                float yFeed = (offset.y + y) * zoom;

                float pointValue = FractalPointValue(xFeed, yFeed);

                pointValue *= maxHeight;
                fractalMap[x, y] = pointValue;
            }
        }
        return fractalMap;
    }
}
