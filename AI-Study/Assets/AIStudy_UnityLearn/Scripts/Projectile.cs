using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float totalLifeSpan = 1f;
    public float mass = 10;
    public float force = 10000;
    public float gravity = -9.8066f;
    public float acceleration = 0;
    public float gravitationalPull = 0;
    public float speedZ = 0;
    public float speedY = 0;
    private float lifeSpan;

    private void Update()
    {
        lifeSpan -= Time.deltaTime;

        if(lifeSpan <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        //acceleration = force / mass;
        //gravitationalPull = gravity / mass;

        //speedY += gravitationalPull * Time.deltaTime;
        //speedZ += acceleration * Time.deltaTime;

        //transform.Translate(0, speedY, speedZ);
    }

    public void Init(Transform origin)
    {
        transform.position = origin.position;
        transform.rotation = origin.rotation;
        lifeSpan = totalLifeSpan;
    }
}
