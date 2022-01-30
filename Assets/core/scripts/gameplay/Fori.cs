using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fori : MonoBehaviour
{
    static readonly int OPEN_HASH =  Animator.StringToHash("open");

    public MountableGroup moutanbleGroup;
    public ForiLoadTrigger loadTriggerA;
    public ForiLoadTrigger loadTriggerB;

    public Animator gateAnimatorA;
    public Animator gateAnimatorB;

    private void Update() {

        if(gateAnimatorA != null && loadTriggerA != null){
            gateAnimatorA.SetBool(OPEN_HASH, loadTriggerA.IsForiDocked);
        }
        if(gateAnimatorB != null && loadTriggerB != null){
            gateAnimatorB.SetBool(OPEN_HASH, loadTriggerB.IsForiDocked);
        }
        
        
    }

    public bool IsFull { get{ return moutanbleGroup.HasFreeMountables; } }
}
