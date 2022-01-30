using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSpawner : Spawner
{
    public override Tuple<Transform, GameObject> SpawnRandomEntryFromSpawnTable()
    {

        var pointAndSpawnedInstance = base.SpawnRandomEntryFromSpawnTable();
        ConstantInput constantInput = pointAndSpawnedInstance.Item2.GetComponent<ConstantInput>();
        if (constantInput != null)
        {
            constantInput.input = pointAndSpawnedInstance.Item1.forward;
        }
        else
        {
            Debug.LogError("Spawned boat missing ConstantInput component!");
        }

        Debug.Log("Spawned boat: " + pointAndSpawnedInstance.Item1.name);
        return pointAndSpawnedInstance;
    }
}
