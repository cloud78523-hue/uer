using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // 左右移動速度
    [SerializeField] private float forwardSpeed = 10f; // 前進速度
    [SerializeField] private float jumpForce = 10f; 

    [SerializeField] private Rigidbody rigid;
    [SerializeField] private Animator anim;

    [SerializeField] private LayerMask groundLayer;
    private float groundCheckDistance = 0.2f;
    private bool isGrounded;  // 是否在地上 

    // 遊戲初始化時執行一次 
    void Awake()
    {
        // Debug.Log("遊戲初始化"); 
    }

    // 物件啟用時執行一次 
    void Start()
    {
        // Debug.Log("物件啟用"); 
    }

    float h;
    // 每幀執行一次 
    void Update()
    {
        // Debug.Log("物件啟用時間：" + Time.time); 

        // 移動物件Z軸0.01單位 
        // transform.position = transform.position + new Vector3(0, 0, 0.01f);
        // transform.position += new Vector3(0, 0, 0.01f); 
        // transform.Translate(0, 0, 0.01f); 

        // 取得垂直和水平方向的輸入並移動 
        // float v = Input.GetAxis("Vertical"); 
        h = Input.GetAxis("Horizontal");

        Vector3 origin = transform.position + Vector3.up *0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down,groundCheckDistance, groundLayer);

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("YVelocity", rigid.velocity.y);

        // 跳躍 
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigid.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }
    }



    // 在固定時間間隔執行一次 
    void FixedUpdate()
    {
        Vector3 currentVelocity = rigid.velocity;
        Vector3 newVelocity = new Vector3(h * moveSpeed,currentVelocity.y, forwardSpeed);
        rigid.velocity = newVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 如果碰撞到的物件身上有 Coin 元件，就呼叫 GameManager 增加金幣數量
        if (other.gameObject.GetComponent<Coin>())
        {
            GameManager.instance.AddCoin();
        }

        // 如果碰撞到的物件身上有 Trap 元件，就呼叫 GameManager 減少生命值
        if (other.gameObject.GetComponent<Trap>())
        {

            GameManager.instance.TakeDamage(other.gameObject.GetComponent<
             Trap>().damage);
        }
    }

    /// <summary> 
    /// 播放腳步聲音效 
    /// </summary> 
    public void PlayFootStepSound()
    {
        AudioManager.instance.PlayFootStepSound();
    }
}


