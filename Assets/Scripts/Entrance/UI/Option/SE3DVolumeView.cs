using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace EntranceUI
{
    public class SE3DVolumeView : MonoBehaviour
    {
        [SerializeField] Slider seSlider;
        [SerializeField] TextMeshProUGUI volumeText;
        [SerializeField] AudioSource sampleSESource;
        [SerializeField] AudioClip sampleClip;
        [SerializeField] float sampleSE_interval = 0.2f;

        public Action<float> OnChangeValueListener;

        CancellationTokenSource cts = new CancellationTokenSource();
        bool isPlayableSE = true;

        /// <summary>
        /// SEボリュームが変わったときのメソッド
        /// </summary>
        /// <param name="value"></param>
        public void OnSE3DVolumeChanged(float value)
        {
            seSlider.value = value;
            volumeText.text = (value * 10).ToString("0.00");

            // サンプルSEの再生
            PlaySE();

            // 〇秒後にSE再生の許可
            DelayAction(PermitPlaySE, sampleSE_interval, cts.Token).Forget();
        }

        /// <summary>
        /// SEスライダーが変えられたときのメソッド
        /// </summary>
        public void OnSE3DVolumeSliderChanged(float value)
        {
            if (OnChangeValueListener != null)
            {
                OnChangeValueListener.Invoke(value);
            }
        }

        /// <summary>
        /// サンプルSEの再生
        /// </summary>
        private void PlaySE()
        {
            if (!isPlayableSE) { return; }
            if (!sampleClip) { return; }
            if (!sampleSESource) { return; }

            sampleSESource.PlayOneShot(sampleClip);
            isPlayableSE = false;
        }
        
        private void PermitPlaySE()
        {
            isPlayableSE = true;
        }

        private async UniTaskVoid DelayAction(Action callBack, float duration, CancellationToken token)
        {
            await UniTask.WaitForSeconds(duration, cancellationToken: token);
            callBack.Invoke();
        }
    }
}
