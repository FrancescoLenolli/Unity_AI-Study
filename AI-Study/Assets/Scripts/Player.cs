using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private TransformVelocity transformVelocity;

    private void Awake()
    {
        transformVelocity = GetComponent<TransformVelocity>();
    }

    public Vector3 GetVelocity()
    {
        return transformVelocity.Value;
    }
}
