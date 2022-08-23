using System;
using UnityEngine;

public class AICore : MonoBehaviour
{
    public MovementType movementType;
    public Transform target;
    public float maxSpeed = 4.0f;

    private Vector3 startingPosition, startingRotation;
    private Vector3 movementValue = Vector3.zero;
    private float rotationValue = 0.0f;
    private bool canMove = false;
    private Steering steering = null;
    private Action<bool> movementAction = null;

    private void Awake()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation.eulerAngles;
        movementType.SetBaseValues(target, maxSpeed);
        movementType.InitData();

        bool isMovementDynamic = movementType.GetSteering().isDynamic;
        movementAction = isMovementDynamic ? (Action<bool>)MoveDynamic : (Action<bool>)MoveKinematic;
    }

    private void Update()
    {
        if (movementType && canMove)
            Move();

        if (Input.GetKeyDown(KeyCode.R))
            ResetCharacter();

        if (Input.GetKeyDown(KeyCode.P))
            PauseCharacter();

        if (Input.GetKeyDown(KeyCode.Return))
            StartCharacter();
    }

    private void Move()
    {
        steering = movementType.GetSteering();
        if (steering == null)
            return;

        transform.position += movementValue * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, rotationValue);

        bool isDirectionValid = Direction.IsValid(steering.direction);
        movementAction?.Invoke(isDirectionValid);
    }

    private void MoveDynamic(bool isDirectionValid)
    {
        if (isDirectionValid)
            movementValue += steering.direction * Time.deltaTime;
        else
            movementValue = Vector3.zero;

        rotationValue += steering.rotation * Time.deltaTime;

        if (movementValue.magnitude > maxSpeed)
        {
            movementValue.Normalize();
            movementValue *= maxSpeed;
        }
    }

    private void MoveKinematic(bool isDirectionValid)
    {
        if (isDirectionValid)
            movementValue = steering.direction;

        rotationValue = steering.rotation;
    }

    private void PauseCharacter()
    {
        canMove = !canMove;
    }

    private void StartCharacter()
    {
        canMove = true;
    }

    private void ResetCharacter()
    {
        transform.position = startingPosition;
        transform.rotation = Quaternion.Euler(startingRotation);
        canMove = false;
    }
}
