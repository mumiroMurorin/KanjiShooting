using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using unityroom.Api;

public class ScoreHolder
{
    // ステージデータ
    ReactiveProperty<StageDetailData> stageData = new ReactiveProperty<StageDetailData>();
    public StageDetailData StageData { set { stageData.Value = value; } }
    public IReadOnlyReactiveProperty<StageDetailData> StageDataReactiveProperty { get { return stageData; } }

    // Wave数
    ReactiveProperty<int> waveCount = new ReactiveProperty<int>();
    public int WaveCount { set { waveCount.Value = value; } }
    public IReadOnlyReactiveProperty<int> WaveCountReactiveProperty { get { return waveCount; } }
    public void IncrementWaveCount()
    {
        waveCount.Value++;
    }

    // 撃破数
    ReactiveProperty<int> killCount = new ReactiveProperty<int>();
    public int KillCount { set { killCount.Value = value; } }
    public IReadOnlyReactiveProperty<int> KillCountReactiveProperty { get { return killCount; } }
    public void IncrementKillCount()
    {
        killCount.Value++;
    }

    // 経過時間
    ReactiveProperty<float> timeCount = new ReactiveProperty<float>();
    public float TimeCount { set { timeCount.Value = value; } }
    public IReadOnlyReactiveProperty<float> TimeCountreactiveProperty { get { return timeCount; } }
    public void AddTime(float value)
    {
        timeCount.Value += value;
    }
    public void RecordSurvivalTimeScoreForUnityRoom()
    {
        Debug.Log("【System】UnityRoomにスコア送信");
        UnityroomApiClient.Instance.SendScore(1, timeCount.Value, ScoreboardWriteMode.HighScoreAsc);
    }

    // 漢字レコード
    private ReactiveCollection<AnswerState> answerStates = new ReactiveCollection<AnswerState>();
    public IReadOnlyReactiveCollection<AnswerState> AnswerStatesReactiveCollection { get { return answerStates; } }
    public void AddAnswerState(AnswerState answerState) { answerStates.Add(answerState); }
}
