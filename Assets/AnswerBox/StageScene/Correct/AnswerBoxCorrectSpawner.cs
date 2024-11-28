using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StageUI
{
    public class AnswerBoxCorrectSpawner : AnswerBoxSpawner
    {
        public override bool CanSpawn(AnswerStatus answerStatus)
        {
            return answerStatus.state == AnswerState.Corrected;
        }

        protected override void AfterSpawn()
        {

        }
    }

}
