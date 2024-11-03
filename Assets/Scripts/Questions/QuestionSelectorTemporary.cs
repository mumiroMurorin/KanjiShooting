using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class QuestionSelectorTemporary : MonoBehaviour, IQuestionSelector
{
    public QuestionData GetQuestionData(QuestionFilter filter) 
    {
        QuestionData[] datas = new QuestionData[]
        {
            new QuestionData { kanji = "膃肭臍", answers = new string[] { "おっとせい" }},
            new QuestionData { kanji = "鑿", answers = new string[] { "のみ" }},
            new QuestionData { kanji = "金鍍金", answers = new string[] { "きんめっき" }},
            new QuestionData { kanji = "吃逆", answers = new string[] { "しゃっくり" }},
            new QuestionData { kanji = "玉蜀黍", answers = new string[] { "とうもろこし" }},
            new QuestionData { kanji = "鉈", answers = new string[] { "なた" }},
            new QuestionData { kanji = "河豚", answers = new string[] { "ふぐ" }},
            new QuestionData { kanji = "卓袱台", answers = new string[] { "ちゃぶだい" }},
            new QuestionData { kanji = "篩", answers = new string[] { "ふるい" }},
            new QuestionData { kanji = "雪花菜", answers = new string[] { "おから" }},
            new QuestionData { kanji = "鬼蜻蛉", answers = new string[] { "おにやんま" }},
            new QuestionData { kanji = "手薬煉", answers = new string[] { "てぐすね" }},
            new QuestionData { kanji = "蜷局", answers = new string[] { "とぐろ" }}
        };


        return datas[Random.Range(0, datas.Length)];
    }
}
