using System.Collections.Generic;
using UnityEngine;

public class JapaneseInputHandler
{
    const int MAX_ANSWER_LENGTH = 15;
    private string result = "";

    // ���[�}������Ђ炪�Ȃւ̑Ή��\
    private readonly Dictionary<string, string> romajiToHiragana = new Dictionary<string, string>(){

        // 4����
            { "ttsu", "����" }, { "xtsu","��" }, { "ltsu","��" },


        // 3����
            { "kki", "����" }, { "kka", "����" }, { "kku", "����" }, { "kke", "����" }, { "kko", "����" },
            { "shi", "��" },
            { "ssi", "����" }, { "ssa", "����" }, { "ssu", "����" }, { "sse", "����" }, { "sso", "����" },
            { "chi", "��" }, { "tsu", "��" },
            { "xtu", "��" }, { "ltu", "��" },
            { "tta", "����" }, { "tti", "����" }, { "ttu", "����" }, { "tte", "����" }, { "tto", "����" },
            { "hha", "����" }, { "hhi", "����" }, { "ffu", "����" }, { "hhe", "����" }, { "hho", "����" },
            { "mma", "����" }, { "mmi", "����" }, { "mmu", "����" }, { "mme", "����" }, { "mmo", "����" },
            { "yya", "����" }, { "yyu", "����" }, { "yyo", "����" },
            { "rra", "����" }, { "rri", "����" }, { "rru", "����" }, { "rre", "����" }, { "rro", "����" },
            { "kya", "����" }, { "kyu", "����" }, { "kyo", "����" },
            { "gya", "����" }, { "gyu", "����" }, { "gyo", "����" },
            { "sha", "����" }, { "shu", "����" }, { "sho", "����" },
            { "sya", "����" }, { "syu", "����" }, { "syo", "����" },
            { "zya", "����" }, { "zyu", "����" }, { "zyo", "����" },
            { "cha", "����" }, { "chu", "����" }, { "cho", "����" },
            { "tya", "����" }, { "tyu", "����" }, { "tyo", "����" },
            { "dya", "����" }, { "dyu", "����" }, { "dyo", "����" },
            { "nya", "�ɂ�" }, { "nyu", "�ɂ�" }, { "nyo", "�ɂ�" },
            { "hya", "�Ђ�" }, { "hyu", "�Ђ�" }, { "hyo", "�Ђ�" },
            { "bya", "�т�" }, { "byu", "�т�" }, { "byo", "�т�" },
            { "mya", "�݂�" }, { "myu", "�݂�" }, { "myo", "�݂�" },
            { "rya", "���" }, { "ryu", "���" }, { "ryo", "���" },
            { "gga", "����" }, { "ggi", "����" }, { "ggu", "����" }, { "gge", "����" }, { "ggo", "����" },
            { "zza", "����" }, { "jji", "����" }, { "zzu", "����" }, { "zze", "����" }, { "zzo", "����" },
            { "dda", "����" }, { "ddi", "����" }, { "ddu", "����" }, { "dde", "����" }, { "ddo", "����" },
            { "bba", "����" }, { "bbi", "����" }, { "bbu", "����" }, { "bbe", "����" }, { "bbo", "����" },
            { "ppa", "����" }, { "ppi", "����" }, { "ppu", "����" }, { "ppe", "����" }, { "ppo", "����" },
            { "xya", "��" }, { "xyi", "��" }, { "xyu", "��" }, { "xye", "��" }, { "xyo", "��" },
            { "lya", "��" }, { "lyi", "��" }, { "lyu", "��" }, { "lye", "��" }, { "lyo", "��" },



        // 2����
            { "xa", "��" }, { "xi", "��" }, { "xu", "��" }, { "xe", "��" }, { "xo", "��" },
            { "la", "��" }, { "li", "��" }, { "lu", "��" }, { "le", "��" }, { "lo", "��" },
            { "ka", "��" }, { "ki", "��" }, { "ku", "��" }, { "ke", "��" }, { "ko", "��" },
            { "sa", "��" }, { "si", "��" }, { "su", "��" }, { "se", "��" }, { "so", "��" },
            { "ta", "��" }, { "ti", "��" }, { "tu", "��" }, { "te", "��" }, { "to", "��" },
            { "na", "��" }, { "ni", "��" }, { "nu", "��" }, { "ne", "��" }, { "no", "��" },
            { "ha", "��" }, { "hi", "��" }, { "hu", "��" }, { "fu", "��" }, { "he", "��" }, { "ho", "��" },
            { "ma", "��" }, { "mi", "��" }, { "mu", "��" }, { "me", "��" }, { "mo", "��" },
            { "ya", "��" }, { "yu", "��" }, { "yo", "��" },
            { "ra", "��" }, { "ri", "��" }, { "ru", "��" }, { "re", "��" }, { "ro", "��" },
            { "wa", "��" }, { "wo", "��" }, { "nn", "��" },
            { "ga", "��" }, { "gi", "��" }, { "gu", "��" }, { "ge", "��" }, { "go", "��" },
            { "za", "��" }, { "ji", "��" }, { "zi", "��" }, { "zu", "��" }, { "ze", "��" }, { "zo", "��" },
            { "da", "��" }, { "di", "��" }, { "du", "��" }, { "de", "��" }, { "do", "��" },
            { "ba", "��" }, { "bi", "��" }, { "bu", "��" }, { "be", "��" }, { "bo", "��" },
            { "pa", "��" }, { "pi", "��" }, { "pu", "��" }, { "pe", "��" }, { "po", "��" },


        // 1����
            { "a", "��" }, { "i", "��" }, { "u", "��" }, { "e", "��" }, { "o", "��" },
            { "-" ,"�[" },


    };

    /// <summary>
    /// �W���I�ȃA���t�@�x�b�g���͂��ꂽ�Ƃ�
    /// </summary>
    /// <param name="key"></param>
    private void OnKeyInput(char key)
    {
        string original = result;
        result += key.ToString().ToLower();

        // �ϊ��ł��郍�[�}�������邩�m�F
        foreach (var entry in romajiToHiragana)
        {
            if (result.EndsWith(entry.Key))
            {
                //�ϊ��O�̃A���t�@�x�b�g�������폜
                result = result.Substring(0, result.Length - entry.Key.Length);
                //�ϊ���̂Ђ炪�Ȃɒu��������
                result += entry.Value;
                break;
            }
        }

        //�ő啶���������Ō��̕����ɖ߂�
        if (result.Length >= MAX_ANSWER_LENGTH) { result = original; }
    }

    /// <summary>
    /// �}�C�i�X���̋L�����܂ފ֐�
    /// </summary>
    /// <param name="keycode"></param>
    public void OnKeyInput(KeyCode keyCode)
    {
        //�L��
        switch (keyCode)
        {
            case KeyCode.Minus:
                OnKeyInput('-');
                return;
        }

        //���̑�
        if (keyCode >= KeyCode.A && keyCode <= KeyCode.Z) { OnKeyInput(keyCode.ToString()[0]); }
    }

    // �m�肵���ǂ݂�Ԃ�
    public string GetResult()
    {
        return result;
    }

    // ���Z�b�g�p
    public void Clear()
    {
        result = "";
    }

    /// <summary>
    /// �ꕶ������
    /// </summary>
    public void BackSpace()
    {
        if(result.Length <= 0) { return; }
        result = result.Substring(0, result.Length - 1);
    }
}
