using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    //���j��
    ReactiveProperty<int> killCount = new ReactiveProperty<int>();
    public int KillCount { set { killCount.Value = value; } }
    public IReadOnlyReactiveProperty<int> KillCountReactiveProperty { get { return killCount; } }
    public void IncrementKillCount()
    {
        killCount.Value++;
    }

    //�������R�[�h
    private ReactiveCollection<AnswerState> answerStates = new ReactiveCollection<AnswerState>();
    public IReadOnlyReactiveCollection<AnswerState> AnswerStatesReactiveCollection { get { return answerStates; } }
    public void AddAnswerState(AnswerState answerState) { answerStates.Add(answerState); }

}
