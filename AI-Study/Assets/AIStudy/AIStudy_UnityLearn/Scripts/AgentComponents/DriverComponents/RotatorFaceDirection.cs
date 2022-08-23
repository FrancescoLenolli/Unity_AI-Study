using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorFaceDirection : Rotator
{
    private Driver driver;

    public override void Init(Agent owner)
    {
        base.Init(owner);
        driver = owner.GetComponent<Driver>();
    }

    protected override void Rotate(Vector3 value)
    {
        FaceMoveDirection();
    }

    private void FaceMoveDirection()
    {
        Vector3 direction = driver.MoveDirection;

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
