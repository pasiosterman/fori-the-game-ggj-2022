using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fori : MonoBehaviour
{
    public MountableGroup moutanbleGroup;
    public bool IsFull { get{ return moutanbleGroup.HasFreeMountables; } }
}
