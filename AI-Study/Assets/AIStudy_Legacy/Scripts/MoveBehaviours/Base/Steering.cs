using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering
{
    public Vector3 direction;
    public float rotation;
    public bool isDynamic;

    public Steering(bool isDynamic)
    {
        this.isDynamic = isDynamic;
        direction = Vector3.zero;
        rotation = 0;
    }
}
