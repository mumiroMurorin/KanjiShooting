using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kanji;

[CreateAssetMenu(menuName = "ScriptableObject/Question Filter", fileName = "QuestionFilter")]
public class QuestionFilter : ScriptableObject
{
    [SerializeField] Level kanjiLevel = Level.Everything;
    public Level KanjiLevel { get { return kanjiLevel; } }

    [SerializeField] Radical kanjiRadical = Radical.Everything;
    public Radical KanjiRadical { get { return kanjiRadical; } }

    [SerializeField] OtherTag kanjiOtherTag = OtherTag.Everything; 
    public OtherTag KanjiOtherTag { get { return kanjiOtherTag; } }

    /// <summary>
    /// î‰är
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool Compare(QuestionData data)
    {
        if((data.level & this.kanjiLevel) == 0) { return false; }
        if((data.radical & this.kanjiRadical) == 0) { return false; }
        if((data.otherTag & this.kanjiOtherTag) == 0) { return false; }

        return true;
    }
}
