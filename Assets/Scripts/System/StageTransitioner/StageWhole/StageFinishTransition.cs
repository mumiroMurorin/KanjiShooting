using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Playables;

public class StageFinishTransition : IStagePhaseTransitioner
{
    public StageFinishTransition()
    {
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        //���R�[�h�̋L�^
        ScoreManager.Instance.RecordSurvivalTimeScoreForUnityRoom();

        //�X�e�[�W�X�e�[�^�X�̕ύX
        StageManager.Instance.ChangeStageStatus(StageStatus.StageFinish);

        //�X�e�[�W�I���܂ő҂�
        await UniTask.WaitForSeconds(0.1f, cancellationToken: token);
        Debug.Log("�ySystem�z�X�e�[�W�I��");
    }
}
