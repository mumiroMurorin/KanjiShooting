using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.Threading;
using Cysharp.Threading.Tasks;

public class StageChangerTimeline : MonoBehaviour, IStageChanger
{
    [Header("�X�e�[�W�J�ڎ��̃A�j���[�V����")]
    [SerializeField] PlayableDirector playableDirector;

    Action callback;
    CancellationTokenSource cts = new CancellationTokenSource();

    public void ChangeStage(Action callBackCompleted)
    {
        callback = callBackCompleted;
        PlayTimeline(cts.Token).Forget();
    }

    private async UniTaskVoid PlayTimeline(CancellationToken token)
    {
        // �^�C�����C���X�^�[�g
        playableDirector.Play();
        // �Đ��I���܂ő҂�
        await UniTask.WaitUntil(() => playableDirector.state != PlayState.Playing, cancellationToken: token);

        // �R�[���o�b�N����
        callback.Invoke();
    }

    private void OnDestroy()
    {
        cts.Cancel();
        cts.Dispose();
    }
}
