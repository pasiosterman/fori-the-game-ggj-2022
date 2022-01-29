using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardForiObjective : BaseObjective
{
    const string QUEUE_FORI = "QueuedFori";

    public ForiStop foriStop;

    public override void StartObjective(AIController controller)
    {
        Debug.Log("Start board föri objective!");
        controller.NavigateToPosition(foriStop.transform.position);
    }

    public override void ExecuteObjective(AIController controller)
    {
        if(controller.HasArrived && !controller.flags.Contains(QUEUE_FORI) ){

            foriStop.QueueToFori(controller.Mounter);
            controller.flags.Add(QUEUE_FORI);
        }
        else{
            if(controller.Mounter.IsMounted){
                controller.CompleteCurrentObjective();
            }
        }
    }
}
