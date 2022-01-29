using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public float updateFrequency = 1;
    public HashSet<string> flags = new HashSet<string>();

    NavigationInput navigationInput;
    List<BaseObjective> objectives = new List<BaseObjective>();
    BaseObjective currentObjective;
    float tickTimer = 0.0f;

    void Start()
    {
        navigationInput = GetComponent<NavigationInput>();
    }

    public void CompleteCurrentObjective()
    {
        currentObjective.CompleteObjective(this);
        objectives.Remove(currentObjective);
        currentObjective = null;
    }

    void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer > 1.0f / updateFrequency)
        {
            tickTimer = 0.0f;
            AIUpdate();
        }
    }

    void AIUpdate()
    {
        if (currentObjective == null && objectives.Count > 0)
        {
            currentObjective = objectives[0];
            currentObjective.StartObjective(this);
        }

        if (currentObjective != null)
        {
            currentObjective.ExecuteObjective(this);
        }
    }

    public void MoveToPosition(Vector3 position)
    {
        navigationInput.TargetPosition = position;
    }

    public bool HasArrived { get { return navigationInput.arrived; } }

    public void AssignObjectives(BaseObjective[] newObjectives)
    {
        objectives.Clear();
        objectives.AddRange(newObjectives);
    }
}