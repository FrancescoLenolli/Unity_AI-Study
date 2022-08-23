using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Edge> edges = new List<Edge>();
    public Node path = null;
    public Node cameFrom;
    public GameObject gameobject;
    public float xPos;
    public float yPos;
    public float zPos;
    public float f;
    public float g;
    public float h;

    public Node(GameObject i)
    {
        gameobject = i;
        xPos = i.transform.position.x;
        yPos = i.transform.position.y;
        zPos = i.transform.position.z;
        path = null;
    }

    public GameObject getId()
    {
        return gameobject;
    }

}
