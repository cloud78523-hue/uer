using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 180f; // 旋轉速度

    [SerializeField] private bool isRotating = true;   // 是否旋轉

    [Header("上下移動設定")]

    [SerializeField] private float bobheight = 0.25f;  // 上下移動高度

    [SerializeField] private float bobspeed = 2f;      // 上下移動速度

    [SerializeField] private bool isBob = true;        // 是否上下移動

    private Vector3 startPos;  // 初始位置


    private void Start()
    {
        startPos = transform.localPosition;  // 獲取初始位置
    }

    void Update()
    {
        if (isRotating) 
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.Self);  // 旋轉物體
         }

        if (isBob)
        {
            float y = Mathf.Sin(Time.time * bobspeed) * bobheight; // Mathf.Sin(x) 返回 -1 到 1 之間的值
            transform.localPosition = startPos + new Vector3(0, y, 0);  // 上下移動物體
        }
         
    }

    






    private void OnTriggerEnter(Collider other)
    {
            Destroy(gameObject);
     }
 }



