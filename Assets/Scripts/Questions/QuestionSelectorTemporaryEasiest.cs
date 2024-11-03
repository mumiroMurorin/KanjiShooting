using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class QuestionSelectorTemporaryEasiest : MonoBehaviour, IQuestionSelector
{
    public QuestionData GetQuestionData(QuestionFilter filter) 
    {
        QuestionData[] datas = new QuestionData[]
        {
            new QuestionData { kanji = "ê’ê—", answers = new string[] { "‚»‚²" }},
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
        };


        return datas[Random.Range(0, datas.Length)];
    }
}
