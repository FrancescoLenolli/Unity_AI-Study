using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverPlayer : Driver
{
    protected override Vector3 GetMoveDirection()
    {
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        return new Vector3(xAxis, 0, yAxis);
    }
}
