using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Kanji;

public class QuestionSelectorTemporary2 : IQuestionSelector
{
    public void Initialize() { }

    public QuestionData GetQuestionData(QuestionFilter filter) 
    {
        QuestionData[] datas = new QuestionData[]
        {
            // �ꕶ������
            new QuestionData { kanji = "�O", answers = new string[] { "���ڂ�" }},
            new QuestionData { kanji = "�T", answers = new string[] { "����" }},
            new QuestionData { kanji = "��", answers = new string[] { "�܂�" }},
            new QuestionData { kanji = "�O", answers = new string[] { "�����т�" }},
            new QuestionData { kanji = "��", answers = new string[] { "�Ă̂Ђ�","���Ȃ�����" }},

            // �񎚏n��
            new QuestionData { kanji = "�Ǎ�", answers = new string[] { "������" }},
            new QuestionData { kanji = "�D��", answers = new string[] { "���イ��" }},
            new QuestionData { kanji = "�W�", answers = new string[] { "������" }},
            new QuestionData { kanji = "�I��", answers = new string[] { "������" }},
            new QuestionData { kanji = "Ῐf", answers = new string[] { "����킭" }},
            new QuestionData { kanji = "����", answers = new string[] { "��������" }},
            new QuestionData { kanji = "���", answers = new string[] { "�Ȃ���" }},
            new QuestionData { kanji = "����", answers = new string[] { "������" }},
            new QuestionData { kanji = "�Ԍ�", answers = new string[] { "���񂰂�" }},
            new QuestionData { kanji = "�މ�", answers = new string[] { "���傤��" }},
            new QuestionData { kanji = "����", answers = new string[] { "���Â�" }},
            new QuestionData { kanji = "�@��", answers = new string[] { "�ɂ傶��" }},
            new QuestionData { kanji = "�|�M", answers = new string[] { "�ق�낤" }},
            new QuestionData { kanji = "���L", answers = new string[] { "�����т傤" }},

            // �O�����n��
            new QuestionData { kanji = "�t�H�w", answers = new string[] { "����񂶂イ����" }},
            new QuestionData { kanji = "�`�E�R", answers = new string[] { "���䂤����" }},
            new QuestionData { kanji = "�Đ_�_", answers = new string[] { "�͂񂵂���" }},
            new QuestionData { kanji = "�G���{", answers = new string[] { "����������" }},
            new QuestionData { kanji = "�䎞�J", answers = new string[] { "���݂�����" }},
            new QuestionData { kanji = "�H����", answers = new string[] { "�����" }},
            new QuestionData { kanji = "�����a", answers = new string[] { "�ӂ��܂ł�" }},

            // �l���n��
            new QuestionData { kanji = "�בR����", answers = new string[] { "�������񂶂��Ⴍ" }},
            new QuestionData { kanji = "�r�����", answers = new string[] { "�悤�Ƃ����ɂ�" }},
            new QuestionData { kanji = "�������", answers = new string[] { "��イ����Ђ�" }},
            new QuestionData { kanji = "�Ȋw����", answers = new string[] { "���傭����������" }},
            new QuestionData { kanji = "���q���v", answers = new string[] { "�����ӂ񂶂�" }},

            // ����
            new QuestionData { kanji = "搂�", answers = new string[] { "������" }},
            new QuestionData { kanji = "����", answers = new string[] { "�����܂�" }},
            new QuestionData { kanji = "����", answers = new string[] { "������" }},
            new QuestionData { kanji = "�Â�", answers = new string[] { "���̂�" }},
            new QuestionData { kanji = "���߂�", answers = new string[] { "�����߂�" }},
            new QuestionData { kanji = "������", answers = new string[] { "���т₩��" }},
            new QuestionData { kanji = "����", answers = new string[] { "��������" }},
            new QuestionData { kanji = "�s����", answers = new string[] { "����������" }},
            new QuestionData { kanji = "����", answers = new string[] { "�����̂���" }},

            // �`�e��
            new QuestionData { kanji = "�d��", answers = new string[] { "�悤����" }},
            new QuestionData { kanji = "�s��", answers = new string[] { "����񂰂�" }},
            new QuestionData { kanji = "����", answers = new string[] { "��������" }},
            new QuestionData { kanji = "����", answers = new string[] { "����ꂢ" }},
            new QuestionData { kanji = "�c��", answers = new string[] { "�䂪��" }},
            new QuestionData { kanji = "����", answers = new string[] { "�˂񂲂�" }},

            // ����ȓǂݕ��̒n��
            new QuestionData { kanji = "�t���s", answers = new string[] { "������Ȃ���" }},
            new QuestionData { kanji = "�튊�s", answers = new string[] { "�Ƃ��Ȃ߂�" }},
            new QuestionData { kanji = "����", answers = new string[] { "������イ��" }},
            new QuestionData { kanji = "�Ɣn�R", answers = new string[] { "����܂��" }},

            // �̐l�̖��O
            new QuestionData { kanji = "�F��׎R", answers = new string[] { "���܂���΂񂴂�" }},
            new QuestionData { kanji = "���ꌹ��", answers = new string[] { "�Ђ炪����Ȃ�" }},
            new QuestionData { kanji = "�ɓ��m��", answers = new string[] { "���Ƃ����񂳂�" }}
        };


        return datas[Random.Range(0, datas.Length)];
    }
}
