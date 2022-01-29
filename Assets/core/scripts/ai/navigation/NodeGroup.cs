using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeGroup
{
    private Node[] nodes;

    public NodeGroup(Node[] nodes)
    {
        this.nodes = nodes;
    }

    public Node[] FindPath(Vector3 startPosition, Vector3 endPosition){

        Node start = FindClosestNodeToPosition(startPosition);
        Node stop = FindClosestNodeToPosition(endPosition);

        if(start == null || stop == null){
            return new Node[0];
        }

        return FindPath(start, stop);
    }

    public Node[] FindPath(Node startNode, Node targetNode)
    {
        if (startNode == targetNode) return new Node[] { startNode };
        if (startNode.IsNeighborOfNode(targetNode))
        {
            return new Node[] { startNode, targetNode };
        }

        List<Node> openNodes = new List<Node>();
        HashSet<Node> closedNodes = new HashSet<Node>();

        openNodes.Add(startNode);
        while (openNodes.Count > 0)
        {
            Node current = openNodes[0];

            for (int i = 1; i < openNodes.Count; i++)
            {
                if (openNodes[i].FCost <= current.FCost)
                {
                    if (openNodes[i].hCost < current.hCost)
                        current = openNodes[i];
                }
            }

            openNodes.Remove(current);
            closedNodes.Add(current);

            if (current == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            for (int i = 0; i < current.neighbors.Length; i++)
            {
                Node neighbor = current.neighbors[i];
                if (closedNodes.Contains(neighbor))
                    continue;

                float newCostToNeighbor = current.gCost + GetDistance(current, neighbor);
                if (newCostToNeighbor < neighbor.gCost || !openNodes.Contains(neighbor))
                {

                    neighbor.gCost = newCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = current;

                    if (!openNodes.Contains(neighbor))
                    {
                        openNodes.Add(neighbor);
                    }
                }
            }
        }
        return new Node[0];
    }

    Node[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Add(startNode);
        path.Reverse();
        return path.ToArray();
    }

    public float GetDistance(Node nodeA, Node nodeB)
    {
        return Vector3.Distance(nodeA.position, nodeB.position);
    }

    public Node FindClosestNodeToPosition(Vector3 position){

        if(nodes.Length == 0) return null;
        if(nodes.Length == 1) return nodes[0];

        float closestDist =  Vector3.Distance(position, nodes[0].position);
        Node closestNode = nodes[0];

        for (int i = 1; i < nodes.Length; i++)
        {
            float dist = Vector3.Distance(position, nodes[i].position);
            if(dist < closestDist){
                closestNode = nodes[i];
                closestDist = dist;
            }
        }
        return closestNode;
    }
}
