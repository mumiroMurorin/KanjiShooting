using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using StageTransition;


public class StageFinishTransition : IPhaseTransitioner
{
    ScoreHolder scoreHolder;

    public StageFinishTransition(ScoreHolder holder)
    {
        scoreHolder = holder;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        // ���R�[�h�̋L�^
        scoreHolder.RecordSurvivalTimeScoreForUnityRoom();

        // �X�e�[�W�X�e�[�^�X�̕ύX
        StageManager.Instance.ChangeStageStatus(StageStatus.StageFinish);

        // �X�e�[�W�I���܂ő҂�
        await UniTask.WaitForSeconds(0.1f, cancellationToken: token);
        Debug.Log("�ySystem�z�X�e�[�W�I��");
    }
}
