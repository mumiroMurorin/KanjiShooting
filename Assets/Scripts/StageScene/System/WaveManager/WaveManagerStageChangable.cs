using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kanji;
using System.Threading;
using Cysharp.Threading.Tasks;

public class WaveManagerStageChangable : WaveManager
{
    [SerializeField] KanjiObjectSpawner kanjiSpawner;
    [SerializeReference, SubclassSelector] IQuestionSelector questionSelector;
    [SerializeField] AudioClip bgm;

    [Header("ステージ遷移までの待ち時間")]
    [SerializeField] float waitingTime;

    [SerializeField] SerializeInterface<IStageChanger> stageChanger;

    CancellationTokenSource cts;

    /// <summary>
    /// 初期化
    /// </summary>
    protected override void AfterInitialize()
    {
        questionSelector.Initialize();
        cts = new CancellationTokenSource();

        //スポナーの初期化
        foreach (IWaveStatus w in waveStatuses)
        {
            w.Initialize(questionSelector, kanjiSpawner);
        }
    }

    /// <summary>
    /// Wave開始
    /// </summary>
    public override void StartWave()
    {
        isWorking = true;
        Sound.SoundManager.Instance.PlayBGM(bgm); //BGMの再生
    }

    /// <summary>
    /// Wave終了
    /// </summary>
    public override void FinishWave()
    {
        isWorking = false;

        // BGMの停止
        Sound.SoundManager.Instance.StopBGM(true);
        // 全ての敵をデスポーンさせる
        foreach (IWaveStatus wave in waveStatuses) { wave.DespawnEnemy(); }

        // ステージ変更
        ChangeStage(cts.Token).Forget();
    }

    protected override void SpawnEnemy()
    {
        if (questionSelector == null) { return; }

        foreach(IWaveStatus waveStatus in waveStatuses)
        {
            waveStatus.SpawnEnemy(TimeRatio, GenerateEnemyInitializationData());
        }
    }

    protected override void AfterUpdate()
    {
        SpawnEnemy();

        //Wave終了
        if (time > maxTime)
        {
            //再生停止
            //Sound.SoundManager.Instance.StopBGM(true);
            EndOfWave();
        }
    }

    /// <summary>
    /// ステージ遷移の非同期処理(ラップ)
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTaskVoid ChangeStage(CancellationToken token)
    {
        // 指定された秒数待つ
        await UniTask.WaitForSeconds(waitingTime, cancellationToken: token);

        // ステージ変更開始
        stageChanger.Value.ChangeStage(EndOfWaveFinish);
    }

    private void OnDestroy()
    {
        cts.Cancel();
        cts.Dispose();
    }
}