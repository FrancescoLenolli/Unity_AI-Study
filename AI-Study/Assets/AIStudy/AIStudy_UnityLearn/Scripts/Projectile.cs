using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float totalLifeSpan = 1f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private new Rigidbody rigidbody = null;

    private float lifeSpan;
    private Transform owner;

    public float Speed { get => speed; }

    private void Update()
    {
        transform.forward = rigidbody.velocity;

        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0f)
        {
            DoDestroy();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.transform.root != owner)
        {
            DoDestroy();
        }
    }

    public void Init(Transform owner, Transform origin)
    {
        this.owner = owner;
        transform.position = origin.position;
        transform.rotation = origin.rotation;
        lifeSpan = totalLifeSpan;
        rigidbody.velocity = speed * origin.forward;
    }

    private void DoDestroy()
    {
        Destroy(gameObject);
    }
}
