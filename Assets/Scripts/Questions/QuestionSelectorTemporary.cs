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
            new QuestionData { kanji = "�K���`", answers = new string[] { "�����Ƃ���" }},
            new QuestionData { kanji = "�w", answers = new string[] { "�̂�" }},
            new QuestionData { kanji = "���t��", answers = new string[] { "����߂���" }},
            new QuestionData { kanji = "�h�t", answers = new string[] { "���������" }},
            new QuestionData { kanji = "��冋o", answers = new string[] { "�Ƃ����낱��" }},
            new QuestionData { kanji = "��", answers = new string[] { "�Ȃ�" }},
            new QuestionData { kanji = "�͓�", answers = new string[] { "�ӂ�" }},
            new QuestionData { kanji = "���ޑ�", answers = new string[] { "����Ԃ���" }},
            new QuestionData { kanji = "�", answers = new string[] { "�ӂ邢" }},
            new QuestionData { kanji = "��ԍ�", answers = new string[] { "������" }},
            new QuestionData { kanji = "�S��x", answers = new string[] { "���ɂ���" }},
            new QuestionData { kanji = "����", answers = new string[] { "�Ă�����" }},
            new QuestionData { kanji = "吋�", answers = new string[] { "�Ƃ���" }}
        };


        return datas[Random.Range(0, datas.Length)];
    }
}
