using System;
using System.Numerics;

[Serializable]
public class PayloadColorData
{
    public BlobColor pink;
    public BlobColor green;
}

[Serializable]
public class BlobColor
{
    public BlobRect rect;
    public Vector2 centroid;
    public int area;
}

[Serializable]
public class BlobRect
{
    public int x;
    public int y;
    public int width;
    public int height;
    public int area;
}