using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 380f;
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
        // Debug.Log("物件啟用時間" + Time.time);

        // 移動物件Z軸0.01單位
        // transform.position = transform.position + new Vector3(0, 0, 0.01f);
        // transform.position += new Vector3(0, 0, 0.01f);
        // transform.Translate(0, 0, 0.01f);


        // 取得垂直和水平方向的輸入並移動
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        transform.Translate(h * Time.deltaTime * speed, 0, v * Time.deltaTime * speed);

        // 跳躍
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
        }
    }
}
