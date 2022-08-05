using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRaycaster : AgentComponent
{
    public override void Init(Agent owner)
    {
        base.Init(owner);
    }

    public override void Tick()
    {
        base.Tick();

        if (!Input.GetMouseButtonDown(0))
            return;

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            Owner.TargetPosition = hit.point;
        }

    }
}
