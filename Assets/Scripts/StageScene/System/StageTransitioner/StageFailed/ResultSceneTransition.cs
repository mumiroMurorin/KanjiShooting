using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Playables;
using System;

public class ResultSceneTransition : IPhaseTransitioner
{
    const string RESULT_SCENE_NAME = "ResultScene";

    PlayableDirector sceneChangeDirector;
    AsyncOperation changeSceneAcync;

    public ResultSceneTransition(PlayableDirector director)
    {
        sceneChangeDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        // ���C���X���b�h�ɖ߂�
        await UniTask.SwitchToMainThread();

        // �I�y���[�V�����̓o�^
        changeSceneAcync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(RESULT_SCENE_NAME);
        changeSceneAcync.allowSceneActivation = false;

        try
        {
            // �V�[���̓ǂݍ���
            // �Ȃ�ƃV�[���̃��[�h�̓��C���X���b�h�ȊO�ł͍s���Ȃ�
            LoadResultScene(changeSceneAcync, token).Forget();
        }
        // ��O����
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        // �V�[���`�F���W�J�n
        if (sceneChangeDirector != null)
        {
            sceneChangeDirector.Play();

            // �V�[���`�F���W���o�I���܂ő҂�
            await UniTask.WaitUntil(() => sceneChangeDirector.state != PlayState.Playing, cancellationToken: token);
        }

        changeSceneAcync.allowSceneActivation = true;

        Debug.Log("�ySystem�z�V�[���`�F���W���o�I��");
    }

    /// <summary>
    /// ���U���g�V�[���̓ǂݍ���
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTaskVoid LoadResultScene(AsyncOperation sceneChangeOperation, CancellationToken token)
    {
        if (token.IsCancellationRequested) { throw new Exception("�V�[���ǂݍ��݂����f����܂���"); }

        // �ǂݍ��݂̊J�n
        Debug.Log("�ySystem�z���U���g�V�[���ǂݍ��݊J�n");
        await sceneChangeOperation;

        Debug.Log("�ySystem�z���U���g�V�[���ǂݍ��݊���");
    }
}
