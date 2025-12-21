using Unity.VisualScripting;
using UnityEngine;




public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;      // 左右移動速度
    [SerializeField] private float forwardSpeed = 10f;   // 向前移動速度
    [SerializeField] private float jumpForce = 10f;




    [SerializeField] private Rigidbody rigid;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer;




    [SerializeField] private MoveObstacle moveObstacle;


    private Vector2 touchStartPos;           // 觸碰起始位置
    private float touchStartTime;            // 觸碰開始時間
    private Vector2 previousTouchPos;        // 上一幀觸碰位置（用於計算速度）
    private bool isTouching = false;         // 是否正在觸碰
    private bool hasJumpThisTouch = false;   // 是否在這次觸碰中跳躍過
    private bool hasHorizontalMovement = false;  // 這次觸碰中是否有左右移動


    // 滑鼠輸入相關（編輯器測試用）
    private Vector2 mouseStartPos;          // 滑鼠起始位置
    private float mouseStartTime;           // 滑鼠按下開始時間
    private Vector2 previousMousePos;       // 上一幀滑鼠位置
    private bool isMouseDown = false;        // 是否正在按住滑鼠
    private bool hasJumpThisMouse = false;   // 是否在這次滑鼠操作中跳躍過
    private bool hasMouseHorizontalMovement = false;  // 這次滑鼠操作中是否有左右移動


    [SerializeField] private float jumpSwipeDistance = 50f;      // 跳躍所需的最小滑動距離（像素）
    [SerializeField] private float jumpSwipeSpeed = 300f;         // 跳躍所需的最小滑動速度（像素/秒）










    private float groundCheckDistance = 0.2f;
    private bool isGrounded;  // 是否在地上
    private bool isJumping = false;  // 是否正在跳躍（用於禁止跳躍時的左右移動）




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


        // 先處理觸控輸入（優先於鍵盤輸入）
        if (Input.touchCount > 0)
        {
            TouchInput();
        }
        // 編輯器測試：使用滑鼠模擬觸控
        else if (Application.isEditor)
        {
            MouseInput();
        }
        // 如果沒有觸控或滑鼠輸入，才使用鍵盤輸入
        else
        {
            h = Input.GetAxis("Horizontal");
        }




        Vector3 origin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance, groundLayer);


        // 如果落地了，重置跳躍狀態
        if (isGrounded && isJumping)
        {
            isJumping = false;
        }


        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("YVelocity", rigid.velocity.y);




        // 跳躍
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }




    private void Jump()
    {
        rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        anim.SetTrigger("Jump");
        isJumping = true;  // 設置跳躍狀態，禁止左右移動
        h = 0;  // 跳躍時清除水平移動
    }






    // 在固定時間間隔執行一次
    void FixedUpdate()
    {
        Vector3 currentVelocity = rigid.velocity;
        // 如果正在跳躍，不應用水平移動
        float horizontalInput = isJumping ? 0f : h;
        Vector3 newVelocity = new Vector3(horizontalInput * moveSpeed, currentVelocity.y, forwardSpeed);
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
            GameManager.instance.TakeDamage(other.gameObject.GetComponent<Trap>().damage);
        }
    }




    /// <summary>
    /// 播放腳步聲音效
    /// </summary>
    public void PlayFootStepSound()
    {
        AudioManager.instance.PlayFootStepSound();
    }




    private void TouchInput()
    {
        if (Input.touchCount == 0)   // 如果沒有觸碰，則重置水平移動和跳躍狀態
        {
            h = 0;
            isTouching = false;
            hasJumpThisTouch = false;
            return;
        }


        Touch touch = Input.GetTouch(0); // 取得觸碰物件


        if (touch.phase == TouchPhase.Began)   // 如果觸碰開始
        {
            touchStartPos = touch.position;    // 記錄觸碰起始位置
            previousTouchPos = touch.position; // 記錄當前位置作為上一幀位置
            touchStartTime = Time.time;        // 記錄觸碰開始時間
            isTouching = true;                 // 設置觸碰狀態
            hasJumpThisTouch = false;          // 設置跳躍狀態
            hasHorizontalMovement = false;     // 重置左右移動標記
        }
        else if (touch.phase == TouchPhase.Moved && isTouching)   // 如果觸碰移動，則計算水平移動
        {
            Vector2 delta = touch.position - touchStartPos;
            Vector2 frameDelta = touch.position - previousTouchPos; // 本幀移動距離


            // 檢查是否有左右移動（絕對值超過閾值）
            if (Mathf.Abs(frameDelta.x) > 5f)  // 如果本幀有明顯的左右移動
            {
                hasHorizontalMovement = true;  // 標記這次操作有左右移動
            }


            // 檢查是否主要是向上滑動（向上距離必須大於左右距離）
            bool isMainlyUpward = Mathf.Abs(delta.y) > Mathf.Abs(delta.x);


            // 如果主要是左右滑動，處理水平移動
            if (!isMainlyUpward)
            {
                h = Mathf.Clamp(delta.x / Screen.width * 5f, -1f, 1f);
            }
            else
            {
                // 如果是向上滑動，但不允許左右移動
                h = 0;
            }


            previousTouchPos = touch.position; // 更新上一幀位置
        }
        else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            // 只在觸碰結束時檢查是否滿足跳躍條件
            if (isTouching)
            {
                Vector2 delta = touch.position - touchStartPos;
                float touchDuration = Time.time - touchStartTime;


                // 只有在這次操作中沒有左右移動，且主要是向上滑動時，才檢查跳躍
                if (!hasHorizontalMovement)
                {
                    // 檢查是否主要是向上滑動（向上距離必須大於左右距離）
                    bool isMainlyUpward = Mathf.Abs(delta.y) > Mathf.Abs(delta.x);


                    if (isMainlyUpward)
                    {
                        // 計算平均滑動速度
                        float averageSwipeSpeed = touchDuration > 0 ? delta.y / touchDuration : 0f;


                        // 檢查是否上滑足夠距離或速度
                        bool isUpwardSwipe = delta.y > jumpSwipeDistance;
                        bool isFastUpwardSwipe = averageSwipeSpeed > jumpSwipeSpeed && delta.y > 0;


                        if ((isUpwardSwipe || isFastUpwardSwipe) && isGrounded && !hasJumpThisTouch)
                        {
                            Jump();
                            hasJumpThisTouch = true;
                        }
                    }
                }
            }


            h = 0;
            isTouching = false;
            hasJumpThisTouch = false;
            hasHorizontalMovement = false;
        }
    }




    /// <summary>
    /// 處理滑鼠輸入（編輯器測試用，模擬觸控行為）
    /// </summary>
    private void MouseInput()
    {
        // 滑鼠左鍵按下（對應觸控開始）
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = Input.mousePosition;
            previousMousePos = Input.mousePosition;
            mouseStartTime = Time.time;
            isMouseDown = true;
            hasJumpThisMouse = false;
            hasMouseHorizontalMovement = false;  // 重置左右移動標記
        }
        // 滑鼠左鍵按住並移動（對應觸控移動）
        else if (Input.GetMouseButton(0) && isMouseDown)
        {
            Vector2 currentMousePos = Input.mousePosition;
            Vector2 delta = currentMousePos - mouseStartPos;
            Vector2 frameDelta = currentMousePos - previousMousePos;


            // 檢查是否有左右移動（絕對值超過閾值）
            if (Mathf.Abs(frameDelta.x) > 5f)  // 如果本幀有明顯的左右移動
            {
                hasMouseHorizontalMovement = true;  // 標記這次操作有左右移動
            }


            // 檢查是否主要是向上滑動（向上距離必須大於左右距離）
            bool isMainlyUpward = Mathf.Abs(delta.y) > Mathf.Abs(delta.x);


            // 如果主要是左右滑動，處理水平移動
            if (!isMainlyUpward)
            {
                h = Mathf.Clamp(delta.x / Screen.width * 5f, -1f, 1f);
            }
            else
            {
                // 如果是向上滑動，但不允許左右移動
                h = 0;
            }


            previousMousePos = currentMousePos;
        }
        // 滑鼠左鍵放開（對應觸控結束）
        else if (Input.GetMouseButtonUp(0) && isMouseDown)
        {
            // 只在滑鼠放開時檢查是否滿足跳躍條件
            Vector2 delta = (Vector2)Input.mousePosition - mouseStartPos;
            float mouseDuration = Time.time - mouseStartTime;


            // 只有在這次操作中沒有左右移動，且主要是向上滑動時，才檢查跳躍
            if (!hasMouseHorizontalMovement)
            {
                // 檢查是否主要是向上滑動（向上距離必須大於左右距離）
                bool isMainlyUpward = Mathf.Abs(delta.y) > Mathf.Abs(delta.x);


                if (isMainlyUpward)
                {
                    float averageSwipeSpeed = mouseDuration > 0 ? delta.y / mouseDuration : 0f;


                    bool isUpwardSwipe = delta.y > jumpSwipeDistance;
                    bool isFastUpwardSwipe = averageSwipeSpeed > jumpSwipeSpeed && delta.y > 0;


                    if ((isUpwardSwipe || isFastUpwardSwipe) && isGrounded && !hasJumpThisMouse)
                    {
                        Jump();
                        hasJumpThisMouse = true;
                    }
                }
            }


            h = 0;
            isMouseDown = false;
            hasJumpThisMouse = false;
            hasMouseHorizontalMovement = false;
        }
        // 如果滑鼠沒有按下，重置狀態
        else if (!Input.GetMouseButton(0))
        {
            h = 0;
            isMouseDown = false;
            hasJumpThisMouse = false;
            hasMouseHorizontalMovement = false;
        }
    }
}






