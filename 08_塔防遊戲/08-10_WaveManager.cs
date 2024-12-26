using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 單個敵人的生成設定
[Serializable]
public class EnemySpawnData
{
    public GameObject enemyPrefab;    // 敵人預製體
    public int count = 1;             // 該類型敵人的數量
    public float spawnDelay = 1f;     // 同類型敵人的生成間隔
}

// 一個波次的設定
[Serializable]
public class WaveData
{
    public string waveName = "Wave";           // 波次名稱
    public float waveDelay = 3f;              // 該波次開始前的等待時間
    public List<EnemySpawnData> enemies;      // 該波次包含的敵人
}

public class WaveManager : MonoBehaviour
{
    [Header("波次設定")]
    [SerializeField] private List<WaveData> waves;              // 所有波次的資料
    [SerializeField] private Transform[] spawnPoints;           // 生成點
    [SerializeField] private bool autoStart = false;            // 是否自動開始

    [Header("UI 參考")]
    [SerializeField] private TMPro.TextMeshProUGUI waveText;   // 波次顯示文字

    private int currentWave = -1;
    private int totalEnemiesInWave;
    private int remainingEnemies;
    private bool isWaveActive = false;

    // 事件系統
    public event Action<int> OnWaveStart;         // 波次開始時觸發
    public event Action<int> OnWaveComplete;      // 波次結束時觸發
    public event Action OnAllWavesComplete;       // 所有波次結束時觸發

    private void Start()
    {
        if (autoStart)
        {
            StartNextWave();
        }
    }

    public void StartNextWave()
    {
        if (isWaveActive || currentWave >= waves.Count - 1) return;

        currentWave++;
        StartCoroutine(SpawnWave(waves[currentWave]));
    }

    private IEnumerator SpawnWave(WaveData wave)
    {
        isWaveActive = true;

        // 等待波次開始延遲
        yield return new WaitForSeconds(wave.waveDelay);

        // 計算該波次的總敵人數
        totalEnemiesInWave = 0;
        foreach (var enemyData in wave.enemies)
        {
            totalEnemiesInWave += enemyData.count;
        }
        remainingEnemies = totalEnemiesInWave;

        // 觸發波次開始事件
        OnWaveStart?.Invoke(currentWave);
        UpdateWaveUI();

        // 生成所有敵人
        foreach (var enemyData in wave.enemies)
        {
            for (int i = 0; i < enemyData.count; i++)
            {
                SpawnEnemy(enemyData.enemyPrefab);
                yield return new WaitForSeconds(enemyData.spawnDelay);
            }
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        if (spawnPoints == null || spawnPoints.Length == 0) return;

        // 隨機選擇生成點
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

        // 生成敵人
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // 監聽敵人死亡
        if (enemy.TryGetComponent<Enemy>(out var enemyComponent))
        {
            enemyComponent.OnEnemyDeath += HandleEnemyDeath;
        }
    }

    private void HandleEnemyDeath(Enemy enemy)
    {
        // 取消訂閱事件
        enemy.OnEnemyDeath -= HandleEnemyDeath;

        remainingEnemies--;

        // 檢查波次是否結束
        if (remainingEnemies <= 0)
        {
            OnWaveComplete?.Invoke(currentWave);
            isWaveActive = false;

            // 檢查是否所有波次都完成
            if (currentWave >= waves.Count - 1)
            {
                OnAllWavesComplete?.Invoke();
            }
        }

        UpdateWaveUI();
    }

    private void UpdateWaveUI()
    {
        if (waveText != null)
        {
            waveText.text = $"Wave {currentWave + 1}/{waves.Count}\nEnemies: {remainingEnemies}/{totalEnemiesInWave}";
        }
    }
}