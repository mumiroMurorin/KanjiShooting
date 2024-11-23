using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kanji;

namespace StageUI
{
    public abstract class AnswerBoxSpawner : MonoBehaviour
    {
        [SerializeField] GameObject answerBoxPrefab;
        [SerializeField] Transform answerBoxParent;

        /// <summary>
        /// スポーンさせる条件に合致しているか返す
        /// </summary>
        /// <param name="answerState"></param>
        /// <returns></returns>
        public abstract bool CanSpawn(AnswerState answerState);

        /// <summary>
        /// 回答UIの生成
        /// </summary>
        /// <param name="answerState"></param>
        /// <returns></returns>
        public AnswerBoxView Spawn(AnswerState answerState)
        {
            //インスタンス化
            GameObject obj = Instantiate(answerBoxPrefab, answerBoxParent);
            AnswerBoxView answerBoxView = obj.GetComponent<AnswerBoxView>();
            answerBoxView.SetAnswer(answerState.questionData);

            return answerBoxView;
        }

        protected abstract void AfterSpawn();
    }

}