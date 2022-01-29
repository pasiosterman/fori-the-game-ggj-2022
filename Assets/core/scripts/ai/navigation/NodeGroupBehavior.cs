using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGroupBehavior : MonoBehaviour
{
    public NodeBehavior startNode;
    public NodeBehavior endNode;

    private NodeGroup nodeGroup;

    // [ContextMenu("print path")]
    // public void PrintPath(){

    //     NodeGroup nodeGroup = CreateNodeGroup();
        
    //     Node start = nodeGroup.GetNodeForNodeBehavior(startNode);
    //     Node end = nodeGroup.GetNodeForNodeBehavior(endNode);

    //     Node[] path = nodeGroup.FindPath(start, end);
    //     string output = "Path lenght: " + path.Length + "\n";
    //     for (int i = 0; i < path.Length; i++)
    //     {
    //         output += i + ": " + path[i].name + "\n";
    //     }
    //     Debug.Log(output);

    //     path = nodeGroup.FindPath(end, start);
    //     output = "Path lenght: " + path.Length + "\n";
    //     for (int i = 0; i < path.Length; i++)
    //     {
    //         output += i + ": " + path[i].name + "\n";
    //     }
    //     Debug.Log(output);
    // }

    private void Awake() {
        nodeGroup = CreateNodeGroup();
    }

    public NodeGroup CreateNodeGroup()
    {
        NodeBehavior[] nodeBehaviors = GetComponentsInChildren<NodeBehavior>();
        Dictionary<NodeBehavior, Node> nodeDict = new Dictionary<NodeBehavior, Node>();
        for (int i = 0; i < nodeBehaviors.Length; i++)
        {
            NodeBehavior it = nodeBehaviors[i];
            nodeDict.Add(it, new Node(it.transform.position, it.name));
        }

        for (int i = 0; i < nodeBehaviors.Length; i++)
        {
            NodeBehavior it = nodeBehaviors[i];
            Node node = nodeDict[it];

            List<Node> neighbors = new List<Node>();
            for (int j = 0; j < it.neighbors.Count; j++)
            {
                NodeBehavior it2 = it.neighbors[j];
                neighbors.Add(nodeDict[it2]);
            }
            node.neighbors = neighbors.ToArray();
        }

        List<Node> nodes = new List<Node>();
        foreach (KeyValuePair<NodeBehavior, Node> item in nodeDict)
        {
            nodes.Add(item.Value);
        }
        return new NodeGroup(nodes.ToArray());
    }

    public Node[] FindPath(Node startNode, Node endNode){
        return nodeGroup.FindPath(startNode, endNode);
    }

    public Node[] FindPath(Vector3 startPosition, Vector3 endPosition){
        return nodeGroup.FindPath(startPosition, endPosition);
    }

    public Node FindClosestNode(Vector3 position){
        return nodeGroup.FindClosestNodeToPosition(position);
    }

    [ContextMenu("Create new neighbor")]
    public void CreateNewNeighbor(){

        GameObject gameObject = new GameObject("Node");
        NodeBehavior nodeBehavior = gameObject.AddComponent<NodeBehavior>();
        nodeBehavior.transform.SetParent(transform);
    }
}
