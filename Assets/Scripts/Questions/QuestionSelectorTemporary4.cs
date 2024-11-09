using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Kanji;

public class QuestionSelectorTemporary4 : IQuestionSelector
{
    public void Initialize() { }

    public QuestionData GetQuestionData(QuestionFilter filter) 
    {
        QuestionData[] datas = new QuestionData[]
        {
            new QuestionData { kanji = "鑿", answers = new string[] { "のみ" }},
            new QuestionData { kanji = "梳る", answers = new string[] { "くしけずる" }},
            new QuestionData { kanji = "卓袱台", answers = new string[] { "ちゃぶだい" }},
            new QuestionData { kanji = "河豚", answers = new string[] { "ふぐ" }},
            new QuestionData { kanji = "膃肭臍", answers = new string[] { "おっとせい" }},
            new QuestionData { kanji = "饂飩", answers = new string[] { "うどん" }},
            new QuestionData { kanji = "鉈", answers = new string[] { "なた" }},
            new QuestionData { kanji = "萎れる", answers = new string[] { "しおれる" }},
            new QuestionData { kanji = "鸛", answers = new string[] { "こうのとり" }},
            new QuestionData { kanji = "鴫", answers = new string[] { "しぎ" }},
            new QuestionData { kanji = "蟀谷", answers = new string[] { "こめかみ" }},
            new QuestionData { kanji = "蟋蟀", answers = new string[] { "こおろぎ" }},
            new QuestionData { kanji = "跳梁跋扈", answers = new string[] { "ちょうりょうばっこ" }},
            new QuestionData { kanji = "磊磊落落", answers = new string[] { "らいらいらくらく" }},
            new QuestionData { kanji = "雪ぐ", answers = new string[] { "すすぐ","そそぐ","" }},
            new QuestionData { kanji = "盥回し", answers = new string[] { "たらいまわし" }},
            new QuestionData { kanji = "燥ぐ", answers = new string[] { "はしゃぐ" }},
            new QuestionData { kanji = "寛ぐ", answers = new string[] { "くつろぐ" }},
            new QuestionData { kanji = "蟠り", answers = new string[] { "わだかまり" }},
            new QuestionData { kanji = "流離う", answers = new string[] { "さすらう" }},
            new QuestionData { kanji = "戦慄く", answers = new string[] { "わななく" }},
            new QuestionData { kanji = "戦く", answers = new string[] { "おののく" }},
            new QuestionData { kanji = "戦ぐ", answers = new string[] { "そよぐ" }},
            new QuestionData { kanji = "霙", answers = new string[] { "みぞれ" }},
            new QuestionData { kanji = "山葵", answers = new string[] { "わさび" }},
            new QuestionData { kanji = "糸瓜", answers = new string[] { "へちま" }},
            new QuestionData { kanji = "灌漑農業", answers = new string[] { "かんがいのうぎょう" }},
            new QuestionData { kanji = "甚振る", answers = new string[] { "いたぶる" }},
            new QuestionData { kanji = "嚏", answers = new string[] { "くしゃみ" }},
            new QuestionData { kanji = "玉蜀黍", answers = new string[] { "とうもろこし" }},
            new QuestionData { kanji = "吃逆", answers = new string[] { "しゃっくり","きつぎゃく" }},
            new QuestionData { kanji = "吃驚", answers = new string[] { "びっくり","きっきょう" }},
            new QuestionData { kanji = "聳える", answers = new string[] { "そびえる" }},
            new QuestionData { kanji = "恰も", answers = new string[] { "あたかも" }},
            new QuestionData { kanji = "泥濘", answers = new string[] { "でいねい","ぬかるみ" }},
            new QuestionData { kanji = "脊黄青鸚哥", answers = new string[] { "せきせいいんこ" }},
            new QuestionData { kanji = "已己巳己", answers = new string[] { "いこみき" }},
            new QuestionData { kanji = "鱸", answers = new string[] { "すずき" }},
            new QuestionData { kanji = "耄れ", answers = new string[] { "おいぼれ" }},
        };


        return datas[Random.Range(0, datas.Length)];
    }
}
