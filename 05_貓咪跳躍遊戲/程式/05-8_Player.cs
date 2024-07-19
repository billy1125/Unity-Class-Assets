using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 使用 LoadScene 必要的引用程式敘述 !!

public class Player : MonoBehaviour
{
    Rigidbody2D rigid2D;          // 用來放貓咪的剛體公開變數
    Animator animator;            // 用來放貓咪的動畫控制器公開變數
    public float jumpForce = 680.0f;     // 跳躍力預設值
    public float walkForce = 30.0f;      // 移動推力預設值
    public float maxWalkSpeed = 2.0f;    // 限制移動的速度值

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();  // 取得貓咪的剛體
        animator = GetComponent<Animator>();    // 取得貓咪的動畫控制器
    }

    void Update()
    {
        // 跳躍
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("JumpTrigger"); // 觸發跳躍動畫
            rigid2D.AddForce(transform.up * jumpForce);
        }

        // 左右移動
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        // 遊戲角色的速度
        float speedx = Mathf.Abs(rigid2D.velocity.x);

        // 速度限制
        if (speedx < maxWalkSpeed)
        {
            rigid2D.AddForce(transform.right * key * walkForce);
        }

        // (追加)依照行進方向翻轉
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // 依遊戲角色的速度改變動畫的速度
        if (rigid2D.velocity.y == 0)
        {
            animator.speed = speedx / 2.0f; // 如果Y軸速度為0，速度值除以二
        }
        else
        {
            animator.speed = 1.0f;
        }
    }

    // 抵達終點
    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene("ClearScene");
    }

}
