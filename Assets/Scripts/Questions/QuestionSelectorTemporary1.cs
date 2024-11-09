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
            new QuestionData { kanji = "ŸT", answers = new string[] { "‚¤‚Â" }},
            new QuestionData { kanji = "‰Q", answers = new string[] { "‚¤‚¸" }},
            new QuestionData { kanji = "–¶", answers = new string[] { "‚«‚è" }},
            new QuestionData { kanji = "¤", answers = new string[] { "‚¤‚Ë","‚¹" }},
            new QuestionData { kanji = "‹ñ", answers = new string[] { "‚¨‚»‚ê" }},
            new QuestionData { kanji = "Š{", answers = new string[] { "‚ ‚²","‚ ‚¬‚Æ" }},

            // “ñšnŒê
            new QuestionData { kanji = "g—t", answers = new string[] { "‚±‚¤‚æ‚¤", "‚à‚İ‚¶" }},
            new QuestionData { kanji = "‹¥’›", answers = new string[] { "‚«‚å‚¤‚¿‚å‚¤" }},
            new QuestionData { kanji = "—Z‰ğ", answers = new string[] { "‚ä‚¤‚©‚¢" }},
            new QuestionData { kanji = "´ò", answers = new string[] { "‚¹‚¢‚¶‚å‚¤" }},
            new QuestionData { kanji = "—{Œì", answers = new string[] { "‚æ‚¤‚²" }},
            new QuestionData { kanji = "‹‰Ø", answers = new string[] { "‚²‚¤‚©" }},
            new QuestionData { kanji = "ˆ¥A", answers = new string[] { "‚ ‚¢‚³‚Â" }},
            new QuestionData { kanji = "‰ ·", answers = new string[] { "‚¨‚¤‚¹‚¢" }},
            new QuestionData { kanji = "Š¢âI", answers = new string[] { "‚ª‚ê‚«" }},
            new QuestionData { kanji = "‰Ç–Ù", answers = new string[] { "‚©‚à‚­" }},
            new QuestionData { kanji = "‰µ”„", answers = new string[] { "‚¨‚ë‚µ‚¤‚è" }},
            new QuestionData { kanji = "—U‰û", answers = new string[] { "‚ä‚¤‚©‚¢" }},
            new QuestionData { kanji = "‹Q‰ì", answers = new string[] { "‚«‚ª" }},
            new QuestionData { kanji = "àÛàè", answers = new string[] { "‚±[‚Ğ[" }},
            new QuestionData { kanji = "‹‡‹ü", answers = new string[] { "‚«‚ã‚¤‚­‚Â" }},
            new QuestionData { kanji = "‹ğs", answers = new string[] { "‚®‚±‚¤" }},


            // O•¶šnŒê
            new QuestionData { kanji = "–fˆÕ•—", answers = new string[] { "‚Ú‚¤‚¦‚«‚Ó‚¤" }},
            new QuestionData { kanji = "©‘RŠE", answers = new string[] { "‚µ‚º‚ñ‚©‚¢" }},
            new QuestionData { kanji = "‹¤Y“}", answers = new string[] { "‚«‚å‚¤‚³‚ñ‚Æ‚¤" }},
            new QuestionData { kanji = "Šj•ª—ô", answers = new string[] { "‚©‚­‚Ô‚ñ‚ê‚Â" }},
            new QuestionData { kanji = "Œêœb—Í", answers = new string[] { "‚²‚¢‚è‚å‚­" }},
            new QuestionData { kanji = "ˆêüŠõ", answers = new string[] { "‚¢‚Á‚µ‚ã‚¤‚«" }},
            new QuestionData { kanji = "‹i’ƒ“X", answers = new string[] { "‚«‚Á‚³‚Ä‚ñ" }},
            new QuestionData { kanji = "Œ¨bœ", answers = new string[] { "‚¯‚ñ‚±‚¤‚±‚Â" }},
            new QuestionData { kanji = "”Œİ‰Æ", answers = new string[] { "‚Í‚­‚µ‚á‚­‚¯" }},

            // lšnŒê
            new QuestionData { kanji = "–¾‹¾~…", answers = new string[] { "‚ß‚¢‚«‚å‚¤‚µ‚·‚¢" }},
            new QuestionData { kanji = "ˆêÎ“ñ’¹", answers = new string[] { "‚¢‚Á‚¹‚«‚É‚¿‚å‚¤" }},
            new QuestionData { kanji = "½S½ˆÓ", answers = new string[] { "‚¹‚¢‚µ‚ñ‚¹‚¢‚¢" }},
            new QuestionData { kanji = "©‹Æ©“¾", answers = new string[] { "‚¶‚²‚¤‚¶‚Æ‚­" }},
            new QuestionData { kanji = "•——Ñ‰ÎR", answers = new string[] { "‚Ó‚¤‚è‚ñ‚©‚´‚ñ" }},

            // “®Œ
            new QuestionData { kanji = "—{‚¤", answers = new string[] { "‚â‚µ‚È‚¤" }},
            new QuestionData { kanji = "Ì‚¦‚é", answers = new string[] { "‚½‚½‚¦‚é" }},
            new QuestionData { kanji = "Õ‚é", answers = new string[] { "‚³‚¦‚¬‚é" }},
            new QuestionData { kanji = "‘£‚·", answers = new string[] { "‚¤‚È‚ª‚·" }},
            new QuestionData { kanji = "Œp‚®", answers = new string[] { "‚Â‚®" }},
            new QuestionData { kanji = "‰ú‚ß", answers = new string[] { "‚¢‚Ü‚µ‚ß" }},
            new QuestionData { kanji = "Œb‚Ş", answers = new string[] { "‚ß‚®‚Ş" }},
            new QuestionData { kanji = "‹Y‚ê‚é", answers = new string[] { "‚½‚í‚Ş‚ê‚é" }},
            new QuestionData { kanji = "ŠÓ‚İ‚é", answers = new string[] { "‚©‚ñ‚ª‚İ‚é" }},

            // Œ`—eŒ
            new QuestionData { kanji = "™R‚¢", answers = new string[] { "‚Í‚©‚È‚¢" }},
            new QuestionData { kanji = "Æ‚¢", answers = new string[] { "‚à‚ë‚¢" }},
            new QuestionData { kanji = "‰Ø‚â‚©", answers = new string[] { "‚Í‚È‚â‚©" }},
            new QuestionData { kanji = "ŠŠ‚ç‚©", answers = new string[] { "‚È‚ß‚ç‚©" }},
            new QuestionData { kanji = "ê‚ç", answers = new string[] { "‚à‚Á‚Ï‚ç" }},

            // “Áê‚È“Ç‚İ•û‚Ì’n–¼
            new QuestionData { kanji = "Îë", answers = new string[] { "‚¢‚µ‚©‚è" }},
            new QuestionData { kanji = "³“¹ŒÎ", answers = new string[] { "‚µ‚ñ‚¶‚±" }},
            new QuestionData { kanji = "’W˜H“‡", answers = new string[] { "‚ ‚í‚¶‚µ‚Ü" }},
            new QuestionData { kanji = "…‹›ì", answers = new string[] { "‚¢‚Æ‚¢‚ª‚í" }},
            new QuestionData { kanji = "ªº", answers = new string[] { "‚Ë‚Ş‚ë" }},

            // ˆÌl‚Ì–¼‘O
            new QuestionData { kanji = "D“cM’·", answers = new string[] { "‚¨‚¾‚Ì‚Ô‚È‚ª" }},
            new QuestionData { kanji = "¼‹½—²·", answers = new string[] { "‚³‚¢‚²‚¤‚½‚©‚à‚è" }},
            new QuestionData { kanji = "“¿ì‰ÆN", answers = new string[] { "‚Æ‚­‚ª‚í‚¢‚¦‚â‚·" }},
            new QuestionData { kanji = "¹“¿‘¾q", answers = new string[] { "‚µ‚å‚¤‚Æ‚­‚½‚¢‚µ" }},
            new QuestionData { kanji = "•“cMŒº", answers = new string[] { "‚½‚¯‚¾‚µ‚ñ‚°‚ñ" }}
        };


        return datas[Random.Range(0, datas.Length)];
    }
}
