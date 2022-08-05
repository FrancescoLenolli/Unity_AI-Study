using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverPlayer : Driver
{
    protected override Vector3 GetMoveDirection()
    {
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(xAxis, 0, yAxis);

        return moveDirection;
    }

    protected override void Face(Vector3 target)
    {
        Vector3 direction = GetMoveDirection();

        if(direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
