using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {

        ActorMover mover = other.GetComponentInParent<ActorMover>();
        if(mover != null){
            Destroy(mover.gameObject);
        }
    }
}