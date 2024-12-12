using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject EnemyAirplanePrefab; //戰機預置物件
    public float span = 1.0f;
    public float delta = 0;

    int intScore = 0;
    public Text ScoreText;

    public GameObject StarFighterPrefab; //戰機預置物件
    public GameObject StartPoint; //戰機產生的起點

    GameObject StarFighter;

    void Start()
    {
        ScoreText.text = "分數：0"; //初始化分數表
        InitialStarFighter(); //初始化戰機
    }

    void Update()
    {
        //以下我們參考之前做的貓咪忍者遊戲，想想看如何拿箭頭產生的程式，修改成為外星怪物的產生程式
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            float px = Random.Range(-3.0f, 3.0f); // 這次我們產生的是-3到3之間的浮點數
            Instantiate(EnemyAirplanePrefab, new Vector3(px, 7, 0), Quaternion.identity);
        }

        if (StarFighter == null) //這樣不是辦法....
        {
            InitialStarFighter();
        }
    }

    // 加分的方法（同學要複習一下C#函式與怎麼設定函式參數）
    public void IncreaseScore(int _score)
    {
        intScore += _score;
        ScoreText.text = "分數：" + intScore;
    }

    //初始化戰機的方法
    public void InitialStarFighter()
    {
        intScore = 0;
        IncreaseScore(intScore);
        StarFighter = Instantiate(StarFighterPrefab, StartPoint.transform.position, StartPoint.transform.rotation);
    }
}