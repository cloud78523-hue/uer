using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private Transform target; // 要追蹤的物件

    private Vector3 distance;                  // 相機與目標的距離

    void Start()
    {
        distance = target.position - transform.position; // 計算初始距離
    }

    // Update is called once per frame
    void Update()
    {
        transform .position = target.position - distance; 
    }
}
