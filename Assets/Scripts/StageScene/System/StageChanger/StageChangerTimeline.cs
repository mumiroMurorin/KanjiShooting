using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.Threading;
using Cysharp.Threading.Tasks;

public class StageChangerTimeline : MonoBehaviour, IStageChanger
{
    [Header("ステージ遷移時のアニメーション")]
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
        // タイムラインスタート
        playableDirector.Play();
        // 再生終了まで待つ
        await UniTask.WaitUntil(() => playableDirector.state != PlayState.Playing, cancellationToken: token);

        // コールバック発火
        callback.Invoke();
    }

    private void OnDestroy()
    {
        cts.Cancel();
        cts.Dispose();
    }
}
