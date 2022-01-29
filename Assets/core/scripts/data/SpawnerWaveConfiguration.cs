using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnerWaveConfiguration", order = 1)]
public class SpawnerWaveConfiguration : ScriptableObject
{
    public List<SpawnerWaveEntry> spawnerWaveEntries;

    public float spawnRate = 5.0f;
}
