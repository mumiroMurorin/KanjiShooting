using System;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace StageUI
{
    public class ExplosionChargeGageView : MonoBehaviour
    {
        [Header("ImageMask")]
        [SerializeField] Image imageMask;
        [Header("ChargeImage")]
        [SerializeField] Image chargeImage;
        [Header("RatioToColor")]
        [SerializeField] Gradient gradient;
        [Header("�\������")]
        [SerializeField] float displayTime;
        [SerializeField] Animator anim;

        CancellationTokenSource cts;

        private void Start()
        {
            this.gameObject.SetActive(false);
        }

        /// <summary>
        /// �`���[�W�l���ς�����Ƃ��̃��\�b�h
        /// </summary>
        /// <param name="value"></param>
        public void OnChangeChargeValue(float value)
        {
            this.gameObject.SetActive(true);
            imageMask.gameObject.SetActive(true);
            imageMask.fillAmount = value;
            chargeImage.color = gradient.Evaluate(value);

            // 0�ɂȂ�����Q�[�W������
            // (���������L�����Z����)
            if(value == 0f) 
            {
                cts = new CancellationTokenSource();
                DelayAction(displayTime, SetTrigger, "Fadeout", cts.Token).Forget();
            }
        }

        /// <summary>
        /// �x��
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="callback"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async UniTask DelayAction<T>(float seconds, Action<T> callback, T arg, CancellationToken cancellationToken)
        {
            try
            {
                // �w�肳�ꂽ�b�������ҋ@�i�L�����Z���\�j
                await UniTask.Delay(TimeSpan.FromSeconds(seconds), cancellationToken: cancellationToken);

                // �L�����Z������Ă��Ȃ���΃R�[���o�b�N�֐������s
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
            this.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            // �L�����Z�����ă��\�[�X�����
            cts?.Cancel();
            cts?.Dispose();
        }

        private void OnDisable()
        {
            // �L�����Z�����ă��\�[�X�����
            cts?.Cancel();
            cts?.Dispose();
            cts = null;
        }
    }

}
