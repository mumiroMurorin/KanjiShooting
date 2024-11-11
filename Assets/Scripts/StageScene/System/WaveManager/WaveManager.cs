using System;
using UnityEngine;
using UniRx;
using StageTransition;

public abstract class WaveManager : MonoBehaviour
{
    [SerializeField] protected float maxTime = 30f;
    [SerializeField] protected Transform[] spawnPoints;

    //WAVE終了時のコールバック
    private Subject<Unit> onEndWaveSubject = new Subject<Unit>();
    public IObservable<Unit> OnEndWaveAsObservable => onEndWaveSubject;

    //WAVE終了時
    private Subject<Unit> onEndWaveFinishingSubject = new Subject<Unit>();
    public IObservable<Unit> OnEndWaveFinishingAsObservable => onEndWaveFinishingSubject;

    //各種ステータス
    protected float TimeRatio => time / maxTime;
    protected Transform PlayerTransform;
    protected bool isWorking = false;
    protected float time;
    protected float intervalCount;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize(Transform playerTransform)
    {
        time = 0;
        intervalCount = float.MaxValue;
        this.PlayerTransform = playerTransform;

        Bind();
        AfterInitialize();
    }

    protected abstract void AfterInitialize();

    private void Bind()
    {
        //ステージが終了したときWaveを停止する
        StageManager.Instance.CurrentStageStatusreactiveproperty
            .Where(status => status == StageStatus.StageFinish)
            .Subscribe(status => { isWorking = false; })
            .AddTo(this.gameObject);
    }

    /// <summary>
    /// Wave開始時の処理
    /// </summary>
    public abstract void StartWave();

    /// <summary>
    /// Wave終了時の処理
    /// </summary>
    public abstract void FinishWave();

    /// <summary>
    /// 敵のスポーン
    /// </summary>
    protected abstract void SpawnEnemy();

    /// <summary>
    /// アップデート後の処理
    /// </summary>
    protected abstract void AfterUpdate();

    /// <summary>
    /// 時間の更新に伴う処理
    /// </summary>
    protected virtual void Update()
    {
        if (!isWorking) { return; }

        //時間の加算
        time += Time.deltaTime;
        intervalCount += Time.deltaTime;
        ScoreManager.Instance.AddTime(Time.deltaTime);

        //それぞれの処理へ
        AfterUpdate();
    }

    /// <summary>
    /// WAVE終了時
    /// </summary>
    protected void EndOfWave()
    {
        //コールバック発火
        onEndWaveSubject.OnNext(Unit.Default);
    }

    /// <summary>
    /// WAVE終了処理完了時
    /// </summary>
    protected void EndOfWaveFinish()
    {
        //コールバック発火
        onEndWaveFinishingSubject.OnNext(Unit.Default);
    }

    /// <summary>
    /// エネミーの初期化データを生成
    /// </summary>
    /// <param name="kanjiObject"></param>
    /// <param name="questionData"></param>
    /// <returns></returns>
    protected EnemyInitializationData GenerateEnemyInitializationData()
    {
        EnemyInitializationData enemyInitializationData = new EnemyInitializationData
        {
            spawnPoint = SelectionSpawnpoint(),
            target = PlayerTransform,
        };

        return enemyInitializationData;
    }

    /// <summary>
    /// スポーンポイントのランダム選出 (別クラスにすべきだが一旦)
    /// </summary>
    /// <returns></returns>
    private Transform SelectionSpawnpoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
    }
}