using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountable : MonoBehaviour
{
    public Mounter currentMounter;

    public void AssignMounter(Mounter passanger)
    {

        currentMounter = passanger;
        if (currentMounter != null)
        {
            currentMounter.mountedTo = this;
            currentMounter.transform.position = transform.position;
            currentMounter.transform.SetParent(transform);
            SetMounterPhysicsEnabled(currentMounter, false);
        }
    }

    public void Umount()
    {
        if (currentMounter != null)
        {
            currentMounter.mountedTo = null;
            currentMounter.transform.SetParent(null);
            SetMounterPhysicsEnabled(currentMounter, true);
            currentMounter = null;
        }
    }

    void SetMounterPhysicsEnabled(Mounter mounter, bool enabled)
    {
        Rigidbody rb = currentMounter.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = !enabled;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.5f);
    }
}
