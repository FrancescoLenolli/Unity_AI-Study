using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfind
{
	List<Edge>	edges = new List<Edge>();
	List<Node>	nodes = new List<Node>();
	List<Node> pathList = new List<Node>();   
	
	public Pathfind(){}
	
	public void AddNode(GameObject gameobject, bool removeRenderer = true, bool removeCollider = true, bool removeId = true)
	{
		Node node = new Node(gameobject);
		nodes.Add(node);
		
		//remove colliders and mesh renderer
		if(removeCollider)
			GameObject.Destroy(gameobject.GetComponent<Collider>());
		if(removeRenderer)
			GameObject.Destroy(gameobject.GetComponent<Renderer>());
		if(removeId)
		{
			TextMesh[] textms = gameobject.GetComponentsInChildren<TextMesh>();

        	foreach (TextMesh tm in textms)	
				GameObject.Destroy(tm.gameObject);
		}
	}
	
	public void AddEdge(GameObject fromNode, GameObject toNode)
	{
		Node from = FindNode(fromNode);
		Node to = FindNode(toNode);
		
		if(from != null && to != null)
		{
			Edge edge = new Edge(from, to);
			edges.Add(edge);
			from.edges.Add(edge);
		}	
	}

	public List<Node> GetPath()
    {
		return pathList;
    }
	
	public int GetPathLength()
	{
		return pathList.Count;	
	}
	
	public GameObject GetPathPoint(int index)
	{
		return pathList[index].gameobject;
	}

	public GameObject GetClosestNode(GameObject target)
    {
		Node closestNode = nodes[0];
		foreach(Node node in pathList)
        {
			float currentDistance = Vector3.Distance(target.transform.position, closestNode.gameobject.transform.position);
			float newDistance = Vector3.Distance(target.transform.position, node.gameobject.transform.position);

			if (newDistance < currentDistance)
				closestNode = node;
		}

		return closestNode.gameobject;
    }
	
	public void PrintPath()
	{
		foreach(Node n in pathList)
		{	
			Debug.Log(n.gameobject.name);	
		}
	}
	
	
	public bool AStar(GameObject startPosition, GameObject endPosition)
	{
	  	Node start = FindNode(startPosition);
	  	Node end = FindNode(endPosition);
	  
	  	if(start == null || end == null)
	  	{
	  		return false;	
	  	}
	  	
	  	List<Node>	open = new List<Node>();
	  	List<Node>	closed = new List<Node>();
	  	float tentative_g_score= 0;
	  	bool tentative_is_better;
	  	
	  	start.g = 0;
	  	start.h = Distance(start,end);
	  	start.f = start.h;
	  	open.Add(start);
	  	
	  	while(open.Count > 0)
	  	{
	  		int i = LowestF(open);
			Node thisnode = open[i];
			if(thisnode.gameobject == endPosition)  //path found
			{
				ReconstructPath(start,end);
				return true;	
			} 	
			
			open.RemoveAt(i);
			closed.Add(thisnode);
			
			Node neighbour;
			foreach(Edge e in thisnode.edges)
			{
				neighbour = e.end;
				neighbour.g = thisnode.g + Distance(thisnode,neighbour);
				
				if (closed.IndexOf(neighbour) > -1)
					continue;
				
				tentative_g_score = thisnode.g + Distance(thisnode, neighbour);
				
				if( open.IndexOf(neighbour) == -1 )
				{
					open.Add(neighbour);
					tentative_is_better = true;	
				}
				else if (tentative_g_score < neighbour.g)
				{
					tentative_is_better = true;	
				}
				else
					tentative_is_better = false;
					
				if(tentative_is_better)
				{
					neighbour.cameFrom = thisnode;
					neighbour.g = tentative_g_score;
					neighbour.h = Distance(thisnode,end);
					neighbour.f = neighbour.g + neighbour.h;	
				}
			}
  	
	  	}
		
		return false;	
	}
	
	public void ReconstructPath(Node startNode, Node endNode)
	{
		pathList.Clear();
		pathList.Add(endNode);
		
		Node node = endNode.cameFrom;
		while(node != startNode && node != null)
		{
			pathList.Insert(0,node);
			node = node.cameFrom;	
		}
		pathList.Insert(0,startNode);
	}

	public void DebugDraw()
	{
		//draw edges
		for (int i = 0; i < edges.Count; i++)
		{
			Debug.DrawLine(edges[i].start.gameobject.transform.position, edges[i].end.gameobject.transform.position, Color.red);

		}
		//draw directions
		for (int i = 0; i < edges.Count; i++)
		{
			Vector3 to = (edges[i].start.gameobject.transform.position - edges[i].end.gameobject.transform.position) * 0.05f;
			Debug.DrawRay(edges[i].end.gameobject.transform.position, to, Color.blue);
		}
	}

	private Node FindNode(GameObject gameobject)
	{
		foreach (Node n in nodes)
		{
			if (n.getId() == gameobject)
				return n;
		}
		return null;
	}

	private float Distance(Node a, Node b)
    {
	  float x = a.xPos - b.xPos;
	  float y = a.yPos - b.yPos;
	  float z = a.zPos - b.zPos;
	  float distance = x*x + y*y + z*z;

	  return distance;
    }

    private int LowestF(List<Node> nodes)
    {
	  float lowestf = 0;
	  int count = 0;
	  int iteratorCount = 0;
	  	  
	  for (int i = 0; i < nodes.Count; i++)
	  {
	  	if(i == 0)
	  	{	
	  		lowestf = nodes[i].f;
	  		iteratorCount = count;
	  	}
	  	else if( nodes[i].f <= lowestf )
	  	{
	  		lowestf = nodes[i].f;
	  		iteratorCount = count;	
	  	}
	  	count++;
	  }
	  return iteratorCount;
    }
}
