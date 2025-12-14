using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject[] roadPrefabs;

    [Header("Spawn Control")]
    [SerializeField] private int maxActiveRoads = 5;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnDistance = 30f;

    private readonly List<GameObject> activeRoads = new();
    private Transform nextSpawnPoint;

    private void Start()
    {
        var startAnchor = new GameObject("RoadStartAnchor").transform;
        startAnchor.position = transform.position;
        startAnchor.rotation = transform.rotation;

        nextSpawnPoint = startAnchor;

        for (int i = 0; i < maxActiveRoads; i++)
            SpawnRoad();
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, nextSpawnPoint.position) < spawnDistance)
        {
            SpawnRoad();
            DeleteOldRoad();
        }
    }

    private void SpawnRoad()
    {
        GameObject prefab = roadPrefabs[Random.Range(0, roadPrefabs.Length)];

        // 先用 nextSpawnPoint 的位置/旋轉生成（只是暫放）
        GameObject newRoad = Instantiate(prefab, nextSpawnPoint.position, nextSpawnPoint.rotation);

        Transform startPoint = newRoad.transform.Find("StartPoint");
        Transform endPoint = newRoad.transform.Find("EndPoint");

        if (startPoint == null || endPoint == null)
        {
            Debug.LogError($"Road prefab [{prefab.name}] 必須包含 StartPoint 與 EndPoint");
            Destroy(newRoad);
            return;
        }

        // 讓新路段的 StartPoint 精準貼到上一段的 nextSpawnPoint
        // 計算 StartPoint 現在跟目標點的差距，整段路反向搬移補上
        Vector3 delta = nextSpawnPoint.position - startPoint.position;
        newRoad.transform.position += delta;

        activeRoads.Add(newRoad);

        // 更新下一段的生成點
        nextSpawnPoint = endPoint;
    }

    private void DeleteOldRoad()
    {
        if (activeRoads.Count <= maxActiveRoads) return;

        GameObject oldestRoad = activeRoads[0];
        activeRoads.RemoveAt(0);
        Destroy(oldestRoad);
    }
}
