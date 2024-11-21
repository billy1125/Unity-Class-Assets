using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ExplosionPrefab;

    void Start()
    {
        Destroy(gameObject, 2); //設定2秒後子彈物件被刪除
    }

    void FixedUpdate()
    {
        gameObject.transform.position += new Vector3(0, 0.5f, 0); //子彈會不斷往上移動
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(ExplosionPrefab, transform.position, transform.rotation); //在子彈碰撞的位置產生爆炸
		Destroy(gameObject); //刪除子彈物件，一定要先有爆炸動畫，再刪除
    }
}
