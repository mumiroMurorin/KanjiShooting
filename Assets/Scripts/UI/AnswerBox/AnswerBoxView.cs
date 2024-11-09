using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kanji;

public abstract class AnswerBoxView : MonoBehaviour
{
    [Header("KanjiText")]
    [SerializeField] TextMeshProUGUI kanjiTmp;
    [Header("AnswerText")]
    [SerializeField] TextMeshProUGUI answerTmp;
    [SerializeField] int maxAnswerStringLength = 12;

    private void Start()
    {
        AfterSpawn();
    }

    /// <summary>
    /// 答えのセット
    /// </summary>
    /// <param name="questionData"></param>
    public void SetAnswer(QuestionData questionData)
    {           
        //漢字の代入
        kanjiTmp.text = questionData.kanji;

        //答えの代入
        answerTmp.text = GetAnswersLaw(questionData.answers);
    }

    /// <summary>
    /// 文字列配列を1次元にする
    /// </summary>
    /// <param name="answers"></param>
    /// <returns></returns>
    protected string GetAnswersLaw(string[] answers)
    {
        string answerText = "";
        for (int i = 0; i < answers.Length; i++)
        {
            //最大文字数をはみ出るようであれば出る
            if (answerText.Length + answers[i].Length > maxAnswerStringLength) { break; }
            answerText += answers[i];
            //最後の要素じゃなければ分割
            if (i < answers.Length - 1) { answerText += ", "; }
        }
        return answerText;
    }

    /// <summary>
    /// 生成後の挙動
    /// </summary>
    protected abstract void AfterSpawn();

    public void DestroyTrigger()
    {
        Destroy(this.gameObject);
    }
}
