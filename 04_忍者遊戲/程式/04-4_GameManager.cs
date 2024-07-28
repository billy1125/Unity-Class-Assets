using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject arrowPrefab; //置放Prefab的變數
    float span = 1.0f; //時間間隔
    float delta = 0; //現在已經累積的時間

    void Update()
    {
        delta += Time.deltaTime; //每一個影格經過的時間，都累加到delta中
        if (delta > span)  // 只要delta累積到大於span（大於1秒），就會產生一個箭頭
        {
            delta = 0;
            int px = Random.Range(-6, 7); // 隨機產生一個-6到7之間的數字
            Instantiate(arrowPrefab, new Vector3(px, 7, 0), Quaternion.identity); // 產生箭頭物件
        }
    }
}
