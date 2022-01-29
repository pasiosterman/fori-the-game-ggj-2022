using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitAreaObjective : BaseObjective
{
    public NodeBehavior[] areaExits;

    public override void StartObjective(AIController controller)
    {
        NodeBehavior nodeBehavior = areaExits[Random.Range(0, areaExits.Length)];
        controller.NavigateToPosition(nodeBehavior.transform.position);
    }

    public override void ExecuteObjective(AIController controller)
    {
        if(controller.HasArrived){
            controller.CompleteCurrentObjective();
        }
    }
}
