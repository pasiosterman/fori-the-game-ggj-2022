using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mounter : MonoBehaviour
{
    public Mountable mountedTo;
    public bool IsMounted { get{ return mountedTo != null; } }

    public void Unmount(){
        mountedTo.Umount();
    }
    
    public void Unmount(Vector3 newPosition){
        mountedTo.Umount();
        transform.position = newPosition;
    }

    public void SwapMountable(Mountable mountable){
        Unmount();
        mountable.AssignMounter(this);
    }
}
