using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpawnerWaveConfiguration[] waves = new SpawnerWaveConfiguration[0];
    public int startWaveIndex = 0;
    public Transform spawnPointsParent;

    protected int currentWaveIndex = 0;
    protected int[,] currentSpawnTable = new int[0, 0];
    protected float currentSpawnRate = 5.0f;
    protected float timeStamp = 0.0f;
    protected Transform spawnedParent;

    protected virtual void Start()
    {
        InitializeSpawner();
    }

    protected virtual void Update()
    {
        HandleSpawning();
    }

    protected virtual void HandleSpawning()
    {
        if(waves == null || waves.Length == 0) return;

        if (currentWaveIndex < 0) return;

        if (Time.time - timeStamp > currentSpawnRate)
        {
            timeStamp = Time.time;
            SpawnRandomEntryFromSpawnTable();
        }
    }

    protected virtual void InitializeSpawner()
    {
        StartWave(startWaveIndex);
        spawnedParent = new GameObject("SpawnedParent").transform;
        spawnedParent.transform.position = Vector3.zero;

        if (spawnPointsParent == null)
        {
            Debug.LogWarning("spawnPointsParent is unassigned, using self as parent.", this);
            spawnPointsParent = transform;
        }

        if (spawnPointsParent.childCount == 0)
        {
            Debug.LogError("spawnPointsParent missing children! Spawner wont know where to spawn boats!", this);
        }
    }

    public virtual Tuple<Transform, GameObject> SpawnRandomEntryFromSpawnTable(){

        int maxValue = FindMaxValueForSpawnTable(currentSpawnTable);
        int randomValue = UnityEngine.Random.Range(0, maxValue);
        GameObject spawnPrefab = WaveEntryPrefabWithValue(randomValue);
        
        Transform point = spawnPointsParent.GetChild(UnityEngine.Random.Range(0, spawnPointsParent.childCount));
        GameObject obj = Instantiate(spawnPrefab, point.position, point.rotation);
        obj.transform.SetParent(spawnedParent);
        obj.transform.forward = point.forward;

        return new Tuple<Transform, GameObject>(point, obj);
    }

    public void StartWave(int waveIndex)
    {
        if (waveIndex < waves.Length && waves.Length > 0)
        {
            currentWaveIndex = waveIndex;
            currentSpawnTable = GenerateSpawnTable(waves[currentWaveIndex].spawnerWaveEntries);
            currentSpawnRate = waves[currentWaveIndex].spawnRate;
        }
        else
        {
            currentWaveIndex = -1;
            Debug.LogError("No wave with index" + waveIndex);
        }
    }

    [ContextMenu("print spawn tables")]
    public void PrintSpawnTables()
    {
        String output = "";
        for (int i = 0; i < waves.Length; i++)
        {
            int[,] spawnTable = GenerateSpawnTable(waves[i].spawnerWaveEntries);
            output += "Wave: " + i + " Size:" + spawnTable.GetLength(0) + "\n";
            for (int j = 0; j < spawnTable.GetLength(0); j++)
            {
                output += (j + 1) + ": " + spawnTable[j, 0] + " - " + spawnTable[j, 1] + "\n";
            }
            output += "Max Value: " + FindMaxValueForSpawnTable(spawnTable) + "\n";
            output += "---";
        }
        Debug.Log(output);
    }

    protected virtual GameObject WaveEntryPrefabWithValue(int value){ 

        for (int i = 0; i < currentSpawnTable.GetLength(0); i++)
        {
            int from = currentSpawnTable[i,0];
            int to = currentSpawnTable[i,1];

            if(value >= from && value < to){
                return waves[currentWaveIndex].spawnerWaveEntries[i].Prefab;
            }
        }

        Debug.LogError("value out of bounds of spawntable");
        return null;
    }

    protected virtual int FindMaxValueForSpawnTable(int[,] spawnTable)
    {
        return spawnTable[spawnTable.GetLength(0) - 1, 1];
    }

    protected virtual int[,] GenerateSpawnTable(List<SpawnerWaveEntry> spawnConfigs)
    {
        int[,] spawnTable = new int[spawnConfigs.Count, 2];
        for (int i = 0; i < spawnConfigs.Count; i++)
        {
            if (i == 0)
            {
                spawnTable[i, 0] = 0;
                spawnTable[i, 1] = spawnConfigs[i].chance;
            }
            else
            {
                spawnTable[i, 0] = spawnTable[i - 1, 1];
                spawnTable[i, 1] = spawnTable[i - 1, 1] + spawnConfigs[i].chance;
            }
        }
        return spawnTable;
    }
}
