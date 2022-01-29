using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForiLoadTrigger : MonoBehaviour
{
    int count = 0;

    private void OnTriggerEnter(Collider other)
    {
        count++;
    }

    private void OnTriggerExit(Collider other)
    {
        count--;
    }

    public bool IsForiDocked { get { return count > 0; } }
}
