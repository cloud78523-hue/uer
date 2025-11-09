using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] roadPrefabs; // 陣列
    [SerializeField] private int maxActiveRoads = 5; // 最大同時存在的路段數量
    [SerializeField] private Transform player;
    [SerializeField] private float roadLength = 10f; // 每段路段長度
    [SerializeField] private float spawnDistance = 30f; // 當玩家距離收成點多近時生成新路段


    private Vector3 nextRoadPoint = Vector3.zero; // Vector3.zero = (0,0,0)
    private List<GameObject> activeRoads = new List<GameObject>(); // 已經生成的路段列表

    private void Start()
    {
        nextRoadPoint = transform.position;
        for (int i = 0; i < maxActiveRoads; i++)
        {
            SpawnRoad();
        }
    }
    void Update()
    {
        if (Vector3.Distance(player.position, nextRoadPoint) < spawnDistance)
        {
            SpawnRoad();
            DeleteRoad();
        }
    }
        


    private void SpawnRoad()
    {
        GameObject road = roadPrefabs[Random.Range(0, roadPrefabs.Length)]; // Random.Range(0, roadPrefabs.Length) =0,1
        GameObject newRoad = Instantiate(road, nextRoadPoint, Quaternion.identity); // Instantiate(物件,位子,旋轉)
        activeRoads.Add(newRoad); // 將新路段加入已經生成的路段列表

        nextRoadPoint = nextRoadPoint + roadLength * Vector3.forward; // Vector3.forward = (0,0,1)
        
    }

    private void DeleteRoad()
    {
        // 如果已經生成的路段數量超過最大同時存在的路段數量，則刪除最舊的路段
        if (activeRoads.Count > maxActiveRoads)
        {
            GameObject oldestRoad = activeRoads[0];
            activeRoads.RemoveAt(0);
            Destroy(oldestRoad);
        }
    }
}

     