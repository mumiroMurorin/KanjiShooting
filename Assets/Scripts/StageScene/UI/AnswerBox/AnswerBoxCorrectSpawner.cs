using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StageUI
{
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

}
