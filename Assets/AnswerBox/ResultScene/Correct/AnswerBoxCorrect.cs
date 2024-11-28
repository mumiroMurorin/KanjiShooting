using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace ResultUI
{
    public class AnswerBoxCorrect : AnswerBoxView
    {
        [SerializeField] Animator anim;

        CancellationTokenSource cts;

        protected override void AfterSpawn()
        {
            cts = new CancellationTokenSource();
        }

        /// <summary>
        /// 遅れてコールバックを実行する関数
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="callback"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async UniTask DelayAction<T>(float duration, Action<T> callback, T arg, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: token);
            callback?.Invoke(arg);
        }

        private void SetTriggerAnimator(string name)
        {
            anim.SetTrigger(name);
        }

        private void OnDestroy()
        {
            cts?.Cancel();
            cts?.Dispose();
        }
    }

}
