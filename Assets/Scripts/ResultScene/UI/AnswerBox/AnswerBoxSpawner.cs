using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ResultUI
{
    public abstract class AnswerBoxSpawner : MonoBehaviour
    {
        [SerializeField] GameObject answerBoxPrefab;
        [SerializeField] Transform answerBoxParent;

        /// <summary>
        /// �X�|�[������������ɍ��v���Ă��邩�Ԃ�
        /// </summary>
        /// <param name="answerState"></param>
        /// <returns></returns>
        public abstract bool CanSpawn(AnswerStatus answerState);

        /// <summary>
        /// ��UI�̐���
        /// </summary>
        /// <param name="answerState"></param>
        /// <returns></returns>
        public AnswerBoxView Spawn(AnswerStatus answerState)
        {
            //�C���X�^���X��
            GameObject obj = Instantiate(answerBoxPrefab, answerBoxParent);
            AnswerBoxView answerBoxView = obj.GetComponent<AnswerBoxView>();
            answerBoxView.SetAnswer(answerState.questionData);

            return answerBoxView;
        }

        protected abstract void AfterSpawn();
    }
}
