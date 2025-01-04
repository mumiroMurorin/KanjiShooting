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

    // ローマ字からひらがなへの対応表
    private readonly Dictionary<string, string> romajiToHiragana = new Dictionary<string, string>(){

        // 4文字
            { "ttsu", "っつ" }, { "xtsu","っ" }, { "ltsu","っ" },
            { "kkya", "っきゃ" }, { "kkyi", "っきぃ" }, { "kkyu", "っきゅ" }, { "kkye", "っきぇ" }, { "kkyo", "っきょ" },
            { "ggya","っぎゃ" }, { "ggyi","っぎぃ" }, { "ggyu","っぎゅ" }, { "ggye","っぎぇ" }, { "ggyo","っぎょ" },
            { "ssha","っしゃ" }, { "sshi","っし" }, { "sshu","っしゅ" },{ "sshe","っしぇ" },{ "ssho","っしょ" },
            { "ssya","っしゃ" }, { "ssyi","っしぃ" }, { "ssyu","っしゅ" },{ "ssye","っしぇ" },{ "ssyo","っしょ" },
            { "zzya","っじゃ" }, { "zzyi","っじぃ" },{ "zzyu","っじゅ" },{ "zzye","っじぇ" },{ "zzyo","っじょ" },
            { "ttya","っちゃ" }, { "ttyi","っちぃ" }, { "ttyu","っちゅ" }, { "ttye","っちぇ" }, { "ttyo","っちょ" },
            { "ccha","っちゃ" },{ "cchi","っち" },{ "cchu","っちゅ" },{ "cche","っちぇ" },{ "ccho","っちょ" },
            { "ddya", "っぢゃ" }, { "ddyi", "っぢぃ" }, { "ddyu", "っぢゅ" }, { "ddye", "っぢぇ" }, { "ddyo", "っぢょ" },
            { "hhya","っひゃ" },{ "hhyi","っひぃ" },{ "hhyu","っひゅ" },{ "hhye","っひぇ" },{ "hhyo","っひょ" },
            { "bbya","っびゃ" } ,{ "bbyi","っびぃ" } ,{ "bbyu","っびゅ" } ,{ "bbye","っびぇ" } ,{ "bbyo","っびょ" } ,
            { "ppya","っぴゃ" },{ "ppyi","っぴぃ" },{ "ppyu","っぴゅ" },{ "ppye","っぴぇ" },{ "ppyo","っぴょ" },
            { "mmya", "っみゃ" },{ "mmyi", "っみぃ" },{ "mmyu", "っみゅ" },{ "mmye", "っみぇ" },{ "mmyo", "っみょ" },
            { "rrya", "っりゃ" },{ "rryi", "っりぃ" },{ "rryu", "っりゅ" },{ "rrye", "っりぇ" },{ "rryo", "っりょ" },
            


        // 3文字
            { "kki", "っき" }, { "kka", "っか" }, { "kku", "っく" }, { "kke", "っけ" }, { "kko", "っこ" },
            { "shi", "し" },
            { "ssi", "っし" }, { "ssa", "っさ" }, { "ssu", "っす" }, { "sse", "っせ" }, { "sso", "っそ" },
            { "chi", "ち" }, { "tsu", "つ" },
            { "xtu", "っ" }, { "ltu", "っ" },
            { "tta", "った" }, { "tti", "っち" }, { "ttu", "っつ" }, { "tte", "って" }, { "tto", "っと" },
            { "hha", "っは" }, { "hhi", "っひ" }, { "hhu", "っふ" }, { "hhe", "っへ" }, { "hho", "っほ" },
            { "mma", "っま" }, { "mmi", "っみ" }, { "mmu", "っむ" }, { "mme", "っめ" }, { "mmo", "っも" },
            { "yya", "っや" }, { "yyu", "っゆ" }, { "yyo", "っよ" },
            { "rra", "っら" }, { "rri", "っり" }, { "rru", "っる" }, { "rre", "っれ" }, { "rro", "っろ" },
            { "jja","っじゃ" },{ "jji","っじ" },{ "jju","っじゅ" },{ "jje","っじぇ" },{ "jjo","っじょ" },
            { "kya", "きゃ" }, { "kyu", "きゅ" }, { "kyo", "きょ" },
            { "gya", "ぎゃ" }, { "gyu", "ぎゅ" }, { "gyo", "ぎょ" },
            { "sha", "しゃ" }, { "shu", "しゅ" }, { "sho", "しょ" },
            { "sya", "しゃ" }, { "syu", "しゅ" }, { "syo", "しょ" },
            { "zya", "じゃ" }, { "zyu", "じゅ" }, { "zyo", "じょ" },
            { "jya", "じゃ" }, { "jyu", "じゅ" }, { "jyo", "じょ" },
            { "cha", "ちゃ" }, { "chu", "ちゅ" }, { "cho", "ちょ" },
            { "tya", "ちゃ" }, { "tyu", "ちゅ" }, { "tyo", "ちょ" },
            { "dya", "ぢゃ" }, { "dyu", "ぢゅ" }, { "dyo", "ぢょ" },
            { "nya", "にゃ" }, { "nyu", "にゅ" }, { "nyo", "にょ" },
            { "hya", "ひゃ" }, { "hyu", "ひゅ" }, { "hyo", "ひょ" },
            { "bya", "びゃ" }, { "byu", "びゅ" }, { "byo", "びょ" },
            { "mya", "みゃ" }, { "myu", "みゅ" }, { "myo", "みょ" },
            { "rya", "りゃ" }, { "ryu", "りゅ" }, { "ryo", "りょ" },
            { "gga", "っが" }, { "ggi", "っぎ" }, { "ggu", "っぐ" }, { "gge", "っげ" }, { "ggo", "っご" },
            { "zza", "っざ" }, { "zzi", "っじ" }, { "zzu", "っず" }, { "zze", "っぜ" }, { "zzo", "っぞ" },
            { "dda", "っだ" }, { "ddi", "っぢ" }, { "ddu", "っづ" }, { "dde", "っで" }, { "ddo", "っど" },
            { "bba", "っば" }, { "bbi", "っび" }, { "bbu", "っぶ" }, { "bbe", "っべ" }, { "bbo", "っぼ" },
            { "ppa", "っぱ" }, { "ppi", "っぴ" }, { "ppu", "っぷ" }, { "ppe", "っぺ" }, { "ppo", "っぽ" },
            { "xya", "ゃ" }, { "xyi", "ぃ" }, { "xyu", "ゅ" }, { "xye", "ぇ" }, { "xyo", "ょ" },
            { "lya", "ゃ" }, { "lyi", "ぃ" }, { "lyu", "ゅ" }, { "lye", "ぇ" }, { "lyo", "ょ" },
            { "ffa","っふぁ" }, { "ffi","っふぃ" }, { "ffu","っふ" }, { "ffe","っふぇ" }, { "ffo","っふぉ" },



        // 2文字
            { "xa", "ぁ" }, { "xi", "ぃ" }, { "xu", "ぅ" }, { "xe", "ぇ" }, { "xo", "ぉ" },
            { "la", "ぁ" }, { "li", "ぃ" }, { "lu", "ぅ" }, { "le", "ぇ" }, { "lo", "ぉ" },
            { "ka", "か" }, { "ki", "き" }, { "ku", "く" }, { "ke", "け" }, { "ko", "こ" },
            { "sa", "さ" }, { "si", "し" }, { "su", "す" }, { "se", "せ" }, { "so", "そ" },
            { "ta", "た" }, { "ti", "ち" }, { "tu", "つ" }, { "te", "て" }, { "to", "と" },
            { "na", "な" }, { "ni", "に" }, { "nu", "ぬ" }, { "ne", "ね" }, { "no", "の" },
            { "ha", "は" }, { "hi", "ひ" }, { "hu", "ふ" }, { "he", "へ" }, { "ho", "ほ" },
            { "ma", "ま" }, { "mi", "み" }, { "mu", "む" }, { "me", "め" }, { "mo", "も" },
            { "ya", "や" }, { "yu", "ゆ" }, { "yo", "よ" },
            { "ra", "ら" }, { "ri", "り" }, { "ru", "る" }, { "re", "れ" }, { "ro", "ろ" },
            { "wa", "わ" }, { "wi","うぃ" }, { "wu","う" }, { "we","うぇ" }, { "wo", "を" }, { "nn", "ん" },
            { "ga", "が" }, { "gi", "ぎ" }, { "gu", "ぐ" }, { "ge", "げ" }, { "go", "ご" },
            { "za", "ざ" }, { "zi", "じ" }, { "zu", "ず" }, { "ze", "ぜ" }, { "zo", "ぞ" },
            { "da", "だ" }, { "di", "ぢ" }, { "du", "づ" }, { "de", "で" }, { "do", "ど" },
            { "ba", "ば" }, { "bi", "び" }, { "bu", "ぶ" }, { "be", "べ" }, { "bo", "ぼ" },
            { "pa", "ぱ" }, { "pi", "ぴ" }, { "pu", "ぷ" }, { "pe", "ぺ" }, { "po", "ぽ" },
            { "ja", "じゃ" }, { "ji", "じ" }, { "ju", "じゅ" }, { "je", "じぇ" }, { "jo", "じょ" },
            { "fa","ふぁ" },{ "fi","ふぃ" },{ "fu","ふ" },{ "fe","ふぇ" },{ "fo","ふぉ" },
            { "qa", "くぁ" }, { "qi", "くぃ" }, { "qu", "く" }, { "qe", "くぇ" }, { "qo", "くぉ" },
            { "nk","んk" }, { "ns","んs" }, { "nt","んt" }, { "nh","んh" }, { "nm","んm" }, { "nr","んr" }, { "nw","んw" },
            { "ng","んg" }, { "nz","んz" }, { "nj","んj" }, { "nd","んd" }, { "nb","んb" }, { "np","んp" }, { "nq","んq" },
            { "nf", "んf" }, { "nl", "んl" }, { "nx", "んx" }, { "nc", "んc" },{ "nv", "んv" },

        // 1文字
            { "a", "あ" }, { "i", "い" }, { "u", "う" }, { "e", "え" }, { "o", "お" },
            { "-" ,"ー" },


    };

    /// <summary>
    /// 標準的なアルファベット入力されたとき
    /// </summary>
    /// <param name="key"></param>
    private void OnKeyInput(char key)
    {
        string original = result.Value;
        result.Value += key.ToString().ToLower();

        // 変換できるローマ字があるか確認(リファクタリングが必要？)
        foreach (var entry in romajiToHiragana)
        {
            if (result.Value.EndsWith(entry.Key))
            {
                // 変換前のアルファベット文字を削除
                result.Value = result.Value.Substring(0, result.Value.Length - entry.Key.Length);
                // 変換後のひらがなに置き換える
                result.Value += entry.Value;
                break;
            }
        }

        //最大文字数超えで元の文字に戻す
        if (result.Value.Length >= MAX_ANSWER_LENGTH) { result.Value = original; }
    }

    /// <summary>
    /// マイナス等の記号も含む関数
    /// </summary>
    /// <param name="keycode"></param>
    public bool OnKeyInput(KeyCode keyCode)
    {
        //記号
        switch (keyCode)
        {
            case KeyCode.Minus:
                OnKeyInput('-');
                return true;
        }

        //その他
        if (keyCode >= KeyCode.A && keyCode <= KeyCode.Z) 
        {
            OnKeyInput(keyCode.ToString()[0]);
            return true;
        }

        return false;
    }

    // 確定した読みを返す
    public string GetResult()
    {
        return result.Value;
    }

    // リセット用
    public void Clear()
    {
        result.Value = "";
    }

    /// <summary>
    /// 一文字消す
    /// </summary>
    public bool BackSpace()
    {
        if(result.Value.Length <= 0) { return false; }
        result.Value = result.Value.Substring(0, result.Value.Length - 1);

        return true;
    }
}
