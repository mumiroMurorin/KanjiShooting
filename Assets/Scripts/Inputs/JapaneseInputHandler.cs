using System.Collections.Generic;
using UnityEngine;

public class JapaneseInputHandler
{
    const int MAX_ANSWER_LENGTH = 15;
    private string result = "";

    // ローマ字からひらがなへの対応表
    private readonly Dictionary<string, string> romajiToHiragana = new Dictionary<string, string>(){

        // 4文字
            { "ttsu", "っつ" }, { "xtsu","っ" }, { "ltsu","っ" },


        // 3文字
            { "kki", "っき" }, { "kka", "っか" }, { "kku", "っく" }, { "kke", "っけ" }, { "kko", "っこ" },
            { "shi", "し" },
            { "ssi", "っし" }, { "ssa", "っさ" }, { "ssu", "っす" }, { "sse", "っせ" }, { "sso", "っそ" },
            { "chi", "ち" }, { "tsu", "つ" },
            { "xtu", "っ" }, { "ltu", "っ" },
            { "tta", "った" }, { "tti", "っち" }, { "ttu", "っつ" }, { "tte", "って" }, { "tto", "っと" },
            { "hha", "っは" }, { "hhi", "っひ" }, { "ffu", "っふ" }, { "hhe", "っへ" }, { "hho", "っほ" },
            { "mma", "っま" }, { "mmi", "っみ" }, { "mmu", "っむ" }, { "mme", "っめ" }, { "mmo", "っも" },
            { "yya", "っや" }, { "yyu", "っゆ" }, { "yyo", "っよ" },
            { "rra", "っら" }, { "rri", "っり" }, { "rru", "っる" }, { "rre", "っれ" }, { "rro", "っろ" },
            { "kya", "きゃ" }, { "kyu", "きゅ" }, { "kyo", "きょ" },
            { "gya", "ぎゃ" }, { "gyu", "ぎゅ" }, { "gyo", "ぎょ" },
            { "sha", "しゃ" }, { "shu", "しゅ" }, { "sho", "しょ" },
            { "sya", "しゃ" }, { "syu", "しゅ" }, { "syo", "しょ" },
            { "zya", "じゃ" }, { "zyu", "じゅ" }, { "zyo", "じょ" },
            { "cha", "ちゃ" }, { "chu", "ちゅ" }, { "cho", "ちょ" },
            { "tya", "ちゃ" }, { "tyu", "ちゅ" }, { "tyo", "ちょ" },
            { "dya", "ぢゃ" }, { "dyu", "ぢゅ" }, { "dyo", "ぢょ" },
            { "nya", "にゃ" }, { "nyu", "にゅ" }, { "nyo", "にょ" },
            { "hya", "ひゃ" }, { "hyu", "ひゅ" }, { "hyo", "ひょ" },
            { "bya", "びゃ" }, { "byu", "びゅ" }, { "byo", "びょ" },
            { "mya", "みゃ" }, { "myu", "みゅ" }, { "myo", "みょ" },
            { "rya", "りゃ" }, { "ryu", "りゅ" }, { "ryo", "りょ" },
            { "gga", "っが" }, { "ggi", "っぎ" }, { "ggu", "っぐ" }, { "gge", "っげ" }, { "ggo", "っご" },
            { "zza", "っざ" }, { "jji", "っじ" }, { "zzu", "っず" }, { "zze", "っぜ" }, { "zzo", "っぞ" },
            { "dda", "っだ" }, { "ddi", "っぢ" }, { "ddu", "っづ" }, { "dde", "っで" }, { "ddo", "っど" },
            { "bba", "っば" }, { "bbi", "っび" }, { "bbu", "っぶ" }, { "bbe", "っべ" }, { "bbo", "っぼ" },
            { "ppa", "っぱ" }, { "ppi", "っぴ" }, { "ppu", "っぷ" }, { "ppe", "っぺ" }, { "ppo", "っぽ" },
            { "xya", "ゃ" }, { "xyi", "ぃ" }, { "xyu", "ゅ" }, { "xye", "ぇ" }, { "xyo", "ょ" },
            { "lya", "ゃ" }, { "lyi", "ぃ" }, { "lyu", "ゅ" }, { "lye", "ぇ" }, { "lyo", "ょ" },



        // 2文字
            { "xa", "ぁ" }, { "xi", "ぃ" }, { "xu", "ぅ" }, { "xe", "ぇ" }, { "xo", "ぉ" },
            { "la", "ぁ" }, { "li", "ぃ" }, { "lu", "ぅ" }, { "le", "ぇ" }, { "lo", "ぉ" },
            { "ka", "か" }, { "ki", "き" }, { "ku", "く" }, { "ke", "け" }, { "ko", "こ" },
            { "sa", "さ" }, { "si", "し" }, { "su", "す" }, { "se", "せ" }, { "so", "そ" },
            { "ta", "た" }, { "ti", "ち" }, { "tu", "つ" }, { "te", "て" }, { "to", "と" },
            { "na", "な" }, { "ni", "に" }, { "nu", "ぬ" }, { "ne", "ね" }, { "no", "の" },
            { "ha", "は" }, { "hi", "ひ" }, { "hu", "ふ" }, { "fu", "ふ" }, { "he", "へ" }, { "ho", "ほ" },
            { "ma", "ま" }, { "mi", "み" }, { "mu", "む" }, { "me", "め" }, { "mo", "も" },
            { "ya", "や" }, { "yu", "ゆ" }, { "yo", "よ" },
            { "ra", "ら" }, { "ri", "り" }, { "ru", "る" }, { "re", "れ" }, { "ro", "ろ" },
            { "wa", "わ" }, { "wo", "を" }, { "nn", "ん" },
            { "ga", "が" }, { "gi", "ぎ" }, { "gu", "ぐ" }, { "ge", "げ" }, { "go", "ご" },
            { "za", "ざ" }, { "ji", "じ" }, { "zi", "じ" }, { "zu", "ず" }, { "ze", "ぜ" }, { "zo", "ぞ" },
            { "da", "だ" }, { "di", "ぢ" }, { "du", "づ" }, { "de", "で" }, { "do", "ど" },
            { "ba", "ば" }, { "bi", "び" }, { "bu", "ぶ" }, { "be", "べ" }, { "bo", "ぼ" },
            { "pa", "ぱ" }, { "pi", "ぴ" }, { "pu", "ぷ" }, { "pe", "ぺ" }, { "po", "ぽ" },


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
        string original = result;
        result += key.ToString().ToLower();

        // 変換できるローマ字があるか確認
        foreach (var entry in romajiToHiragana)
        {
            if (result.EndsWith(entry.Key))
            {
                //変換前のアルファベット文字を削除
                result = result.Substring(0, result.Length - entry.Key.Length);
                //変換後のひらがなに置き換える
                result += entry.Value;
                break;
            }
        }

        //最大文字数超えで元の文字に戻す
        if (result.Length >= MAX_ANSWER_LENGTH) { result = original; }
    }

    /// <summary>
    /// マイナス等の記号も含む関数
    /// </summary>
    /// <param name="keycode"></param>
    public void OnKeyInput(KeyCode keyCode)
    {
        //記号
        switch (keyCode)
        {
            case KeyCode.Minus:
                OnKeyInput('-');
                return;
        }

        //その他
        if (keyCode >= KeyCode.A && keyCode <= KeyCode.Z) { OnKeyInput(keyCode.ToString()[0]); }
    }

    // 確定した読みを返す
    public string GetResult()
    {
        return result;
    }

    // リセット用
    public void Clear()
    {
        result = "";
    }

    /// <summary>
    /// 一文字消す
    /// </summary>
    public void BackSpace()
    {
        if(result.Length <= 0) { return; }
        result = result.Substring(0, result.Length - 1);
    }
}
