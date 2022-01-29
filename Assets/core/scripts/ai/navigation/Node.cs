using UnityEngine;

public class Node
{
    public Node[] neighbors = new Node[0];
    public float gCost;
    public float hCost;
    
    public Vector3 position = Vector3.zero;
    public string name = "";

    public Node parent = null;
    public Node(){}

    public Node(Vector3 position, string name){
        this.position = position;
        this.name = name;
    }
    
    public bool IsNeighborOfNode(Node node){
        for (int i = 0; i < neighbors.Length; i++)
        {
            if(node == neighbors[i])
                return true;   
        }
        return false;
    }

    public float FCost { get{ return gCost + hCost; } }
}
