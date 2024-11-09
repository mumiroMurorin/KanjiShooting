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
            // 一文字漢字
            new QuestionData { kanji = "朧", answers = new string[] { "おぼろ" }},
            new QuestionData { kanji = "禅", answers = new string[] { "ぜん" }},
            new QuestionData { kanji = "繭", answers = new string[] { "まゆ" }},
            new QuestionData { kanji = "唇", answers = new string[] { "くちびる" }},
            new QuestionData { kanji = "掌", answers = new string[] { "てのひら","たなごころ" }},

            // 二字熟語
            new QuestionData { kanji = "孤高", answers = new string[] { "ここう" }},
            new QuestionData { kanji = "愁眉", answers = new string[] { "しゅうび" }},
            new QuestionData { kanji = "燦爛", answers = new string[] { "さんらん" }},
            new QuestionData { kanji = "迂遠", answers = new string[] { "うえん" }},
            new QuestionData { kanji = "眩惑", answers = new string[] { "げんわく" }},
            new QuestionData { kanji = "押印", answers = new string[] { "おういん" }},
            new QuestionData { kanji = "捺印", answers = new string[] { "なついん" }},
            new QuestionData { kanji = "悪寒", answers = new string[] { "おかん" }},
            new QuestionData { kanji = "間隙", answers = new string[] { "かんげき" }},
            new QuestionData { kanji = "釣果", answers = new string[] { "ちょうか" }},
            new QuestionData { kanji = "小鼓", answers = new string[] { "こつづみ" }},
            new QuestionData { kanji = "如実", answers = new string[] { "にょじつ" }},
            new QuestionData { kanji = "翻弄", answers = new string[] { "ほんろう" }},
            new QuestionData { kanji = "愛猫", answers = new string[] { "あいびょう" }},

            // 三文字熟語
            new QuestionData { kanji = "春秋学", answers = new string[] { "しゅんじゅうがく" }},
            new QuestionData { kanji = "義勇軍", answers = new string[] { "ぎゆうぐん" }},
            new QuestionData { kanji = "汎神論", answers = new string[] { "はんしんろん" }},
            new QuestionData { kanji = "烏骨鶏", answers = new string[] { "うこっけい" }},
            new QuestionData { kanji = "蝉時雨", answers = new string[] { "せみしぐれ" }},
            new QuestionData { kanji = "秋刀魚", answers = new string[] { "さんま" }},
            new QuestionData { kanji = "伏魔殿", answers = new string[] { "ふくまでん" }},

            // 四字熟語
            new QuestionData { kanji = "泰然自若", answers = new string[] { "たいぜんじじゃく" }},
            new QuestionData { kanji = "羊頭狗肉", answers = new string[] { "ようとうくにく" }},
            new QuestionData { kanji = "流言飛語", answers = new string[] { "りゅうげんひご" }},
            new QuestionData { kanji = "曲学阿世", answers = new string[] { "きょくがくあせい" }},
            new QuestionData { kanji = "獅子奮迅", answers = new string[] { "ししふんじん" }},

            // 動詞
            new QuestionData { kanji = "謳う", answers = new string[] { "うたう" }},
            new QuestionData { kanji = "匿う", answers = new string[] { "かくまう" }},
            new QuestionData { kanji = "穿つ", answers = new string[] { "うがつ" }},
            new QuestionData { kanji = "偲ぶ", answers = new string[] { "しのぶ" }},
            new QuestionData { kanji = "崇める", answers = new string[] { "あがめる" }},
            new QuestionData { kanji = "脅かす", answers = new string[] { "おびやかす" }},
            new QuestionData { kanji = "覆る", answers = new string[] { "くつがえる" }},
            new QuestionData { kanji = "虐げる", answers = new string[] { "しいたげる" }},
            new QuestionData { kanji = "唆す", answers = new string[] { "そそのかす" }},

            // 形容詞
            new QuestionData { kanji = "妖艶", answers = new string[] { "ようえん" }},
            new QuestionData { kanji = "峻厳", answers = new string[] { "しゅんげん" }},
            new QuestionData { kanji = "荘厳", answers = new string[] { "そうごん" }},
            new QuestionData { kanji = "艶麗", answers = new string[] { "えんれい" }},
            new QuestionData { kanji = "歪む", answers = new string[] { "ゆがむ" }},
            new QuestionData { kanji = "懇ろ", answers = new string[] { "ねんごろ" }},

            // 特殊な読み方の地名
            new QuestionData { kanji = "稚内市", answers = new string[] { "わっかないし" }},
            new QuestionData { kanji = "常滑市", answers = new string[] { "とこなめし" }},
            new QuestionData { kanji = "青龍寺", answers = new string[] { "せいりゅうじ" }},
            new QuestionData { kanji = "鞍馬山", answers = new string[] { "くらまやま" }},

            // 偉人の名前
            new QuestionData { kanji = "熊沢蕃山", answers = new string[] { "くまざわばんざん" }},
            new QuestionData { kanji = "平賀源内", answers = new string[] { "ひらがげんない" }},
            new QuestionData { kanji = "伊藤仁斎", answers = new string[] { "いとうじんさい" }}
        };


        return datas[Random.Range(0, datas.Length)];
    }
}
