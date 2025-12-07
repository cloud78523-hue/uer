using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 2f;
    private LayerMask wallLayer;


    private Vector3 moveDirection = Vector3.right;
    private Rigidbody rb;


    void Start()
    {
        wallLayer = LayerMask.GetMask("wall");
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // 移動障礙物
        if (rb != null)
        {
            rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, rb.velocity.z);
        }
        else
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        // 碰到東西就反轉方向
        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            moveDirection = -moveDirection;
        }
    }
}

