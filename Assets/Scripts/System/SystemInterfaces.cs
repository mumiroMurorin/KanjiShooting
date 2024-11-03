using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

/// <summary>
/// ���̃f�[�^
/// </summary>
public struct QuestionData
{
    public string kanji;
    public string furigana;
    public string[] answers;
}

/// <summary>
/// ���Z���N�^�[
/// </summary>
public interface IQuestionSelector
{
    public QuestionData GetQuestionData(QuestionFilter filter);
}

/// <summary>
/// �X�e�[�W���t�F�[�Y�̑J��
/// </summary>
public interface IStagePhaseTransitioner
{
    UniTask ExecuteAsync(CancellationToken cancellationToken);
}