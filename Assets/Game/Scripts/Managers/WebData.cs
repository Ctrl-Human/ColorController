using System;
using UnityEngine;

[Serializable]
public class WebData
{
    public Payload payload;
    public string from;
    public long time;
}

[Serializable]
public class Payload
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
