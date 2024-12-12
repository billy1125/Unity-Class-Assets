using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 1; // 移動速度
    public GameObject ExplosionPrefab; // 爆炸效果的預製物件

    void Start()
    {
        Destroy(gameObject, 2); //設定2秒後子彈物件被刪除
    }

    void FixedUpdate()
    {
        gameObject.transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime; //子彈會不斷往上移動
    }
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Wall") //如果碰撞的標籤不是Wall(的以外物件)
        {
            Instantiate(ExplosionPrefab, transform.position, transform.rotation); //在子彈碰撞的位置產生爆炸
        }
		Destroy(gameObject); //刪除子彈物件
    }
}
