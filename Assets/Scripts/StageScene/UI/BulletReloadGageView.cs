using System;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace StageUI
{
    public class BulletReloadGageView : MonoBehaviour
    {
        [Header("ChargeImage")]
        [SerializeField] Image image;
        [Header("RatioToColor")]
        [SerializeField] Gradient gradient;
        [Header("チャージ後表示時間")]
        [SerializeField] float displayTime;
        [SerializeField] Animator anim;

        CancellationTokenSource cts;

        private void Start()
        {
            image.gameObject.SetActive(false);
        }

        /// <summary>
        /// リロード値が変わったときのメソッド
        /// </summary>
        /// <param name="value"></param>
        public void OnChangeReloadValue(float value)
        {
            image.gameObject.SetActive(true);
            image.fillAmount = value;
            image.color = gradient.Evaluate(value);

            //フェードアウト
            if (value >= 1f)
            {
                cts = new CancellationTokenSource();
                DelayAction(displayTime, SetTrigger, "Fadeout", cts.Token).Forget();
            }
        }

        /// <summary>
        /// 遅延
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="callback"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async UniTask DelayAction<T>(float seconds, Action<T> callback, T arg, CancellationToken cancellationToken)
        {
            try
            {
                // 指定された秒数だけ待機（キャンセル可能）
                await UniTask.Delay(TimeSpan.FromSeconds(seconds), cancellationToken: cancellationToken);

                // キャンセルされていなければコールバック関数を実行
                if (!cancellationToken.IsCancellationRequested)
                {
                    callback?.Invoke(arg);
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("DelayAction was canceled.");
            }
        }

        private void SetTrigger(string name)
        {
            anim.SetTrigger(name);
        }

        public void SetDeactive()
        {
            image.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            // キャンセルしてリソースを解放
            cts?.Cancel();
            cts?.Dispose();
        }

        private void OnDisable()
        {
            // キャンセルしてリソースを解放
            cts?.Cancel();
            cts?.Dispose();
            cts = null;
        }
    }

}
