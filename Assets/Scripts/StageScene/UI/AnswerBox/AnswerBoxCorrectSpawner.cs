using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerBoxCorrectSpawner : AnswerBoxSpawner
{
    public override bool CanSpawn(AnswerState answerState)
    {
        return answerState.isCorrect;
    }

    protected override void AfterSpawn()
    {

    }
}
