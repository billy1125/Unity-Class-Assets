using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    public float MoveSpeed = 0.005f; //敵機的速度設定值

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5); //設定10秒後敵機物件被刪除
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(0, -1 * MoveSpeed, 0); 
        transform.Translate(Vector3.down * MoveSpeed) * Time.deltaTime; //敵機會不斷往下移動，使用Translate
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet") //如果碰撞的標籤是Bullet
        {
            Destroy(gameObject); //刪除外星怪物物件
        }
    }
}
