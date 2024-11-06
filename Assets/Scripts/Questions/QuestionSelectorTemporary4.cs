using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class QuestionSelectorTemporary4 : MonoBehaviour, IQuestionSelector
{
    public QuestionData GetQuestionData(QuestionFilter filter) 
    {
        QuestionData[] datas = new QuestionData[]
        {
            new QuestionData { kanji = "�w", answers = new string[] { "�̂�" }},
            new QuestionData { kanji = "����", answers = new string[] { "����������" }},
            new QuestionData { kanji = "���ޑ�", answers = new string[] { "����Ԃ���" }},
            new QuestionData { kanji = "�͓�", answers = new string[] { "�ӂ�" }},
            new QuestionData { kanji = "�K���`", answers = new string[] { "�����Ƃ���" }},
            new QuestionData { kanji = "�Z�J", answers = new string[] { "���ǂ�" }},
            new QuestionData { kanji = "��", answers = new string[] { "�Ȃ�" }},
            new QuestionData { kanji = "�ނ��", answers = new string[] { "�������" }},
            new QuestionData { kanji = "�`", answers = new string[] { "�����̂Ƃ�" }},
            new QuestionData { kanji = "��", answers = new string[] { "����" }},
            new QuestionData { kanji = "婒J", answers = new string[] { "���߂���" }},
            new QuestionData { kanji = "��", answers = new string[] { "�����났" }},
            new QuestionData { kanji = "�������", answers = new string[] { "���傤��傤�΂���" }},
            new QuestionData { kanji = "��������", answers = new string[] { "�炢�炢�炭�炭" }},
            new QuestionData { kanji = "�Ⴎ", answers = new string[] { "������","������","" }},
            new QuestionData { kanji = "᷉�", answers = new string[] { "���炢�܂킵" }},
            new QuestionData { kanji = "����", answers = new string[] { "�͂��Ⴎ" }},
            new QuestionData { kanji = "����", answers = new string[] { "���낮" }},
            new QuestionData { kanji = "崂�", answers = new string[] { "�킾���܂�" }},
            new QuestionData { kanji = "������", answers = new string[] { "�����炤" }},
            new QuestionData { kanji = "��ɂ�", answers = new string[] { "��ȂȂ�" }},
            new QuestionData { kanji = "�킭", answers = new string[] { "���̂̂�" }},
            new QuestionData { kanji = "�킮", answers = new string[] { "���悮" }},
            new QuestionData { kanji = "��", answers = new string[] { "�݂���" }},
            new QuestionData { kanji = "�R��", answers = new string[] { "�킳��" }},
            new QuestionData { kanji = "���Z", answers = new string[] { "�ւ���" }},
            new QuestionData { kanji = "���_��", answers = new string[] { "���񂪂��̂����傤" }},
            new QuestionData { kanji = "�r�U��", answers = new string[] { "�����Ԃ�" }},
            new QuestionData { kanji = "��", answers = new string[] { "�������" }},
            new QuestionData { kanji = "��冋o", answers = new string[] { "�Ƃ����낱��" }},
            new QuestionData { kanji = "�h�t", answers = new string[] { "���������","�����Ⴍ" }},
            new QuestionData { kanji = "�h��", answers = new string[] { "�т�����","�������傤" }},
            new QuestionData { kanji = "�ނ���", answers = new string[] { "���т���" }},
            new QuestionData { kanji = "����", answers = new string[] { "��������" }},
            new QuestionData { kanji = "�D�^", answers = new string[] { "�ł��˂�","�ʂ����" }},
            new QuestionData { kanji = "�҉����_�F", answers = new string[] { "������������" }},
            new QuestionData { kanji = "�ߌȖ���", answers = new string[] { "�����݂�" }},
            new QuestionData { kanji = "��", answers = new string[] { "������" }},
            new QuestionData { kanji = "�̂�", answers = new string[] { "�����ڂ�" }},
        };


        return datas[Random.Range(0, datas.Length)];
    }
}
