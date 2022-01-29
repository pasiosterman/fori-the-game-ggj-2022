using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignObjectiveTo : MonoBehaviour
{
    public AIController target;

    void Start()
    {
        BaseObjective[] childObjetives = GetComponentsInChildren<BaseObjective>();
        AIController aIController = target.GetComponentInChildren<AIController>();
        aIController.AssignObjectives(childObjetives);
    }   
}
