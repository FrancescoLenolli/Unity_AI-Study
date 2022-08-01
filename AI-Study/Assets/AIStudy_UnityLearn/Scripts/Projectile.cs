using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float totalLifeSpan = 1f;
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
    }

    public void Init(Transform origin)
    {
        transform.position = origin.position;
        transform.rotation = origin.rotation;
        lifeSpan = totalLifeSpan;
    }
}
