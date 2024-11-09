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
            new QuestionData { kanji = "鬱", answers = new string[] { "うつ" }},
            new QuestionData { kanji = "渦", answers = new string[] { "うず" }},
            new QuestionData { kanji = "霧", answers = new string[] { "きり" }},
            new QuestionData { kanji = "畝", answers = new string[] { "うね","せ" }},
            new QuestionData { kanji = "虞", answers = new string[] { "おそれ" }},
            new QuestionData { kanji = "顎", answers = new string[] { "あご","あぎと" }},

            // 二字熟語
            new QuestionData { kanji = "紅葉", answers = new string[] { "こうよう", "もみじ" }},
            new QuestionData { kanji = "凶兆", answers = new string[] { "きょうちょう" }},
            new QuestionData { kanji = "融解", answers = new string[] { "ゆうかい" }},
            new QuestionData { kanji = "清浄", answers = new string[] { "せいじょう" }},
            new QuestionData { kanji = "養護", answers = new string[] { "ようご" }},
            new QuestionData { kanji = "豪華", answers = new string[] { "ごうか" }},
            new QuestionData { kanji = "挨拶", answers = new string[] { "あいさつ" }},
            new QuestionData { kanji = "旺盛", answers = new string[] { "おうせい" }},
            new QuestionData { kanji = "瓦礫", answers = new string[] { "がれき" }},
            new QuestionData { kanji = "寡黙", answers = new string[] { "かもく" }},
            new QuestionData { kanji = "卸売", answers = new string[] { "おろしうり" }},
            new QuestionData { kanji = "誘拐", answers = new string[] { "ゆうかい" }},
            new QuestionData { kanji = "飢餓", answers = new string[] { "きが" }},
            new QuestionData { kanji = "珈琲", answers = new string[] { "こーひー" }},
            new QuestionData { kanji = "窮屈", answers = new string[] { "きゅうくつ" }},
            new QuestionData { kanji = "愚行", answers = new string[] { "ぐこう" }},


            // 三文字熟語
            new QuestionData { kanji = "貿易風", answers = new string[] { "ぼうえきふう" }},
            new QuestionData { kanji = "自然界", answers = new string[] { "しぜんかい" }},
            new QuestionData { kanji = "共産党", answers = new string[] { "きょうさんとう" }},
            new QuestionData { kanji = "核分裂", answers = new string[] { "かくぶんれつ" }},
            new QuestionData { kanji = "語彙力", answers = new string[] { "ごいりょく" }},
            new QuestionData { kanji = "一周忌", answers = new string[] { "いっしゅうき" }},
            new QuestionData { kanji = "喫茶店", answers = new string[] { "きっさてん" }},
            new QuestionData { kanji = "肩甲骨", answers = new string[] { "けんこうこつ" }},
            new QuestionData { kanji = "伯爵家", answers = new string[] { "はくしゃくけ" }},

            // 四字熟語
            new QuestionData { kanji = "明鏡止水", answers = new string[] { "めいきょうしすい" }},
            new QuestionData { kanji = "一石二鳥", answers = new string[] { "いっせきにちょう" }},
            new QuestionData { kanji = "誠心誠意", answers = new string[] { "せいしんせいい" }},
            new QuestionData { kanji = "自業自得", answers = new string[] { "じごうじとく" }},
            new QuestionData { kanji = "風林火山", answers = new string[] { "ふうりんかざん" }},

            // 動詞
            new QuestionData { kanji = "養う", answers = new string[] { "やしなう" }},
            new QuestionData { kanji = "称える", answers = new string[] { "たたえる" }},
            new QuestionData { kanji = "遮る", answers = new string[] { "さえぎる" }},
            new QuestionData { kanji = "促す", answers = new string[] { "うながす" }},
            new QuestionData { kanji = "継ぐ", answers = new string[] { "つぐ" }},
            new QuestionData { kanji = "戒め", answers = new string[] { "いましめ" }},
            new QuestionData { kanji = "恵む", answers = new string[] { "めぐむ" }},
            new QuestionData { kanji = "戯れる", answers = new string[] { "たわむれる" }},
            new QuestionData { kanji = "鑑みる", answers = new string[] { "かんがみる" }},

            // 形容詞
            new QuestionData { kanji = "儚い", answers = new string[] { "はかない" }},
            new QuestionData { kanji = "脆い", answers = new string[] { "もろい" }},
            new QuestionData { kanji = "華やか", answers = new string[] { "はなやか" }},
            new QuestionData { kanji = "滑らか", answers = new string[] { "なめらか" }},
            new QuestionData { kanji = "専ら", answers = new string[] { "もっぱら" }},

            // 特殊な読み方の地名
            new QuestionData { kanji = "石狩", answers = new string[] { "いしかり" }},
            new QuestionData { kanji = "宍道湖", answers = new string[] { "しんじこ" }},
            new QuestionData { kanji = "淡路島", answers = new string[] { "あわじしま" }},
            new QuestionData { kanji = "糸魚川", answers = new string[] { "いといがわ" }},
            new QuestionData { kanji = "根室", answers = new string[] { "ねむろ" }},

            // 偉人の名前
            new QuestionData { kanji = "織田信長", answers = new string[] { "おだのぶなが" }},
            new QuestionData { kanji = "西郷隆盛", answers = new string[] { "さいごうたかもり" }},
            new QuestionData { kanji = "徳川家康", answers = new string[] { "とくがわいえやす" }},
            new QuestionData { kanji = "聖徳太子", answers = new string[] { "しょうとくたいし" }},
            new QuestionData { kanji = "武田信玄", answers = new string[] { "たけだしんげん" }}
        };


        return datas[Random.Range(0, datas.Length)];
    }
}
