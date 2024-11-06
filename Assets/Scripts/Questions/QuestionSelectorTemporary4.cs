using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class QuestionSelectorTemporary4 : MonoBehaviour, IQuestionSelector
{
    public QuestionData GetQuestionData(QuestionFilter filter) 
    {
        QuestionData[] datas = new QuestionData[]
        {
            new QuestionData { kanji = "èw", answers = new string[] { "‚Ì‚İ" }},
            new QuestionData { kanji = "€‚é", answers = new string[] { "‚­‚µ‚¯‚¸‚é" }},
            new QuestionData { kanji = "‘ìåŞ‘ä", answers = new string[] { "‚¿‚á‚Ô‚¾‚¢" }},
            new QuestionData { kanji = "‰Í“Ø", answers = new string[] { "‚Ó‚®" }},
            new QuestionData { kanji = "äKãëä`", answers = new string[] { "‚¨‚Á‚Æ‚¹‚¢" }},
            new QuestionData { kanji = "éZéJ", answers = new string[] { "‚¤‚Ç‚ñ" }},
            new QuestionData { kanji = "çë", answers = new string[] { "‚È‚½" }},
            new QuestionData { kanji = "ˆŞ‚ê‚é", answers = new string[] { "‚µ‚¨‚ê‚é" }},
            new QuestionData { kanji = "ê`", answers = new string[] { "‚±‚¤‚Ì‚Æ‚è" }},
            new QuestionData { kanji = "°", answers = new string[] { "‚µ‚¬" }},
            new QuestionData { kanji = "å©’J", answers = new string[] { "‚±‚ß‚©‚İ" }},
            new QuestionData { kanji = "å§å©", answers = new string[] { "‚±‚¨‚ë‚¬" }},
            new QuestionData { kanji = "’µ—Àæëç»", answers = new string[] { "‚¿‚å‚¤‚è‚å‚¤‚Î‚Á‚±" }},
            new QuestionData { kanji = "áûáû——", answers = new string[] { "‚ç‚¢‚ç‚¢‚ç‚­‚ç‚­" }},
            new QuestionData { kanji = "á‚®", answers = new string[] { "‚·‚·‚®","‚»‚»‚®","" }},
            new QuestionData { kanji = "á·‰ñ‚µ", answers = new string[] { "‚½‚ç‚¢‚Ü‚í‚µ" }},
            new QuestionData { kanji = "‘‡‚®", answers = new string[] { "‚Í‚µ‚á‚®" }},
            new QuestionData { kanji = "Š°‚®", answers = new string[] { "‚­‚Â‚ë‚®" }},
            new QuestionData { kanji = "å´‚è", answers = new string[] { "‚í‚¾‚©‚Ü‚è" }},
            new QuestionData { kanji = "—¬—£‚¤", answers = new string[] { "‚³‚·‚ç‚¤" }},
            new QuestionData { kanji = "íœÉ‚­", answers = new string[] { "‚í‚È‚È‚­" }},
            new QuestionData { kanji = "í‚­", answers = new string[] { "‚¨‚Ì‚Ì‚­" }},
            new QuestionData { kanji = "í‚®", answers = new string[] { "‚»‚æ‚®" }},
            new QuestionData { kanji = "èÂ", answers = new string[] { "‚İ‚¼‚ê" }},
            new QuestionData { kanji = "Rˆ¨", answers = new string[] { "‚í‚³‚Ñ" }},
            new QuestionData { kanji = "…‰Z", answers = new string[] { "‚Ö‚¿‚Ü" }},
            new QuestionData { kanji = "ŸóŸò”_‹Æ", answers = new string[] { "‚©‚ñ‚ª‚¢‚Ì‚¤‚¬‚å‚¤" }},
            new QuestionData { kanji = "rU‚é", answers = new string[] { "‚¢‚½‚Ô‚é" }},
            new QuestionData { kanji = "šŠ", answers = new string[] { "‚­‚µ‚á‚İ" }},
            new QuestionData { kanji = "‹Êå†‹o", answers = new string[] { "‚Æ‚¤‚à‚ë‚±‚µ" }},
            new QuestionData { kanji = "‹h‹t", answers = new string[] { "‚µ‚á‚Á‚­‚è","‚«‚Â‚¬‚á‚­" }},
            new QuestionData { kanji = "‹h‹Á", answers = new string[] { "‚Ñ‚Á‚­‚è","‚«‚Á‚«‚å‚¤" }},
            new QuestionData { kanji = "ãŞ‚¦‚é", answers = new string[] { "‚»‚Ñ‚¦‚é" }},
            new QuestionData { kanji = "Š†‚à", answers = new string[] { "‚ ‚½‚©‚à" }},
            new QuestionData { kanji = "“Dà^", answers = new string[] { "‚Å‚¢‚Ë‚¢","‚Ê‚©‚é‚İ" }},
            new QuestionData { kanji = "Ò‰©Âê_šF", answers = new string[] { "‚¹‚«‚¹‚¢‚¢‚ñ‚±" }},
            new QuestionData { kanji = "›ßŒÈ–¤ŒÈ", answers = new string[] { "‚¢‚±‚İ‚«" }},
            new QuestionData { kanji = "éç", answers = new string[] { "‚·‚¸‚«" }},
            new QuestionData { kanji = "ãÌ‚ê", answers = new string[] { "‚¨‚¢‚Ú‚ê" }},
        };


        return datas[Random.Range(0, datas.Length)];
    }
}
