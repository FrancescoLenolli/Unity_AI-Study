using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverNavMeshHide : DriverNavMesh
{
    private GameObject[] hidingSpots;

    public override void Init(Agent owner)
    {
        base.Init(owner);
        hidingSpots = GameObject.FindGameObjectsWithTag("Hide Spot");
    }

    public override void Tick()
    {
        base.Tick();

        if (CanSeeTarget())
        {
            SetDestinationPosition(GetTargetPosition());
        }
    }

    private Vector3 GetTargetPosition()
    {
        return CleverHide();
    }

    private Vector3 Hide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        for (int i = 0; i < hidingSpots.Length; ++i)
        {
            Transform hidingSpot = hidingSpots[i].transform;
            Vector3 hideDirection =  hidingSpot.position - Owner.Target.transform.position;
            Vector3 hidePosition = hidingSpot.position + hideDirection.normalized * 10f;

            float hidePositionDistance = Vector3.Distance(Owner.transform.position, hidePosition);

            if(hidePositionDistance < distance)
            {
                chosenSpot = hidePosition;
                distance = hidePositionDistance;
            }
        }

        return chosenSpot;
    }

    private Vector3 CleverHide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosedDirection = Vector3.zero;
        GameObject chosenObject = hidingSpots[0];

        for (int i = 0; i < hidingSpots.Length; ++i)
        {
            Transform hidingSpot = hidingSpots[i].transform;
            Vector3 hideDirection = hidingSpot.position - Owner.Target.transform.position;
            Vector3 hidePosition = hidingSpot.position + hideDirection.normalized * 10f;

            float hidePositionDistance = Vector3.Distance(Owner.transform.position, hidePosition);

            if (hidePositionDistance < distance)
            {
                chosenSpot = hidePosition;
                chosedDirection = hideDirection;
                chosenObject = hidingSpot.gameObject;
                distance = hidePositionDistance;
            }
        }

        Collider hideCollider = chosenObject.GetComponent<Collider>();
        Ray backRay = new Ray(chosenSpot, -chosedDirection.normalized);
        RaycastHit info;
        float rayDistance = 250f;
        hideCollider.Raycast(backRay, out info, rayDistance);

        return info.point + chosedDirection.normalized;
    }

    private bool CanSeeTarget()
    {
        RaycastHit info;
        Vector3 offset = new Vector3(0f, .5f, 0f);
        Vector3 rayToTarget = (target.transform.position + offset) - (Owner.transform.position + offset);
        if(Physics.Raycast(Owner.transform.position + offset, rayToTarget, out info))
        {
            if(info.transform.gameObject == target.gameObject)
            {
                return true;
            }
        }

        return false;
    }
}
