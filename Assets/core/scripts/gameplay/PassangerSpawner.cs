using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassangerSpawner : Spawner
{
    public GameObject ObjectivesParent;
    public NodeGroupBehavior nodeGroupToAssign;

    BaseObjective[] objectivesToAssign;

    protected override void Start()
    {
        base.Start();

        if(nodeGroupToAssign == null){
            Debug.LogError("Missing reference to nodeGroupToAssign", this);
        }

        InitObjectivesToAssign();
    }

    private void InitObjectivesToAssign()
    {
        if (ObjectivesParent != null)
        {
            objectivesToAssign = ObjectivesParent.GetComponentsInChildren<BaseObjective>();
            if (objectivesToAssign.Length == 0)
            {
                Debug.LogWarning("No objectives found in "
                    + ObjectivesParent.name
                    + " spawned passangers will not receive any objectives!", this);
            }
        }
        else
        {
            Debug.LogError("Missing reference to ObjectivesParent", this);
            objectivesToAssign = new BaseObjective[0];
        }
    }

    public override Tuple<Transform, GameObject> SpawnRandomEntryFromSpawnTable()
    {
        var value = base.SpawnRandomEntryFromSpawnTable();
        AIController aIController = value.Item2.GetComponent<AIController>();
        if (aIController != null)
        {
            if(nodeGroupToAssign != null){
                aIController.AssignNodeGroup(nodeGroupToAssign);
            }
            AssignObjectives(aIController, objectivesToAssign);
        }
        else
        {
            Debug.LogError("Spawned passanger missing AIController component!", this);
        }

        return value;
    }

    private void AssignObjectives(AIController aIController, BaseObjective[] newObjectives)
    {
        if (aIController != null)
        {
            aIController.AssignObjectives(newObjectives);
        }
        else
        {
            Debug.LogError("Spawned Object has no AIController! Unable to assign objectives!", this);
        }
    }
}
