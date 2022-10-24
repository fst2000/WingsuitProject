using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing
{
    Vector3 localNormal = Vector3.up;
    Vector3 localPosition = Vector3.zero;
    float sizeX, sizeY;
   public Wing(Vector3 localNormal, Vector3 localPosition, float sizeX, float sizeY)
    {
        this.localNormal = localNormal;
        this.localPosition = localPosition;
        this.sizeX = sizeX;
        this.sizeY = sizeY;
    }
    public float GetSquare()
    {
        return sizeX * sizeY;
    }
    public Vector3 GetLocalPosition()
    {
        return localPosition;
    }
    public Vector3 GetLocalNormal()
    {
        return localNormal;
    }
}
