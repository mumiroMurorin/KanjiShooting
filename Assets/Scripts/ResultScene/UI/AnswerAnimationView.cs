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
        /// リザルトフェーズに遷移したとき
        /// </summary>
        public void OnTransitionResult(AnswerStatus[] results)
        {
            cts = new CancellationTokenSource();

            SpawnAnswerAnimation(results, animationDuration, cts.Token).Forget();
        }

        /// <summary>
        /// リザルトをアニメーション形式で表示
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

            // アンサーボックスのスポーン
            foreach (AnswerStatus answer in results)
            {
                SpawnAnswerBox(answer);
                scrollrect.verticalNormalizedPosition = 0;
                await UniTask.WaitForSeconds(delayPerResult, cancellationToken: token);
            }

            scrollrect.verticalNormalizedPosition = 0;

            // 一番下まで行ったらちょっと待つ
            await UniTask.WaitForSeconds(waitTime, cancellationToken: token);

            // 一番上まで戻る
            scrollrect.DOVerticalNormalizedPos(1, backOfTopDuration)
                .OnComplete(() => { scrollrect.vertical = true; });
        }

        /// <summary>
        /// 条件に合ったスポナーでアンサーボックスをスポーンさせる
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
