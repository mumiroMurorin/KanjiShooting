using System;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

public class BulletReloadGageView : MonoBehaviour
{
    [Header("ChargeImage")]
    [SerializeField] Image image;
    [Header("RatioToColor")]
    [SerializeField] Gradient gradient;
    [Header("�`���[�W��\������")]
    [SerializeField] float displayTime;
    [SerializeField] Animator anim;

    CancellationTokenSource cts;

    private void Start()
    {
        image.gameObject.SetActive(false);
    }

    /// <summary>
    /// �����[�h�l���ς�����Ƃ��̃��\�b�h
    /// </summary>
    /// <param name="value"></param>
    public void OnChangeReloadValue(float value)
    {
        image.gameObject.SetActive(true);
        image.fillAmount = value;
        image.color = gradient.Evaluate(value);

        //�t�F�[�h�A�E�g
        if(value >= 1f)
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
        image.gameObject.SetActive(false);
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