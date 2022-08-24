using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 // 3 years of game dev without knowing this existed, wtf.
public class RaycastBounce : MonoBehaviour
{
    private void Update()
    {
        RaycastHit info;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out info, 10);
        Debug.DrawRay(transform.position, transform.forward * 10, Color.green);

        if (!info.collider)
            return;

        Vector3 reflectedPosition = Vector3.Reflect(transform.forward, info.normal);
        Debug.DrawRay(info.point, reflectedPosition * 10, Color.red);
    }
}
