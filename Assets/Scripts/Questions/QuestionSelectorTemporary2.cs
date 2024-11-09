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
            // ˆê•¶šŠ¿š
            new QuestionData { kanji = "O", answers = new string[] { "‚¨‚Ú‚ë" }},
            new QuestionData { kanji = "‘T", answers = new string[] { "‚º‚ñ" }},
            new QuestionData { kanji = "–š", answers = new string[] { "‚Ü‚ä" }},
            new QuestionData { kanji = "O", answers = new string[] { "‚­‚¿‚Ñ‚é" }},
            new QuestionData { kanji = "¶", answers = new string[] { "‚Ä‚Ì‚Ğ‚ç","‚½‚È‚²‚±‚ë" }},

            // “ñšnŒê
            new QuestionData { kanji = "ŒÇ‚", answers = new string[] { "‚±‚±‚¤" }},
            new QuestionData { kanji = "D”û", answers = new string[] { "‚µ‚ã‚¤‚Ñ" }},
            new QuestionData { kanji = "Wà£", answers = new string[] { "‚³‚ñ‚ç‚ñ" }},
            new QuestionData { kanji = "‰I‰“", answers = new string[] { "‚¤‚¦‚ñ" }},
            new QuestionData { kanji = "á¿˜f", answers = new string[] { "‚°‚ñ‚í‚­" }},
            new QuestionData { kanji = "‰Ÿˆó", answers = new string[] { "‚¨‚¤‚¢‚ñ" }},
            new QuestionData { kanji = "“æˆó", answers = new string[] { "‚È‚Â‚¢‚ñ" }},
            new QuestionData { kanji = "ˆ«Š¦", answers = new string[] { "‚¨‚©‚ñ" }},
            new QuestionData { kanji = "ŠÔŒ„", answers = new string[] { "‚©‚ñ‚°‚«" }},
            new QuestionData { kanji = "’Ş‰Ê", answers = new string[] { "‚¿‚å‚¤‚©" }},
            new QuestionData { kanji = "¬ŒÛ", answers = new string[] { "‚±‚Â‚Ã‚İ" }},
            new QuestionData { kanji = "”@À", answers = new string[] { "‚É‚å‚¶‚Â" }},
            new QuestionData { kanji = "–|˜M", answers = new string[] { "‚Ù‚ñ‚ë‚¤" }},
            new QuestionData { kanji = "ˆ¤”L", answers = new string[] { "‚ ‚¢‚Ñ‚å‚¤" }},

            // O•¶šnŒê
            new QuestionData { kanji = "tHŠw", answers = new string[] { "‚µ‚ã‚ñ‚¶‚ã‚¤‚ª‚­" }},
            new QuestionData { kanji = "‹`—EŒR", answers = new string[] { "‚¬‚ä‚¤‚®‚ñ" }},
            new QuestionData { kanji = "”Ä_˜_", answers = new string[] { "‚Í‚ñ‚µ‚ñ‚ë‚ñ" }},
            new QuestionData { kanji = "‰GœŒ{", answers = new string[] { "‚¤‚±‚Á‚¯‚¢" }},
            new QuestionData { kanji = "ä‰J", answers = new string[] { "‚¹‚İ‚µ‚®‚ê" }},
            new QuestionData { kanji = "H“‹›", answers = new string[] { "‚³‚ñ‚Ü" }},
            new QuestionData { kanji = "•š–‚“a", answers = new string[] { "‚Ó‚­‚Ü‚Å‚ñ" }},

            // lšnŒê
            new QuestionData { kanji = "‘×‘R©á", answers = new string[] { "‚½‚¢‚º‚ñ‚¶‚¶‚á‚­" }},
            new QuestionData { kanji = "—r“ª‹ç“÷", answers = new string[] { "‚æ‚¤‚Æ‚¤‚­‚É‚­" }},
            new QuestionData { kanji = "—¬Œ¾”òŒê", answers = new string[] { "‚è‚ã‚¤‚°‚ñ‚Ğ‚²" }},
            new QuestionData { kanji = "‹ÈŠwˆ¢¢", answers = new string[] { "‚«‚å‚­‚ª‚­‚ ‚¹‚¢" }},
            new QuestionData { kanji = "‚q•±v", answers = new string[] { "‚µ‚µ‚Ó‚ñ‚¶‚ñ" }},

            // “®Œ
            new QuestionData { kanji = "æ‚¤", answers = new string[] { "‚¤‚½‚¤" }},
            new QuestionData { kanji = "“½‚¤", answers = new string[] { "‚©‚­‚Ü‚¤" }},
            new QuestionData { kanji = "ú‚Â", answers = new string[] { "‚¤‚ª‚Â" }},
            new QuestionData { kanji = "Ã‚Ô", answers = new string[] { "‚µ‚Ì‚Ô" }},
            new QuestionData { kanji = "’‚ß‚é", answers = new string[] { "‚ ‚ª‚ß‚é" }},
            new QuestionData { kanji = "‹º‚©‚·", answers = new string[] { "‚¨‚Ñ‚â‚©‚·" }},
            new QuestionData { kanji = "•¢‚é", answers = new string[] { "‚­‚Â‚ª‚¦‚é" }},
            new QuestionData { kanji = "‹s‚°‚é", answers = new string[] { "‚µ‚¢‚½‚°‚é" }},
            new QuestionData { kanji = "´‚·", answers = new string[] { "‚»‚»‚Ì‚©‚·" }},

            // Œ`—eŒ
            new QuestionData { kanji = "—d‰", answers = new string[] { "‚æ‚¤‚¦‚ñ" }},
            new QuestionData { kanji = "sŒµ", answers = new string[] { "‚µ‚ã‚ñ‚°‚ñ" }},
            new QuestionData { kanji = "‘‘Œµ", answers = new string[] { "‚»‚¤‚²‚ñ" }},
            new QuestionData { kanji = "‰—í", answers = new string[] { "‚¦‚ñ‚ê‚¢" }},
            new QuestionData { kanji = "˜c‚Ş", answers = new string[] { "‚ä‚ª‚Ş" }},
            new QuestionData { kanji = "§‚ë", answers = new string[] { "‚Ë‚ñ‚²‚ë" }},

            // “Áê‚È“Ç‚İ•û‚Ì’n–¼
            new QuestionData { kanji = "’t“às", answers = new string[] { "‚í‚Á‚©‚È‚¢‚µ" }},
            new QuestionData { kanji = "íŠŠs", answers = new string[] { "‚Æ‚±‚È‚ß‚µ" }},
            new QuestionData { kanji = "Â—´›", answers = new string[] { "‚¹‚¢‚è‚ã‚¤‚¶" }},
            new QuestionData { kanji = "ˆÆ”nR", answers = new string[] { "‚­‚ç‚Ü‚â‚Ü" }},

            // ˆÌl‚Ì–¼‘O
            new QuestionData { kanji = "ŒF‘ò”×R", answers = new string[] { "‚­‚Ü‚´‚í‚Î‚ñ‚´‚ñ" }},
            new QuestionData { kanji = "•½‰êŒ¹“à", answers = new string[] { "‚Ğ‚ç‚ª‚°‚ñ‚È‚¢" }},
            new QuestionData { kanji = "ˆÉ“¡mÖ", answers = new string[] { "‚¢‚Æ‚¤‚¶‚ñ‚³‚¢" }}
        };


        return datas[Random.Range(0, datas.Length)];
    }
}
