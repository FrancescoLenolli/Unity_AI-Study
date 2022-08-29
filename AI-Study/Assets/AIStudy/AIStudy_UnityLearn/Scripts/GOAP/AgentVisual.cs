using UnityEngine;

[ExecuteInEditMode]
public class AgentVisual : MonoBehaviour
{
    public GOAP.Agent thisAgent;

    // Start is called before the first frame update
    void Start()
    {
        thisAgent = GetComponent<GOAP.Agent>();
    }
}
