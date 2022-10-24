using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing
{
    Vector3 normal = Vector3.up;
    Vector3 position = Vector3.zero;
   public Wing(Vector3 normal, Vector3 position)
    {
        this.normal = normal;
        this.position = position;
    }
}
