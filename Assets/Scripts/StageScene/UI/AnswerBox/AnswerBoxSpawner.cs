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
        public abstract bool CanSpawn(AnswerStatus answerStatus);

        /// <summary>
        /// 回答UIの生成
        /// </summary>
        /// <param name="answerState"></param>
        /// <returns></returns>
        public AnswerBoxView Spawn(AnswerStatus answerStatus)
        {
            //インスタンス化
            GameObject obj = Instantiate(answerBoxPrefab, answerBoxParent);
            AnswerBoxView answerBoxView = obj.GetComponent<AnswerBoxView>();
            answerBoxView.SetAnswer(answerStatus.questionData);

            return answerBoxView;
        }

        protected abstract void AfterSpawn();
    }

}