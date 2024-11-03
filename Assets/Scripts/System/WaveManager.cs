using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

[System.Serializable]
class WaveStatus
{
    [SerializeField] EnemySpawner spawner;
    [SerializeField] AnimationCurve curve;

    public float GetSpawnNum(float timeRatio) { return curve.Evaluate(timeRatio); }

    public void Initialize()
    {
        spawner.Initialize();
    }

    public void SpawnEnemy(EnemyInitializationData enemyInitializationData) 
    { 
        spawner.SpawnEnemy(enemyInitializationData); 
    }
}

public class WaveManager : MonoBehaviour
{
    [SerializeField] float maxTime = 30f;
    [SerializeField] float spawnInterval;
    [SerializeField] WaveStatus[] waveStatuses;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] QuestionFilter questionFilter;

    //WAVE終了時のコールバック
    private Subject<Unit> onEndWaveSubject = new Subject<Unit>();
    public IObservable<Unit> OnEndWaveAsObservable => onEndWaveSubject;

    //WAVE終了時
    private Subject<Unit> onEndWaveFinishingSubject = new Subject<Unit>();
    public IObservable<Unit> OnEndWaveFinishingAsObservable => onEndWaveFinishingSubject;

    //各種ステータス
    float TimeRatio => time / maxTime;
    bool isWorking = false;
    float time;
    float intervalCount;
    Transform playerTransform;
    KanjiObjectSpawner kanjiSpawner;
    IQuestionSelector questionSelector;
    Vector3[] spawnPositions;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize(Transform playerTransform, KanjiObjectSpawner kanjiSpawner, IQuestionSelector questionSelector)
    {
        time = 0;
        intervalCount = float.MaxValue;

        this.playerTransform = playerTransform;
        this.kanjiSpawner = kanjiSpawner;
        this.questionSelector = questionSelector;

        //スポーンポイントの設定
        spawnPositions = new Vector3[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPositions[i] = spawnPoints[i].position;
        }

        //スポナーの初期化
        foreach (WaveStatus w in waveStatuses)
        {
            w.Initialize();
        }
    }

    /// <summary>
    /// Wave開始
    /// </summary>
    public void StartWave() 
    {
        isWorking = true;
    }

    /// <summary>
    /// Wave終了
    /// </summary>
    public void FinishWave()
    {
        isWorking = false;
        EndOfWaveFinish();
    }

    /// <summary>
    /// 時間の更新に伴う処理
    /// </summary>
    private void Update()
    {
        if (!isWorking) { return; }

        //時間の加算
        time += Time.deltaTime;
        intervalCount += Time.deltaTime;

        //敵をスポーンさせる
        if (intervalCount > spawnInterval) 
        {
            intervalCount = 0;
            SpawnEnemy();
        }

        //Wave終了
        if (time > maxTime)
        {
            EndOfWave();
        }
    }

    /// <summary>
    /// 敵のスポーン
    /// </summary>
    private void SpawnEnemy()
    {
        if (questionSelector == null) { return; }

        for (int i = 0; i < waveStatuses.Length; i++)
        {
            //ウェーブ内の敵の数毎
            WaveStatus wave = waveStatuses[i];
            for (int spawnNum = 0; spawnNum < wave.GetSpawnNum(TimeRatio); spawnNum++)
            {
                //問題の選定
                QuestionData data = questionSelector.GetQuestionData(questionFilter);
                //漢字の生成
                GameObject kanjiObject = kanjiSpawner.SpawnKanji(data);
                //スポーン
                wave.SpawnEnemy(GenerateEnemyInitializationData(kanjiObject, data));
            }
        }
    }

    /// <summary>
    /// WAVE終了時
    /// </summary>
    private void EndOfWave()
    {
        //コールバック発火
        onEndWaveSubject.OnNext(Unit.Default);
    }

    /// <summary>
    /// WAVE終了処理完了時
    /// </summary>
    private void EndOfWaveFinish()
    {
        //コールバック発火
        onEndWaveFinishingSubject.OnNext(Unit.Default);
    }

    private EnemyInitializationData GenerateEnemyInitializationData(GameObject kanjiObject, QuestionData questionData)
    {
        EnemyInitializationData enemyInitializationData = new EnemyInitializationData
        {
            spawnPoint = SelectionSpawnpoint(),
            target = playerTransform,
            kanjiObject = kanjiObject,
            questionData = questionData
        };

        return enemyInitializationData;
    }

    private Transform SelectionSpawnpoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
    }
}