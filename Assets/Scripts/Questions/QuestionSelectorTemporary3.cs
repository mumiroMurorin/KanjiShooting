using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class QuestionSelectorTemporary3 : MonoBehaviour, IQuestionSelector
{
    public QuestionData GetQuestionData(QuestionFilter filter) 
    {
        QuestionData[] datas = new QuestionData[]
        {
            // ˆê•¶šŠ¿š
            new QuestionData { kanji = "‹d", answers = new string[] { "‚«‚­" }},
            new QuestionData { kanji = "–", answers = new string[] { "‚Ü‚®‚ë","‚µ‚Ñ" }},
            new QuestionData { kanji = "‰", answers = new string[] { "‚Â‚Î‚ß","‚³‚©‚à‚è" }},
            new QuestionData { kanji = "å¢", answers = new string[] { "‚Í‚¦" }},
            new QuestionData { kanji = "æ¬", answers = new string[] { "‚±‚¾‚Ü","‚â‚Ü‚Ñ‚±" }},
            new QuestionData { kanji = "—ø", answers = new string[] { "‚³‚´‚È‚İ" }},


            // “ñšnŒê
            new QuestionData { kanji = "’cî", answers = new string[] { "‚¤‚¿‚í" }},
            new QuestionData { kanji = "œ¾œç", answers = new string[] { "‚¢‚ñ‚¬‚ñ" }},
            new QuestionData { kanji = "œ’›", answers = new string[] { "‚±‚¤‚±‚Â" }},
            new QuestionData { kanji = "œï‘Ä", answers = new string[] { "‚ç‚ñ‚¾","‚ç‚¢‚¾" }},
            new QuestionData { kanji = "–úX", answers = new string[] { "‚¢‚æ‚¢‚æ" }},
            new QuestionData { kanji = "™G‰z", answers = new string[] { "‚¹‚ñ‚¦‚Â" }},
            new QuestionData { kanji = "”ë–Æ", answers = new string[] { "‚Ğ‚ß‚ñ" }},
            new QuestionData { kanji = "“¥P", answers = new string[] { "‚Æ‚¤‚µ‚ã‚¤" }},
            new QuestionData { kanji = "‘a‚¢", answers = new string[] { "‚¤‚Æ‚¢" }},
            new QuestionData { kanji = "ŠÓ‚İ‚é", answers = new string[] { "‚©‚ñ‚ª‚İ‚é" }},
            new QuestionData { kanji = "“K‹X", answers = new string[] { "‚Ä‚«‚¬" }},
            new QuestionData { kanji = "ˆâŠ¶", answers = new string[] { "‚¢‚©‚ñ" }},
            new QuestionData { kanji = "^•", answers = new string[] { "‚µ‚ñ‚µ" }},
            new QuestionData { kanji = "Œœ”O", answers = new string[] { "‚¯‚Ë‚ñ" }},
            new QuestionData { kanji = "i’»", answers = new string[] { "‚µ‚ñ‚¿‚å‚­" }},
            new QuestionData { kanji = "Šˆà", answers = new string[] { "‚¢‚í‚ä‚é" }},
            new QuestionData { kanji = "æ]•ñ", answers = new string[] { "‚Ó‚Ù‚¤" }},
            new QuestionData { kanji = "‘ã‘Ö", answers = new string[] { "‚¾‚¢‚½‚¢" }},
            new QuestionData { kanji = "{H", answers = new string[] { "‚¹‚±‚¤" }},
            new QuestionData { kanji = "NW", answers = new string[] { "‚µ‚ã‚¤‚µ‚ã‚¤" }},
            new QuestionData { kanji = "˜M‚é", answers = new string[] { "‚¢‚¶‚é" }},
            new QuestionData { kanji = "…ç", answers = new string[] { "‚¶‚ã‚ñ‚µ‚ã" }},
            new QuestionData { kanji = "¦´", answers = new string[] { "‚µ‚³" }},
            new QuestionData { kanji = "À‹", answers = new string[] { "‚¹‚¢‚«‚å" }},
            new QuestionData { kanji = "àêár", answers = new string[] { "‚©‚µ" }},
            new QuestionData { kanji = "˜ëáÕ", answers = new string[] { "‚Ó‚©‚ñ" }},
            new QuestionData { kanji = "Ù‘¬", answers = new string[] { "‚¹‚Á‚»‚­" }},
            new QuestionData { kanji = "”ÏG", answers = new string[] { "‚Í‚ñ‚´‚Â" }},
            new QuestionData { kanji = "“Q", answers = new string[] { "‚í‚¾‚¿","‚Ä‚Â" }},
            new QuestionData { kanji = "ˆ‰", answers = new string[] { "‚â‚ä" }},
            new QuestionData { kanji = "Œ[–Ö", answers = new string[] { "‚¯‚¢‚à‚¤" }},
            new QuestionData { kanji = "ˆ´ù", answers = new string[] { "‚ ‚Á‚¹‚ñ" }},
            new QuestionData { kanji = "ãÅ‰", answers = new string[] { "‚Ğ‚·‚¢","‚©‚í‚¹‚İ" }},
            new QuestionData { kanji = "œÔšL", answers = new string[] { "‚Ç‚¤‚±‚­" }},
            new QuestionData { kanji = "æk•Ù", answers = new string[] { "‚«‚×‚ñ" }},
            new QuestionData { kanji = "‹\áÔ", answers = new string[] { "‚¬‚Ü‚ñ" }},
            new QuestionData { kanji = "™FŒô", answers = new string[] { "‚¬‚å‚¤‚±‚¤" }},
            new QuestionData { kanji = "—ãs", answers = new string[] { "‚ê‚¢‚±‚¤" }},
            new QuestionData { kanji = "–¢‘\—L", answers = new string[] { "‚İ‚¼‚¤" }},
            new QuestionData { kanji = "àÂàÏ", answers = new string[] { "‚±‚¤‚©‚Â" }},
            new QuestionData { kanji = "Œ}‡", answers = new string[] { "‚°‚¢‚²‚¤" }},
            new QuestionData { kanji = "éÀ", answers = new string[] { "‚Í‚º" }},
            new QuestionData { kanji = "çaç€", answers = new string[] { "‚ ‚Â‚ê‚«" }},
            new QuestionData { kanji = "‘Î›³", answers = new string[] { "‚½‚¢‚¶" }},
            new QuestionData { kanji = "Ià", answers = new string[] { "‚µ‚ã‚¤‚¦‚ñ" }},
            new QuestionData { kanji = "è‡’l", answers = new string[] { "‚¢‚«‚¿","‚µ‚«‚¢‚¿" }},
            new QuestionData { kanji = "‰b’q", answers = new string[] { "‚¦‚¢‚¿" }},
            new QuestionData { kanji = "Œë•T", answers = new string[] { "‚²‚Ñ‚ã‚¤" }},
            new QuestionData { kanji = "Š@", answers = new string[] { "‚³‚«‚ª‚¯" }},
            new QuestionData { kanji = "“f˜I", answers = new string[] { "‚Æ‚ë" }},
            new QuestionData { kanji = "áà", answers = new string[] { "‚«‚å‚¤‚¶" }},
            new QuestionData { kanji = "’¢–â", answers = new string[] { "‚¿‚å‚¤‚à‚ñ" }},
            new QuestionData { kanji = "NO", answers = new string[] { "‚à‚¤‚ë‚¤" }},
            new QuestionData { kanji = "œÜœª", answers = new string[] { "‚µ‚å‚¤‚·‚¢" }},
            new QuestionData { kanji = "êŸ", answers = new string[] { "‚µ‚ã‚µ‚å‚¤" }},
            new QuestionData { kanji = "æÜß", answers = new string[] { "‚µ‚å‚­‚´‚¢" }},
            new QuestionData { kanji = "œa¯", answers = new string[] { "‚·‚¢‚¹‚¢" }},
            new QuestionData { kanji = "ç•„", answers = new string[] { "‚µ‚ã‚ñ‚¶‚ã‚ñ" }},
            new QuestionData { kanji = "–â", answers = new string[] { "‚µ‚à‚ñ" }},
            new QuestionData { kanji = "”½ä", answers = new string[] { "‚Í‚ñ‚·‚¤" }},

            // O•¶šnŒê
            new QuestionData { kanji = "–A–—–²", answers = new string[] { "‚Ù‚¤‚Ü‚Â‚Ş" }},
            new QuestionData { kanji = "ˆâŠü•¨", answers = new string[] { "‚¢‚«‚Ô‚Â" }},
            new QuestionData { kanji = "‰vr’j", answers = new string[] { "‚Ü‚·‚ç‚¨" }},
            new QuestionData { kanji = "”@‰½—l", answers = new string[] { "‚¢‚©‚³‚Ü" }},
            new QuestionData { kanji = "”÷‰·“’", answers = new string[] { "‚Ê‚é‚Ü‚ä" }},


            // lšnŒê
            new QuestionData { kanji = "Œ–XšX", answers = new string[] { "‚¯‚ñ‚¯‚ñ‚²‚¤‚²‚¤" }},
            new QuestionData { kanji = "Š¤Š¤æ€æ€", answers = new string[] { "‚©‚ñ‚©‚ñ‚ª‚­‚ª‚­" }},
            new QuestionData { kanji = "•sš•s‹ü", answers = new string[] { "‚Ó‚Æ‚¤‚Ó‚­‚Â" }},
            new QuestionData { kanji = "”ßŠìŒğŒğ", answers = new string[] { "‚Ğ‚«‚±‚à‚²‚à" }},
            new QuestionData { kanji = "é³–£é±é²", answers = new string[] { "‚¿‚İ‚à‚¤‚è‚å‚¤" }},

            // “®Œ
            new QuestionData { kanji = "D‚¦‚é", answers = new string[] { "‚¤‚ê‚¦‚é" }},
            new QuestionData { kanji = "º‚¤", answers = new string[] { "‚³‚ç‚¤" }},
            new QuestionData { kanji = "’H‚é", answers = new string[] { "‚½‚Ç‚é" }},
            new QuestionData { kanji = "’Ô‚é", answers = new string[] { "‚Â‚Ã‚é" }},
            new QuestionData { kanji = "•ò‚é", answers = new string[] { "‚½‚Ä‚Ü‚Â‚é","‚¤‚¯‚½‚Ü‚í‚é" }},

            // Œ`—eŒ
            new QuestionData { kanji = "œ¡‚é", answers = new string[] { "‚à‚Æ‚é" }},
            new QuestionData { kanji = "–F‚µ‚¢", answers = new string[] { "‚©‚®‚í‚µ‚¢","‚©‚ñ‚Î‚µ‚¢" }},
            new QuestionData { kanji = "é`ã", answers = new string[] { "‚¶‚å‚¤‚º‚Â" }},
            new QuestionData { kanji = "œ›‚Ü‚µ‚¢", answers = new string[] { "‚¨‚¼‚Ü‚µ‚¢" }},
            new QuestionData { kanji = "í‚µ‚¦", answers = new string[] { "‚Æ‚±‚µ‚¦" }},

        };


        return datas[Random.Range(0, datas.Length)];
    }
}
