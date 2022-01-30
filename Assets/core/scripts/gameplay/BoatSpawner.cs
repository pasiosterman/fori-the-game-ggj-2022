using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSpawner : MonoBehaviour
{
    public SpawnerWaveConfiguration[] waves = new SpawnerWaveConfiguration[0];
    public int startWaveIndex = 0;
    public Transform spawnPointsParent;

    private int currentWaveIndex = 0;
    private int[,] currentSpawnTable = new int[0, 0];
    private float currentSpawnRate = 5.0f;
    private float timeStamp = 0.0f;

    private Transform spawnedParent;

    private void Start() {
        StartWave(startWaveIndex);
        spawnedParent = new GameObject("SpawnedParent").transform;
        spawnedParent.transform.position = Vector3.zero;

        if(spawnPointsParent == null){
            Debug.LogWarning("spawnPointsParent is unassigned, using self as parent.", this);
            spawnPointsParent = transform;
        }
        
        if(spawnPointsParent.childCount == 0){
            Debug.LogError("spawnPointsParent missing children! Spawner wont know where to spawn boats!", this);
        }
    }

    private void Update() {
        
        if(currentWaveIndex < 0) return;

        if(Time.time - timeStamp > currentSpawnRate){
            timeStamp = Time.time;
            SpawnRandomBoat();
        }
    }

    public void SpawnRandomBoat(){

        int maxValue = FindMaxValueForSpawnTable(currentSpawnTable);
        int randomValue = UnityEngine.Random.Range(0, maxValue);
        GameObject spawnPrefab = WaveEntryPrefabWithValue(randomValue);
        
        Transform point = spawnPointsParent.GetChild(UnityEngine.Random.Range(0, spawnPointsParent.childCount));
        GameObject obj = Instantiate(spawnPrefab, point.position, point.rotation);
        obj.transform.SetParent(spawnedParent);
        
        ConstantInput constantInput = obj.GetComponent<ConstantInput>();
        constantInput.input = point.forward;

        Debug.Log("Spawned boat" + obj.name);
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

    GameObject WaveEntryPrefabWithValue(int value){ 

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

    int FindMaxValueForSpawnTable(int[,] spawnTable)
    {
        return spawnTable[spawnTable.GetLength(0) - 1, 1];
    }

    int[,] GenerateSpawnTable(List<SpawnerWaveEntry> spawnConfigs)
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
