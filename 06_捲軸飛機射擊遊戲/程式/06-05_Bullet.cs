using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
		Destroy(gameObject); //刪除子彈物件
    }
}
