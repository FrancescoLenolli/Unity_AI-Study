using UnityEngine;

public class FlockAgent : MonoBehaviour
{
    private FlockHandler handler;
    private float speed;
    private float rotationSpeed;

    public void Init(FlockHandler handler)
    {
        this.handler = handler;
        speed = Random.Range(handler.MinSpeed, handler.MaxSpeed);
        rotationSpeed = Random.Range(handler.MinRotationSpeed, handler.MaxRotationSpeed);
    }

    private void Update()
    {
        RaycastHit hit = new RaycastHit();
        Vector3 direction = Vector3.zero;

        if (handler.OutOfBounds(transform.position))
        {
            direction = handler.Bounds.center;
        }
        else if (Physics.Raycast(transform.position, transform.forward * 10, out hit))
        {
            direction = Vector3.Reflect(transform.forward, hit.normal);
        }

        if (direction != Vector3.zero)
            RotateTowards(handler.Bounds.center);

        if (Random.Range(0, 100) < 40)
        {
            speed = Random.Range(handler.MinSpeed, handler.MaxSpeed);
            rotationSpeed = Random.Range(handler.MinRotationSpeed, handler.MaxRotationSpeed);
            ApplyRules();
        }

        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    /*
     * Flock Behaviour Rules:
     * - Move towards average position of the group (sum of all positions/number of the group)
     * - Align with average direction of the group (sum of all directions/number of the group)
     * - Avoid crowding other group members
     */
    private void ApplyRules()
    {
        GameObject[] agents = handler.Agents.ToArray();

        Vector3 averageCentre = Vector3.zero;
        Vector3 averageAvoid = Vector3.zero;
        float averageSpeed = .01f;
        int groupSize = 0;
        float distance;

        foreach (GameObject agent in agents)
        {
            if (agent != gameObject)
            {
                distance = Vector3.Distance(agent.transform.position, transform.position);
                if (distance <= handler.NeighbourDistance)
                {
                    averageCentre += agent.transform.position;
                    groupSize++;

                    if (distance < 1f)
                    {
                        averageAvoid += transform.position - agent.transform.position;
                    }

                    FlockAgent flockAgent = agent.GetComponent<FlockAgent>();
                    averageSpeed += flockAgent.speed;
                }
            }

        }

        if (groupSize > 0)
        {
            averageCentre = averageCentre / groupSize + (handler.TargetPosition - transform.position);
            speed = averageSpeed / groupSize;
            RotateTowards(averageCentre + averageAvoid);
        }
        else
        {
            /*
             * If group size is zero, the agent is separated from the group.
             * I decided to have him rotate towards the target position instead
             * of wander around at random.
             */
            RotateTowards(handler.TargetPosition);
        }
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                rotationSpeed * Time.deltaTime);
        }
    }
}
