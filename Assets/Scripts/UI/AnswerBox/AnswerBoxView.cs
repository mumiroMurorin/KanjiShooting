using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class AnswerBoxView : MonoBehaviour
{
    [Header("KanjiText")]
    [SerializeField] TextMeshProUGUI kanjiTmp;
    [Header("AnswerText")]
    [SerializeField] TextMeshProUGUI answerTmp;
    [SerializeField] int maxAnswerStringLength = 12;

    /// <summary>
    /// �����̃Z�b�g
    /// </summary>
    /// <param name="questionData"></param>
    public void SetAnswer(QuestionData questionData)
    {           
        //�����̑��
        kanjiTmp.text = questionData.kanji;

        //�����̑��
        answerTmp.text = GetAnswersLaw(questionData.answers);
    }

    /// <summary>
    /// ������z���1�����ɂ���
    /// </summary>
    /// <param name="answers"></param>
    /// <returns></returns>
    protected string GetAnswersLaw(string[] answers)
    {
        string answerText = "";
        for (int i = 0; i < answers.Length; i++)
        {
            //�ő啶�������͂ݏo��悤�ł���Ώo��
            if (answerText.Length + answers[i].Length > maxAnswerStringLength) { break; }
            //�Ō�̗v�f����Ȃ���Ε���
            if (i < answers.Length - 1) { answerText += ", "; }
            answerText += answers[i];
        }
        return answerText;
    }

    /// <summary>
    /// ������̋���
    /// </summary>
    protected abstract void AfterSpawn();
}
