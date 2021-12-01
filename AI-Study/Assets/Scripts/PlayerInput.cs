using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float movementSpeed = 1.5f;
    public float rotationSpeed = 1.5f;

    private Vector3 inputValue;
    private float rotationDirection;

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        inputValue = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += movementSpeed * Time.deltaTime * inputValue;
    }

    private void Rotate()
    {
        rotationDirection = Input.GetKey(KeyCode.Q) ? 1 : Input.GetKey(KeyCode.E) ? -1 : 0;
        float newRotationZ = transform.rotation.eulerAngles.z + rotationDirection * rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, newRotationZ);
    }
    
}
