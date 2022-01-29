using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassangerSlotsParent : MonoBehaviour
{
    private PassangerSlot[] passangerSlots = new PassangerSlot[0];

    private void Start()
    {
        passangerSlots = GetComponentsInChildren<PassangerSlot>();
    }

    public void AssignPassangerSlot(ActorMover actorMover)
    {

        for (int i = 0; i < passangerSlots.Length; i++)
        {
            PassangerSlot it = passangerSlots[i];
            if (it.currentActor == null)
            {

            }
        }
    }
}
