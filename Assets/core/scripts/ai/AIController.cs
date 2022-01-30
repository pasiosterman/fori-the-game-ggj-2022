using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mounter)), RequireComponent(typeof(NavigationInput))]
public class AIController : MonoBehaviour
{
    public float updateFrequency = 1;
    NavigationInput navigationInput;
    Mounter mounter; 
    List<BaseObjective> objectives = new List<BaseObjective>();
    BaseObjective currentObjective;
    float tickTimer = 0.0f;

    bool initialized = false;

    void Start()
    {
        if(!initialized) Initialize();
    }

    private void Initialize()
    {
        navigationInput = GetComponent<NavigationInput>();
        mounter = GetComponent<Mounter>();
        initialized = true;
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

    public void NavigateToPosition(Vector3 position)
    {
        if(!initialized) Initialize();

        navigationInput.NavigateTowards(position);
    }

    public void AssignObjectives(BaseObjective[] newObjectives)
    {
        if(!initialized) Initialize();

        currentObjective = null;
        objectives.Clear();
        objectives.AddRange(newObjectives);
    }

    public void AssignNodeGroup(NodeGroupBehavior nodeGroup){
        if(!initialized) Initialize();
        navigationInput.nodeGroupBehavior = nodeGroup;
    }

    public HashSet<string> flags = new HashSet<string>();
    public Mounter Mounter { get{ return mounter; } }
    public bool HasArrived { get { return navigationInput.arrived; } }
}