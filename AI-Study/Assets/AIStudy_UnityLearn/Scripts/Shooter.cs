using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : AgentComponent
{
    [SerializeField] private Transform weapon = null;
    [SerializeField] private Transform muzzle = null;
    [SerializeField] private Projectile projectilePrefab = null;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private bool canShoot = true;
    [SerializeField] private bool autopilot = false;
    public Transform target = null;

    public override void Tick()
    {

        Targeter targeter = (Targeter)owner.GetAgentComponent(typeof(Targeter));
        target = targeter.Target;

        if(canShoot)
        {
            if(!autopilot)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    Shoot();
                }
            }
            else
            {

            }
        }

        if(!autopilot)
        {
            Rotate(Input.GetAxis("Vertical"));
        }
    }

    private void Shoot()
    {
        Projectile projectile = Instantiate(projectilePrefab);

        projectile.Init(muzzle);
    }

    private void Rotate(float input)
    {
        weapon.Rotate(input * rotationSpeed * Time.deltaTime, 0, 0);
    }

    private float? CalculateAngle(bool low)
    {
        return null;
    }
}
