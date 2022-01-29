using UnityEngine;
using System.Collections.Generic;

public class NodeBehavior : MonoBehaviour
{
    public List<NodeBehavior> neighbors = new List<NodeBehavior>();

    void OnDrawGizmosSelected()
    {
        //Draws double lines if parent selected but ¯\_(ツ)_/¯
        for (int i = 0; i < neighbors.Count; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, neighbors[i].transform.position);
            Gizmos.DrawSphere(transform.position, 0.3f);
        }
    }

    [ContextMenu("Create new neighbor")]
    public void CreateNewNeighbor(){

        GameObject gameObject = new GameObject("Node");
        NodeBehavior nodeBehavior = gameObject.AddComponent<NodeBehavior>();
        nodeBehavior.neighbors.Add(this);
        neighbors.Add(nodeBehavior);
        
        if(transform.parent != null){
            nodeBehavior.transform.SetParent(transform.parent);
            nodeBehavior.transform.position = transform.position;
        }
    }
}
