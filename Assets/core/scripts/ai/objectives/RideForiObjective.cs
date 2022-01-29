using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideForiObjective : BaseObjective
{
    public override void ExecuteObjective(AIController controller)
    {
        if(!controller.Mounter.IsMounted){
            controller.CompleteCurrentObjective();
        }
    }
}
