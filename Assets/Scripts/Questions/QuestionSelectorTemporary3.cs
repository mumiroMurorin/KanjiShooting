using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Kanji;

public class QuestionSelectorTemporary3 : IQuestionSelector
{
    public void Initialize() { }

    public QuestionData GetQuestionData(QuestionFilter filter) 
    {
        QuestionData[] datas = new QuestionData[]
        {
            // 一文字漢字
            new QuestionData { kanji = "掬", answers = new string[] { "きく" }},
            new QuestionData { kanji = "鮪", answers = new string[] { "まぐろ","しび" }},
            new QuestionData { kanji = "燕", answers = new string[] { "つばめ","さかもり" }},
            new QuestionData { kanji = "蠅", answers = new string[] { "はえ" }},
            new QuestionData { kanji = "谺", answers = new string[] { "こだま","やまびこ" }},
            new QuestionData { kanji = "漣", answers = new string[] { "さざなみ" }},


            // 二字熟語
            new QuestionData { kanji = "団扇", answers = new string[] { "うちわ" }},
            new QuestionData { kanji = "慇懃", answers = new string[] { "いんぎん" }},
            new QuestionData { kanji = "恍惚", answers = new string[] { "こうこつ" }},
            new QuestionData { kanji = "懶惰", answers = new string[] { "らんだ","らいだ" }},
            new QuestionData { kanji = "愈々", answers = new string[] { "いよいよ" }},
            new QuestionData { kanji = "僭越", answers = new string[] { "せんえつ" }},
            new QuestionData { kanji = "罷免", answers = new string[] { "ひめん" }},
            new QuestionData { kanji = "踏襲", answers = new string[] { "とうしゅう" }},
            new QuestionData { kanji = "疎い", answers = new string[] { "うとい" }},
            new QuestionData { kanji = "鑑みる", answers = new string[] { "かんがみる" }},
            new QuestionData { kanji = "適宜", answers = new string[] { "てきぎ" }},
            new QuestionData { kanji = "遺憾", answers = new string[] { "いかん" }},
            new QuestionData { kanji = "真摯", answers = new string[] { "しんし" }},
            new QuestionData { kanji = "懸念", answers = new string[] { "けねん" }},
            new QuestionData { kanji = "進捗", answers = new string[] { "しんちょく" }},
            new QuestionData { kanji = "所謂", answers = new string[] { "いわゆる" }},
            new QuestionData { kanji = "訃報", answers = new string[] { "ふほう" }},
            new QuestionData { kanji = "代替", answers = new string[] { "だいたい" }},
            new QuestionData { kanji = "施工", answers = new string[] { "せこう" }},
            new QuestionData { kanji = "蒐集", answers = new string[] { "しゅうしゅう" }},
            new QuestionData { kanji = "弄る", answers = new string[] { "いじる" }},
            new QuestionData { kanji = "遵守", answers = new string[] { "じゅんしゅ" }},
            new QuestionData { kanji = "示唆", answers = new string[] { "しさ" }},
            new QuestionData { kanji = "逝去", answers = new string[] { "せいきょ" }},
            new QuestionData { kanji = "瑕疵", answers = new string[] { "かし" }},
            new QuestionData { kanji = "俯瞰", answers = new string[] { "ふかん" }},
            new QuestionData { kanji = "拙速", answers = new string[] { "せっそく" }},
            new QuestionData { kanji = "煩雑", answers = new string[] { "はんざつ" }},
            new QuestionData { kanji = "轍", answers = new string[] { "わだち","てつ" }},
            new QuestionData { kanji = "揶揄", answers = new string[] { "やゆ" }},
            new QuestionData { kanji = "啓蒙", answers = new string[] { "けいもう" }},
            new QuestionData { kanji = "斡旋", answers = new string[] { "あっせん" }},
            new QuestionData { kanji = "翡翠", answers = new string[] { "ひすい","かわせみ" }},
            new QuestionData { kanji = "慟哭", answers = new string[] { "どうこく" }},
            new QuestionData { kanji = "詭弁", answers = new string[] { "きべん" }},
            new QuestionData { kanji = "欺瞞", answers = new string[] { "ぎまん" }},
            new QuestionData { kanji = "僥倖", answers = new string[] { "ぎょうこう" }},
            new QuestionData { kanji = "励行", answers = new string[] { "れいこう" }},
            new QuestionData { kanji = "未曾有", answers = new string[] { "みぞう" }},
            new QuestionData { kanji = "狡猾", answers = new string[] { "こうかつ" }},
            new QuestionData { kanji = "迎合", answers = new string[] { "げいごう" }},
            new QuestionData { kanji = "鯊", answers = new string[] { "はぜ" }},
            new QuestionData { kanji = "軋轢", answers = new string[] { "あつれき" }},
            new QuestionData { kanji = "対峙", answers = new string[] { "たいじ" }},
            new QuestionData { kanji = "終焉", answers = new string[] { "しゅうえん" }},
            new QuestionData { kanji = "閾値", answers = new string[] { "いきち","しきいち" }},
            new QuestionData { kanji = "叡智", answers = new string[] { "えいち" }},
            new QuestionData { kanji = "誤謬", answers = new string[] { "ごびゅう" }},
            new QuestionData { kanji = "魁", answers = new string[] { "さきがけ" }},
            new QuestionData { kanji = "吐露", answers = new string[] { "とろ" }},
            new QuestionData { kanji = "矜持", answers = new string[] { "きょうじ" }},
            new QuestionData { kanji = "弔問", answers = new string[] { "ちょうもん" }},
            new QuestionData { kanji = "朦朧", answers = new string[] { "もうろう" }},
            new QuestionData { kanji = "憔悴", answers = new string[] { "しょうすい" }},
            new QuestionData { kanji = "殊勝", answers = new string[] { "しゅしょう" }},
            new QuestionData { kanji = "贖罪", answers = new string[] { "しょくざい" }},
            new QuestionData { kanji = "彗星", answers = new string[] { "すいせい" }},
            new QuestionData { kanji = "逡巡", answers = new string[] { "しゅんじゅん" }},
            new QuestionData { kanji = "諮問", answers = new string[] { "しもん" }},
            new QuestionData { kanji = "反芻", answers = new string[] { "はんすう" }},

            // 三文字熟語
            new QuestionData { kanji = "泡沫夢", answers = new string[] { "ほうまつむ" }},
            new QuestionData { kanji = "遺棄物", answers = new string[] { "いきぶつ" }},
            new QuestionData { kanji = "益荒男", answers = new string[] { "ますらお" }},
            new QuestionData { kanji = "如何様", answers = new string[] { "いかさま" }},
            new QuestionData { kanji = "微温湯", answers = new string[] { "ぬるまゆ" }},


            // 四字熟語
            new QuestionData { kanji = "喧々囂々", answers = new string[] { "けんけんごうごう" }},
            new QuestionData { kanji = "侃侃諤諤", answers = new string[] { "かんかんがくがく" }},
            new QuestionData { kanji = "不撓不屈", answers = new string[] { "ふとうふくつ" }},
            new QuestionData { kanji = "悲喜交交", answers = new string[] { "ひきこもごも" }},
            new QuestionData { kanji = "魑魅魍魎", answers = new string[] { "ちみもうりょう" }},

            // 動詞
            new QuestionData { kanji = "愁える", answers = new string[] { "うれえる" }},
            new QuestionData { kanji = "攫う", answers = new string[] { "さらう" }},
            new QuestionData { kanji = "辿る", answers = new string[] { "たどる" }},
            new QuestionData { kanji = "綴る", answers = new string[] { "つづる" }},
            new QuestionData { kanji = "奉る", answers = new string[] { "たてまつる","うけたまわる" }},

            // 形容詞
            new QuestionData { kanji = "悖る", answers = new string[] { "もとる" }},
            new QuestionData { kanji = "芳しい", answers = new string[] { "かぐわしい","かんばしい" }},
            new QuestionData { kanji = "饒舌", answers = new string[] { "じょうぜつ" }},
            new QuestionData { kanji = "悍ましい", answers = new string[] { "おぞましい" }},
            new QuestionData { kanji = "常しえ", answers = new string[] { "とこしえ" }},

        };


        return datas[Random.Range(0, datas.Length)];
    }
}
