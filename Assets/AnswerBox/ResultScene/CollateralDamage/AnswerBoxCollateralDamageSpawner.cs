using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResultUI
{
    public class AnswerBoxCollateralDamageSpawner : AnswerBoxSpawner
    {
        public override bool CanSpawn(AnswerStatus answerStatus)
        {
            return answerStatus.state == AnswerState.CollateralDamage;
        }

        protected override void AfterSpawn()
        {

        }
    }

}
