using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnerWaveConfiguration", order = 1)]
public class SpawnerWaveConfiguration : ScriptableObject
{
    public List<SpawnConfiguration> spawnConfigurations;
}
