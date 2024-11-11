using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class WaveStartEffectTransition : IPhaseTransitioner
{
    PlayableDirector waveStartDirector;

    public WaveStartEffectTransition(PlayableDirector director)
    {
        waveStartDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        //Wave�J�E���g���X�V
        ScoreManager.Instance.IncrementWaveCount();

        //�E�F�[�u�J�n���o
        waveStartDirector.Play();

        //�E�F�[�u�J�n���o�I���܂ő҂�
        await UniTask.WaitUntil(() => waveStartDirector.state != PlayState.Playing, cancellationToken: token);
        Debug.Log("�ySystem�z�E�F�[�u�J�n���o�I��");
    }
}