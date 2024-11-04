using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;

public class StageFailedStartTransition : IStagePhaseTransitioner
{
    public StageFailedStartTransition()
    {
       
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        Debug.Log("�ySystem�zGameOver�����J�n");
        
        //�X�R�A�L�^
        ScoreManager.Instance.RecordSurvivalTimeScoreForUnityRoom();
        //BGM�̃t�F�[�h�A�E�g
        Sound.SoundManager.Instance.StopBGM(true);
        StageManager.Instance.ChangeStageStatus(StageStatus.StageFinish);

        await UniTask.Delay(1, cancellationToken: token);
    }
}
