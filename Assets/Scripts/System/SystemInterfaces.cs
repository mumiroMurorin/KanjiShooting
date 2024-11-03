using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

/// <summary>
/// 問題のデータ
/// </summary>
public struct QuestionData
{
    public string kanji;
    public string furigana;
    public string[] answers;
}

/// <summary>
/// 問題セレクター
/// </summary>
public interface IQuestionSelector
{
    public QuestionData GetQuestionData(QuestionFilter filter);
}

/// <summary>
/// ステージ内フェーズの遷移
/// </summary>
public interface IStagePhaseTransitioner
{
    UniTask ExecuteAsync(CancellationToken cancellationToken);
}