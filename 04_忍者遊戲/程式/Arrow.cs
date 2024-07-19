using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3); // 3秒後，刪除自己
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 當箭頭碰到其他有碰撞體的東西時
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); // 碰到有碰撞體的東西就刪除自己
    }
}
