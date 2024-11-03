using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;

public class WaveStartTransition : IStagePhaseTransitioner
{
    WaveManager executeWaveManager;

    public WaveStartTransition(WaveManager waveManager)
    {
        executeWaveManager = waveManager;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        Debug.Log("�ySystem�zWave�J�n");
       
        //WAVE�I�����̃R�[���o�b�N���w��
        bool isEndOfWave = false;
        executeWaveManager?.OnEndWaveAsObservable
             .Subscribe(_ => { isEndOfWave = true; })
             .AddTo(token);

        //�X�^�[�g������
        executeWaveManager?.StartWave();

        //�E�F�[�u�I���܂ő҂�
        await UniTask.WaitUntil(() => isEndOfWave, PlayerLoopTiming.Update, token);
    }
}