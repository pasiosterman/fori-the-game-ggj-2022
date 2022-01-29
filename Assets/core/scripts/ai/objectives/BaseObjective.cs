using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObjective : MonoBehaviour
{
    public virtual void StartObjective(AIController controller){}
    public virtual void ExecuteObjective(AIController controller){}
    public virtual void CompleteObjective(AIController controller){}
}
