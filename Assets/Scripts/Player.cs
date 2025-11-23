using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private Rigidbody rigid; 
    [SerializeField] private Animator anim; 

    [SerializeField] private LayerMask groundLayer;
    private float groundCheckDistance = 0.2f;
    private bool isGrounded; // 是否在地上
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
        // float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(h, rigid.velocity.y, 1); // 前後移動固定1
        rigid.velocity = moveDirection * moveSpeed; // 設定物體速度

        Vector3 origin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance, groundLayer); // 檢查是否在地面上

        anim.SetBool("isGrounded", isGrounded); // 設定動畫參數
        anim.SetFloat("YUelocity", rigid.velocity.y);

        // 跳躍
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigid.AddForce(0, jumpForce, 0);
            anim.SetTrigger("Jump"); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 如果碰撞到的物件身上有Coin元件，就呼叫 GameManager 增加金幣數量
        if(other.gameObject.GetComponent<Coin>())
        {
            GameManager.instance.AddCoin();
        }

        if (other.gameObject.GetComponent<Trap>())
        {
            GameManager.instance.TakeDamage(other.gameObject.GetComponent<Trap>().damage);
        }
    }
}
