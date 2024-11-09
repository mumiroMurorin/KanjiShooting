using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Kanji;

public class QuestionSelectorTemporary1 : IQuestionSelector
{
    public void Initialize() { }

    public QuestionData GetQuestionData(QuestionFilter filter) 
    {
        QuestionData[] datas = new QuestionData[]
        {
            new QuestionData { kanji = "�T", answers = new string[] { "����" }},
            new QuestionData { kanji = "�Q", answers = new string[] { "����" }},
            new QuestionData { kanji = "��", answers = new string[] { "����" }},
            new QuestionData { kanji = "��", answers = new string[] { "����","��" }},
            new QuestionData { kanji = "��", answers = new string[] { "������" }},
            new QuestionData { kanji = "�{", answers = new string[] { "����","������" }},

            // �񎚏n��
            new QuestionData { kanji = "�g�t", answers = new string[] { "�����悤", "���݂�" }},
            new QuestionData { kanji = "����", answers = new string[] { "���傤���傤" }},
            new QuestionData { kanji = "�Z��", answers = new string[] { "�䂤����" }},
            new QuestionData { kanji = "����", answers = new string[] { "�������傤" }},
            new QuestionData { kanji = "�{��", answers = new string[] { "�悤��" }},
            new QuestionData { kanji = "����", answers = new string[] { "������" }},
            new QuestionData { kanji = "���A", answers = new string[] { "��������" }},
            new QuestionData { kanji = "����", answers = new string[] { "��������" }},
            new QuestionData { kanji = "���I", answers = new string[] { "���ꂫ" }},
            new QuestionData { kanji = "�ǖ�", answers = new string[] { "������" }},
            new QuestionData { kanji = "����", answers = new string[] { "���낵����" }},
            new QuestionData { kanji = "�U��", answers = new string[] { "�䂤����" }},
            new QuestionData { kanji = "�Q��", answers = new string[] { "����" }},
            new QuestionData { kanji = "����", answers = new string[] { "���[�Ё[" }},
            new QuestionData { kanji = "����", answers = new string[] { "���イ����" }},
            new QuestionData { kanji = "���s", answers = new string[] { "������" }},


            // �O�����n��
            new QuestionData { kanji = "�f�Օ�", answers = new string[] { "�ڂ������ӂ�" }},
            new QuestionData { kanji = "���R�E", answers = new string[] { "�����񂩂�" }},
            new QuestionData { kanji = "���Y�}", answers = new string[] { "���傤����Ƃ�" }},
            new QuestionData { kanji = "�j����", answers = new string[] { "�����Ԃ���" }},
            new QuestionData { kanji = "��b��", answers = new string[] { "������傭" }},
            new QuestionData { kanji = "�����", answers = new string[] { "�������イ��" }},
            new QuestionData { kanji = "�i���X", answers = new string[] { "�������Ă�" }},
            new QuestionData { kanji = "���b��", answers = new string[] { "���񂱂�����" }},
            new QuestionData { kanji = "���݉�", answers = new string[] { "�͂����Ⴍ��" }},

            // �l���n��
            new QuestionData { kanji = "�����~��", answers = new string[] { "�߂����傤������" }},
            new QuestionData { kanji = "��Γ�", answers = new string[] { "���������ɂ��傤" }},
            new QuestionData { kanji = "���S����", answers = new string[] { "�������񂹂���" }},
            new QuestionData { kanji = "���Ǝ���", answers = new string[] { "���������Ƃ�" }},
            new QuestionData { kanji = "���щΎR", answers = new string[] { "�ӂ���񂩂���" }},

            // ����
            new QuestionData { kanji = "�{��", answers = new string[] { "�₵�Ȃ�" }},
            new QuestionData { kanji = "�̂���", answers = new string[] { "��������" }},
            new QuestionData { kanji = "�Ղ�", answers = new string[] { "��������" }},
            new QuestionData { kanji = "����", answers = new string[] { "���Ȃ���" }},
            new QuestionData { kanji = "�p��", answers = new string[] { "��" }},
            new QuestionData { kanji = "����", answers = new string[] { "���܂���" }},
            new QuestionData { kanji = "�b��", answers = new string[] { "�߂���" }},
            new QuestionData { kanji = "�Y���", answers = new string[] { "����ނ��" }},
            new QuestionData { kanji = "�ӂ݂�", answers = new string[] { "���񂪂݂�" }},

            // �`�e��
            new QuestionData { kanji = "�R��", answers = new string[] { "�͂��Ȃ�" }},
            new QuestionData { kanji = "�Ƃ�", answers = new string[] { "���낢" }},
            new QuestionData { kanji = "�؂₩", answers = new string[] { "�͂Ȃ₩" }},
            new QuestionData { kanji = "���炩", answers = new string[] { "�Ȃ߂炩" }},
            new QuestionData { kanji = "���", answers = new string[] { "�����ς�" }},

            // ����ȓǂݕ��̒n��
            new QuestionData { kanji = "�Ύ�", answers = new string[] { "��������" }},
            new QuestionData { kanji = "������", answers = new string[] { "���񂶂�" }},
            new QuestionData { kanji = "�W�H��", answers = new string[] { "���킶����" }},
            new QuestionData { kanji = "������", answers = new string[] { "���Ƃ�����" }},
            new QuestionData { kanji = "����", answers = new string[] { "�˂ނ�" }},

            // �̐l�̖��O
            new QuestionData { kanji = "�D�c�M��", answers = new string[] { "�����̂ԂȂ�" }},
            new QuestionData { kanji = "��������", answers = new string[] { "����������������" }},
            new QuestionData { kanji = "����ƍN", answers = new string[] { "�Ƃ����킢���₷" }},
            new QuestionData { kanji = "�������q", answers = new string[] { "���傤�Ƃ�������" }},
            new QuestionData { kanji = "���c�M��", answers = new string[] { "���������񂰂�" }}
        };


        return datas[Random.Range(0, datas.Length)];
    }
}
