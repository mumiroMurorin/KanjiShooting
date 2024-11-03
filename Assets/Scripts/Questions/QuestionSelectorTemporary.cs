using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class QuestionSelectorTemporary : MonoBehaviour, IQuestionSelector
{
    public QuestionData GetQuestionData(QuestionFilter filter) 
    {
        QuestionData[] datas = new QuestionData[]
        {
            new QuestionData { kanji = "äKãëä`", answers = new string[] { "‚¨‚Á‚Æ‚¹‚¢" }},
            new QuestionData { kanji = "èw", answers = new string[] { "‚Ì‚İ" }},
            new QuestionData { kanji = "‹à“t‹à", answers = new string[] { "‚«‚ñ‚ß‚Á‚«" }},
            new QuestionData { kanji = "‹h‹t", answers = new string[] { "‚µ‚á‚Á‚­‚è" }},
            new QuestionData { kanji = "‹Êå†‹o", answers = new string[] { "‚Æ‚¤‚à‚ë‚±‚µ" }},
            new QuestionData { kanji = "çë", answers = new string[] { "‚È‚½" }},
            new QuestionData { kanji = "‰Í“Ø", answers = new string[] { "‚Ó‚®" }},
            new QuestionData { kanji = "‘ìåŞ‘ä", answers = new string[] { "‚¿‚á‚Ô‚¾‚¢" }},
            new QuestionData { kanji = "â¿", answers = new string[] { "‚Ó‚é‚¢" }},
            new QuestionData { kanji = "á‰ÔØ", answers = new string[] { "‚¨‚©‚ç" }},
            new QuestionData { kanji = "‹Så‘åx", answers = new string[] { "‚¨‚É‚â‚ñ‚Ü" }},
            new QuestionData { kanji = "è–ò—ù", answers = new string[] { "‚Ä‚®‚·‚Ë" }},
            new QuestionData { kanji = "å‹Ç", answers = new string[] { "‚Æ‚®‚ë" }}
        };


        return datas[Random.Range(0, datas.Length)];
    }
}
