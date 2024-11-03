using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;

public class WaveFinishTransition : IStagePhaseTransitioner
{
    WaveManager executeWaveManager;

    public WaveFinishTransition(WaveManager waveManager)
    {
        executeWaveManager = waveManager;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        //WAVE�I�������������̃R�[���o�b�N���w��
        bool isEndOfWaveFinish = false;
        executeWaveManager?.OnEndWaveFinishingAsObservable
             .Subscribe(_ => { isEndOfWaveFinish = true; })
             .AddTo(token);

        //�I��������
        executeWaveManager?.FinishWave();

        //WAVE�I�����������܂ő҂�
        await UniTask.WaitUntil(() => isEndOfWaveFinish, PlayerLoopTiming.Update, token);
        Debug.Log("�ySystem�zWave�I��");
    }
}