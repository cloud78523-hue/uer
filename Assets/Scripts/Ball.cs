using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
     void Awake()
    {

       // Debug.Log("遊戲初始化");
    }
    // 物件啟用時執行一次
    void Start()
    {
        Debug.Log("物件啟用");
    }

    // 每幀執行一次
    void Update()
    {
        // Debug.Log("物件啟用時間"+Time.time);

        // transform.Translate(0, 0, 0.01f);

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        transform.Translate(h * Time.deltaTime, 0, v * Time.deltaTime);
    }
}
