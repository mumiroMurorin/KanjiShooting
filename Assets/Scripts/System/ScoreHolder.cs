using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHolder
{
    // ステージデータ
    public StageDetailData stageData;
    // ウェーブ数
    public int waveCount;
    // 撃破数
    public int killCount;
    // 経過時間
    public float timeCount;
    // 解答データ
    public List<AnswerState> answerStates;
}
