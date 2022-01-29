using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardForiObjective : BaseObjective
{
    public NodeBehavior foriEntryPoint;

    public override void StartObjective(AIController controller)
    {
        Debug.Log("Start board föri objective!");
        controller.MoveToPosition(foriEntryPoint.transform.position);
    }

    public override void ExecuteObjective(AIController controller)
    {
        if(controller.HasArrived){
            Debug.Log("mission complete!");
            controller.CompleteCurrentObjective();
        }
    }
}
