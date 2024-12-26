using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;

namespace ResultUI
{
    public class AnswerAnimationView : MonoBehaviour
    {
        [SerializeField] ScrollRect scrollrect;
        [SerializeField] AnswerBoxSpawner[] answerBoxSpawners;
        [SerializeField] float animationDuration = 2f;
        [SerializeField] float waitTime = 0.75f;
        [SerializeField] float backOfTopDuration = 0.75f;

        CancellationTokenSource cts;

        /// <summary>
        /// ���U���g�t�F�[�Y�ɑJ�ڂ����Ƃ�
        /// </summary>
        public void OnTransitionResult(AnswerStatus[] results)
        {
            cts = new CancellationTokenSource();

            SpawnAnswerAnimation(results, animationDuration, cts.Token).Forget();
        }

        /// <summary>
        /// ���U���g���A�j���[�V�����`���ŕ\��
        /// </summary>
        /// <param name="results"></param>
        /// <param name="duration"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async UniTaskVoid SpawnAnswerAnimation(AnswerStatus[] results, float duration, CancellationToken token)
        {
            int answerNum = results.Length;
            float delayPerResult = duration / answerNum;

            scrollrect.vertical = false;

            // �A���T�[�{�b�N�X�̃X�|�[��
            foreach (AnswerStatus answer in results)
            {
                SpawnAnswerBox(answer);
                scrollrect.verticalNormalizedPosition = 0;
                await UniTask.WaitForSeconds(delayPerResult, cancellationToken: token);
            }

            scrollrect.verticalNormalizedPosition = 0;

            // ��ԉ��܂ōs�����炿����Ƒ҂�
            await UniTask.WaitForSeconds(waitTime, cancellationToken: token);

            // ��ԏ�܂Ŗ߂�
            scrollrect.DOVerticalNormalizedPos(1, backOfTopDuration)
                .OnComplete(() => { scrollrect.vertical = true; });
        }

        /// <summary>
        /// �����ɍ������X�|�i�[�ŃA���T�[�{�b�N�X���X�|�[��������
        /// </summary>
        /// <param name="answerState"></param>
        private void SpawnAnswerBox(AnswerStatus answerStatus)
        {
            foreach (AnswerBoxSpawner a in answerBoxSpawners)
            {
                if (a.CanSpawn(answerStatus))
                {
                    a.Spawn(answerStatus);
                    break;
                }
            }
        }

        private void OnDestroy()
        {
            cts.Cancel();
            cts.Dispose();
        }

    }
}
