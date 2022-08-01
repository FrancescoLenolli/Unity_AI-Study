using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform weapon = null;
    [SerializeField] private Transform muzzle = null;
    [SerializeField] private Projectile projectilePrefab = null;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private bool canShoot = true;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            Shoot();
        }

        Rotate(Input.GetAxis("Vertical"));
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
}
