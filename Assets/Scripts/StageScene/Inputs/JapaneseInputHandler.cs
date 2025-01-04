using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UniRx;

public class JapaneseInputHandler
{
    const int MAX_ANSWER_LENGTH = 15;
    const int MAX_UNIT_CHARNUM = 4;
    private ReactiveProperty<string> result = new ReactiveProperty<string>("");

    public IReadOnlyReactiveProperty<string> AnswerReactiveProperty { get { return result; } }

    // ���[�}������Ђ炪�Ȃւ̑Ή��\
    private readonly Dictionary<string, string> romajiToHiragana = new Dictionary<string, string>(){

        // 4����
            { "ttsu", "����" }, { "xtsu","��" }, { "ltsu","��" },
            { "kkya", "������" }, { "kkyi", "������" }, { "kkyu", "������" }, { "kkye", "������" }, { "kkyo", "������" },
            { "ggya","������" }, { "ggyi","������" }, { "ggyu","������" }, { "ggye","������" }, { "ggyo","������" },
            { "ssha","������" }, { "sshi","����" }, { "sshu","������" },{ "sshe","������" },{ "ssho","������" },
            { "ssya","������" }, { "ssyi","������" }, { "ssyu","������" },{ "ssye","������" },{ "ssyo","������" },
            { "zzya","������" }, { "zzyi","������" },{ "zzyu","������" },{ "zzye","������" },{ "zzyo","������" },
            { "ttya","������" }, { "ttyi","������" }, { "ttyu","������" }, { "ttye","������" }, { "ttyo","������" },
            { "ccha","������" },{ "cchi","����" },{ "cchu","������" },{ "cche","������" },{ "ccho","������" },
            { "ddya", "������" }, { "ddyi", "������" }, { "ddyu", "������" }, { "ddye", "������" }, { "ddyo", "������" },
            { "hhya","���Ђ�" },{ "hhyi","���Ђ�" },{ "hhyu","���Ђ�" },{ "hhye","���Ђ�" },{ "hhyo","���Ђ�" },
            { "bbya","���т�" } ,{ "bbyi","���т�" } ,{ "bbyu","���т�" } ,{ "bbye","���т�" } ,{ "bbyo","���т�" } ,
            { "ppya","���҂�" },{ "ppyi","���҂�" },{ "ppyu","���҂�" },{ "ppye","���҂�" },{ "ppyo","���҂�" },
            { "mmya", "���݂�" },{ "mmyi", "���݂�" },{ "mmyu", "���݂�" },{ "mmye", "���݂�" },{ "mmyo", "���݂�" },
            { "rrya", "�����" },{ "rryi", "���股" },{ "rryu", "�����" },{ "rrye", "���肥" },{ "rryo", "�����" },
            


        // 3����
            { "kki", "����" }, { "kka", "����" }, { "kku", "����" }, { "kke", "����" }, { "kko", "����" },
            { "shi", "��" },
            { "ssi", "����" }, { "ssa", "����" }, { "ssu", "����" }, { "sse", "����" }, { "sso", "����" },
            { "chi", "��" }, { "tsu", "��" },
            { "xtu", "��" }, { "ltu", "��" },
            { "tta", "����" }, { "tti", "����" }, { "ttu", "����" }, { "tte", "����" }, { "tto", "����" },
            { "hha", "����" }, { "hhi", "����" }, { "hhu", "����" }, { "hhe", "����" }, { "hho", "����" },
            { "mma", "����" }, { "mmi", "����" }, { "mmu", "����" }, { "mme", "����" }, { "mmo", "����" },
            { "yya", "����" }, { "yyu", "����" }, { "yyo", "����" },
            { "rra", "����" }, { "rri", "����" }, { "rru", "����" }, { "rre", "����" }, { "rro", "����" },
            { "jja","������" },{ "jji","����" },{ "jju","������" },{ "jje","������" },{ "jjo","������" },
            { "kya", "����" }, { "kyu", "����" }, { "kyo", "����" },
            { "gya", "����" }, { "gyu", "����" }, { "gyo", "����" },
            { "sha", "����" }, { "shu", "����" }, { "sho", "����" },
            { "sya", "����" }, { "syu", "����" }, { "syo", "����" },
            { "zya", "����" }, { "zyu", "����" }, { "zyo", "����" },
            { "jya", "����" }, { "jyu", "����" }, { "jyo", "����" },
            { "cha", "����" }, { "chu", "����" }, { "cho", "����" },
            { "tya", "����" }, { "tyu", "����" }, { "tyo", "����" },
            { "dya", "����" }, { "dyu", "����" }, { "dyo", "����" },
            { "nya", "�ɂ�" }, { "nyu", "�ɂ�" }, { "nyo", "�ɂ�" },
            { "hya", "�Ђ�" }, { "hyu", "�Ђ�" }, { "hyo", "�Ђ�" },
            { "bya", "�т�" }, { "byu", "�т�" }, { "byo", "�т�" },
            { "mya", "�݂�" }, { "myu", "�݂�" }, { "myo", "�݂�" },
            { "rya", "���" }, { "ryu", "���" }, { "ryo", "���" },
            { "gga", "����" }, { "ggi", "����" }, { "ggu", "����" }, { "gge", "����" }, { "ggo", "����" },
            { "zza", "����" }, { "zzi", "����" }, { "zzu", "����" }, { "zze", "����" }, { "zzo", "����" },
            { "dda", "����" }, { "ddi", "����" }, { "ddu", "����" }, { "dde", "����" }, { "ddo", "����" },
            { "bba", "����" }, { "bbi", "����" }, { "bbu", "����" }, { "bbe", "����" }, { "bbo", "����" },
            { "ppa", "����" }, { "ppi", "����" }, { "ppu", "����" }, { "ppe", "����" }, { "ppo", "����" },
            { "xya", "��" }, { "xyi", "��" }, { "xyu", "��" }, { "xye", "��" }, { "xyo", "��" },
            { "lya", "��" }, { "lyi", "��" }, { "lyu", "��" }, { "lye", "��" }, { "lyo", "��" },
            { "ffa","���ӂ�" }, { "ffi","���ӂ�" }, { "ffu","����" }, { "ffe","���ӂ�" }, { "ffo","���ӂ�" },



        // 2����
            { "xa", "��" }, { "xi", "��" }, { "xu", "��" }, { "xe", "��" }, { "xo", "��" },
            { "la", "��" }, { "li", "��" }, { "lu", "��" }, { "le", "��" }, { "lo", "��" },
            { "ka", "��" }, { "ki", "��" }, { "ku", "��" }, { "ke", "��" }, { "ko", "��" },
            { "sa", "��" }, { "si", "��" }, { "su", "��" }, { "se", "��" }, { "so", "��" },
            { "ta", "��" }, { "ti", "��" }, { "tu", "��" }, { "te", "��" }, { "to", "��" },
            { "na", "��" }, { "ni", "��" }, { "nu", "��" }, { "ne", "��" }, { "no", "��" },
            { "ha", "��" }, { "hi", "��" }, { "hu", "��" }, { "he", "��" }, { "ho", "��" },
            { "ma", "��" }, { "mi", "��" }, { "mu", "��" }, { "me", "��" }, { "mo", "��" },
            { "ya", "��" }, { "yu", "��" }, { "yo", "��" },
            { "ra", "��" }, { "ri", "��" }, { "ru", "��" }, { "re", "��" }, { "ro", "��" },
            { "wa", "��" }, { "wi","����" }, { "wu","��" }, { "we","����" }, { "wo", "��" }, { "nn", "��" },
            { "ga", "��" }, { "gi", "��" }, { "gu", "��" }, { "ge", "��" }, { "go", "��" },
            { "za", "��" }, { "zi", "��" }, { "zu", "��" }, { "ze", "��" }, { "zo", "��" },
            { "da", "��" }, { "di", "��" }, { "du", "��" }, { "de", "��" }, { "do", "��" },
            { "ba", "��" }, { "bi", "��" }, { "bu", "��" }, { "be", "��" }, { "bo", "��" },
            { "pa", "��" }, { "pi", "��" }, { "pu", "��" }, { "pe", "��" }, { "po", "��" },
            { "ja", "����" }, { "ji", "��" }, { "ju", "����" }, { "je", "����" }, { "jo", "����" },
            { "fa","�ӂ�" },{ "fi","�ӂ�" },{ "fu","��" },{ "fe","�ӂ�" },{ "fo","�ӂ�" },
            { "qa", "����" }, { "qi", "����" }, { "qu", "��" }, { "qe", "����" }, { "qo", "����" },
            { "nk","��k" }, { "ns","��s" }, { "nt","��t" }, { "nh","��h" }, { "nm","��m" }, { "nr","��r" }, { "nw","��w" },
            { "ng","��g" }, { "nz","��z" }, { "nj","��j" }, { "nd","��d" }, { "nb","��b" }, { "np","��p" }, { "nq","��q" },
            { "nf", "��f" }, { "nl", "��l" }, { "nx", "��x" }, { "nc", "��c" },{ "nv", "��v" },

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
        string original = result.Value;
        result.Value += key.ToString().ToLower();

        // �ϊ��ł��郍�[�}�������邩�m�F(���t�@�N�^�����O���K�v�H)
        foreach (var entry in romajiToHiragana)
        {
            if (result.Value.EndsWith(entry.Key))
            {
                // �ϊ��O�̃A���t�@�x�b�g�������폜
                result.Value = result.Value.Substring(0, result.Value.Length - entry.Key.Length);
                // �ϊ���̂Ђ炪�Ȃɒu��������
                result.Value += entry.Value;
                break;
            }
        }

        //�ő啶���������Ō��̕����ɖ߂�
        if (result.Value.Length >= MAX_ANSWER_LENGTH) { result.Value = original; }
    }

    /// <summary>
    /// �}�C�i�X���̋L�����܂ފ֐�
    /// </summary>
    /// <param name="keycode"></param>
    public bool OnKeyInput(KeyCode keyCode)
    {
        //�L��
        switch (keyCode)
        {
            case KeyCode.Minus:
                OnKeyInput('-');
                return true;
        }

        //���̑�
        if (keyCode >= KeyCode.A && keyCode <= KeyCode.Z) 
        {
            OnKeyInput(keyCode.ToString()[0]);
            return true;
        }

        return false;
    }

    // �m�肵���ǂ݂�Ԃ�
    public string GetResult()
    {
        return result.Value;
    }

    // ���Z�b�g�p
    public void Clear()
    {
        result.Value = "";
    }

    /// <summary>
    /// �ꕶ������
    /// </summary>
    public bool BackSpace()
    {
        if(result.Value.Length <= 0) { return false; }
        result.Value = result.Value.Substring(0, result.Value.Length - 1);

        return true;
    }
}
