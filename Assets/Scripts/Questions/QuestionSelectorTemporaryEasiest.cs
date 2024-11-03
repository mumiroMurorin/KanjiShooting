using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class QuestionSelectorTemporaryEasiest : MonoBehaviour, IQuestionSelector
{
    public QuestionData GetQuestionData(QuestionFilter filter) 
    {
        QuestionData[] datas = new QuestionData[]
        {
            new QuestionData { kanji = "��", answers = new string[] { "����" }},
            new QuestionData { kanji = "�G�z", answers = new string[] { "���񂦂�" }},
            new QuestionData { kanji = "���", answers = new string[] { "�Ђ߂�" }},
            new QuestionData { kanji = "���P", answers = new string[] { "�Ƃ����イ" }},
            new QuestionData { kanji = "�a��", answers = new string[] { "���Ƃ�" }},
            new QuestionData { kanji = "�ӂ݂�", answers = new string[] { "���񂪂݂�" }},
            new QuestionData { kanji = "�K�X", answers = new string[] { "�Ă���" }},
            new QuestionData { kanji = "�⊶", answers = new string[] { "������" }},
            new QuestionData { kanji = "�^��", answers = new string[] { "����" }},
            new QuestionData { kanji = "���O", answers = new string[] { "���˂�" }},
            new QuestionData { kanji = "�i��", answers = new string[] { "���񂿂傭" }},
            new QuestionData { kanji = "����", answers = new string[] { "������" }},
            new QuestionData { kanji = "�]��", answers = new string[] { "�ӂق�" }},
            new QuestionData { kanji = "���", answers = new string[] { "��������" }},
            new QuestionData { kanji = "�{�H", answers = new string[] { "������" }},
            new QuestionData { kanji = "�N�W", answers = new string[] { "���イ���イ" }},
            new QuestionData { kanji = "�M��", answers = new string[] { "������" }},
            new QuestionData { kanji = "����", answers = new string[] { "����񂵂�" }},
            new QuestionData { kanji = "����", answers = new string[] { "����" }},
            new QuestionData { kanji = "����", answers = new string[] { "��������" }},
            new QuestionData { kanji = "���r", answers = new string[] { "����" }},
            new QuestionData { kanji = "����", answers = new string[] { "�ӂ���" }},
            new QuestionData { kanji = "�ّ�", answers = new string[] { "��������" }},
            new QuestionData { kanji = "�ώG", answers = new string[] { "�͂񂴂�" }},
            new QuestionData { kanji = "�Q", answers = new string[] { "�킾��","�Ă�" }},
            new QuestionData { kanji = "����", answers = new string[] { "���" }},
            new QuestionData { kanji = "�[��", answers = new string[] { "��������" }},
            new QuestionData { kanji = "����", answers = new string[] { "��������" }},
            new QuestionData { kanji = "�Ő�", answers = new string[] { "�Ђ���","���킹��" }},
            new QuestionData { kanji = "�ԚL", answers = new string[] { "�ǂ�����" }},
            new QuestionData { kanji = "�k��", answers = new string[] { "���ׂ�" }},
            new QuestionData { kanji = "�\��", answers = new string[] { "���܂�" }},
            new QuestionData { kanji = "�F��", answers = new string[] { "���傤����" }},
            new QuestionData { kanji = "��s", answers = new string[] { "�ꂢ����" }},
            new QuestionData { kanji = "���\�L", answers = new string[] { "�݂���" }},
            new QuestionData { kanji = "����", answers = new string[] { "��������" }},
            new QuestionData { kanji = "�}��", answers = new string[] { "��������" }},
            new QuestionData { kanji = "��", answers = new string[] { "�͂�" }},
            new QuestionData { kanji = "�a�", answers = new string[] { "���ꂫ" }},
            new QuestionData { kanji = "�Λ�", answers = new string[] { "������" }},
            new QuestionData { kanji = "�I��", answers = new string[] { "���イ����" }},
            new QuestionData { kanji = "臒l", answers = new string[] { "������","��������" }},
            new QuestionData { kanji = "�b�q", answers = new string[] { "������" }},
            new QuestionData { kanji = "��T", answers = new string[] { "���тイ" }},
            new QuestionData { kanji = "�@", answers = new string[] { "��������" }},
            new QuestionData { kanji = "�f�I", answers = new string[] { "�Ƃ�" }},
            new QuestionData { kanji = "����", answers = new string[] { "���傤��" }},
            new QuestionData { kanji = "����", answers = new string[] { "���傤����" }},
            new QuestionData { kanji = "�N�O", answers = new string[] { "�����낤" }},
            new QuestionData { kanji = "�ܜ�", answers = new string[] { "���傤����" }},
            new QuestionData { kanji = "�ꏟ", answers = new string[] { "���サ�傤" }},
            new QuestionData { kanji = "�܍�", answers = new string[] { "���傭����" }},
            new QuestionData { kanji = "�a��", answers = new string[] { "��������" }},
            new QuestionData { kanji = "畏�", answers = new string[] { "����񂶂��" }},
            new QuestionData { kanji = "����", answers = new string[] { "������" }},
            new QuestionData { kanji = "���", answers = new string[] { "�͂񂷂�" }},
        };


        return datas[Random.Range(0, datas.Length)];
    }
}
