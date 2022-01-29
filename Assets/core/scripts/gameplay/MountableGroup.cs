using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountableGroup : MonoBehaviour
{
    private Mountable[] mountables = new Mountable[0];

    private void Start()
    {
        mountables = GetComponentsInChildren<Mountable>();
    }

    public Mountable GetFreeMountable(){

        for (int i = 0; i < mountables.Length; i++)
        {
            Mountable it = mountables[i];
            if (it.currentMounter == null)
            {
                return it;
            }
        }
        return null;
    }

    public bool HasFreeMountables
    {
        get
        {
            for (int i = 0; i < mountables.Length; i++)
            {
                if(mountables[i].currentMounter == null)
                    return true;
            }
            return false;
        }
    }
}
