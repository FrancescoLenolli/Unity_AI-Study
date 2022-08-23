using UnityEngine;
using System.Collections;

public class Edge
{
	public Node start;
	public Node end;
	
	public Edge(Node from, Node to)
	{
		start = from;
		end = to;
	}
}
