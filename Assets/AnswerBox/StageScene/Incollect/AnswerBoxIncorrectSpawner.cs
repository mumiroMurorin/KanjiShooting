using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StageUI
{
    public class AnswerBoxIncorrectSpawner : AnswerBoxSpawner
    {
        public override bool CanSpawn(AnswerStatus answerStatus)
        {
            return answerStatus.state == AnswerState.Incorrected;
        }

        protected override void AfterSpawn()
        {

        }
    }

}
