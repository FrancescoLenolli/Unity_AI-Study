using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct Link
{
    public enum Direction { Unidirectional, Bidirectional};
    public GameObject node1;
    public GameObject node2;
    public Direction direction;
}

public class PathHandler : MonoBehaviour
{
    [SerializeField] private List<Waypoint> waypoints = null;
    [SerializeField] private List<Link> links = null;
    [SerializeField] private Pathfind pathfind = new Pathfind();
    [SerializeField] private PathFollower pathFollower = null;
    [SerializeField] private GameObject targetPosition;

    private void Start()
    {
        if (waypoints.Count == 0)
            return;

        waypoints.ForEach(waypoint => pathfind.AddNode(waypoint.gameObject));
        links.ForEach(link =>
        {
            pathfind.AddEdge(link.node1, link.node2);
            if (link.direction == Link.Direction.Bidirectional)
                pathfind.AddEdge(link.node2, link.node1);
        });
    }

    private void Update()
    {
        pathfind.DebugDraw();
    }

    [ContextMenu("Set Path")]
    public void SetPath()
    {
        if (pathfind.AStar(pathfind.GetClosestNode(pathFollower.owner.gameObject), targetPosition))
        {
            Transform[] nodePositions = pathfind.GetPath().Select(node => node.gameobject.transform).ToArray();
            pathFollower.SetPath(nodePositions);
        }
    }
}
