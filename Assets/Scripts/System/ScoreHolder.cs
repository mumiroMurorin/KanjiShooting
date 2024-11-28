using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using unityroom.Api;

public class ScoreHolder
{
    // �G���g�����X�̏������
    public EntranceTransition.MenuStatus InitialEntranceMenuStatus { get; set; } = EntranceTransition.MenuStatus.Title;

    // �X�e�[�W�f�[�^
    ReactiveProperty<StageDetailData> stageData = new ReactiveProperty<StageDetailData>();
    public StageDetailData StageData { set { stageData.Value = value; } }
    public IReadOnlyReactiveProperty<StageDetailData> StageDataReactiveProperty { get { return stageData; } }

    // Wave��
    ReactiveProperty<int> waveCount = new ReactiveProperty<int>();
    public int WaveCount { set { waveCount.Value = value; } }
    public IReadOnlyReactiveProperty<int> WaveCountReactiveProperty { get { return waveCount; } }
    public void IncrementWaveCount()
    {
        waveCount.Value++;
    }

    // ���j��
    ReactiveProperty<int> killCount = new ReactiveProperty<int>();
    public int KillCount { set { killCount.Value = value; } }
    public IReadOnlyReactiveProperty<int> KillCountReactiveProperty { get { return killCount; } }
    public void IncrementKillCount()
    {
        killCount.Value++;
    }

    // �o�ߎ���
    ReactiveProperty<float> timeCount = new ReactiveProperty<float>();
    public float TimeCount { set { timeCount.Value = value; } }
    public IReadOnlyReactiveProperty<float> TimeCountReactiveProperty { get { return timeCount; } }
    public void AddTime(float value)
    {
        timeCount.Value += value;
    }
    public void RecordSurvivalTimeScoreForUnityRoom()
    {
        Debug.Log("�ySystem�zUnityRoom�ɃX�R�A���M");
        UnityroomApiClient.Instance.SendScore(1, timeCount.Value, ScoreboardWriteMode.HighScoreAsc);
    }

    // �������R�[�h
    private ReactiveCollection<AnswerStatus> answerStates = new ReactiveCollection<AnswerStatus>();
    public IReadOnlyReactiveCollection<AnswerStatus> AnswerStatesReactiveCollection { get { return answerStates; } }
    public void AddAnswerStatus(AnswerStatus answerStatus) { answerStates.Add(answerStatus); }
}
