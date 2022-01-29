using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassangerSlot : MonoBehaviour
{
    public ActorMover currentActor;

    public void AssignToPassanger(ActorMover actorMover){

        currentActor = actorMover;
        if(currentActor != null){
            currentActor.transform.position = transform.position;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.5f);
    }
}
