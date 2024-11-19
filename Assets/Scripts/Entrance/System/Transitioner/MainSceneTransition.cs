using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using EntranceTransition;
using UnityEngine.Playables;
using System;


public class MainSceneTransition : IPhaseTransitioner
{
    const string MAIN_SCENE_NAME = "MainScene";

    IEntranceUIcontroller entranceUIcontroller;
    PlayableDirector sortieDirector;
    AsyncOperation changeSceneAcync;

    public MainSceneTransition(IEntranceUIcontroller uiController, PlayableDirector director)
    {
        entranceUIcontroller = uiController;
        sortieDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        // ���C���X���b�h�ɖ߂�
        await UniTask.SwitchToMainThread();
        // �I�y���[�V�����̓o�^
        changeSceneAcync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(MAIN_SCENE_NAME);
        changeSceneAcync.allowSceneActivation = false;

        try
        {
            // �V�[���̓ǂݍ���
            // �Ȃ�ƃV�[���̃��[�h�̓��C���X���b�h�ȊO�ł͍s���Ȃ�
            LoadMainScene(changeSceneAcync, token).Forget();
        }
        // ��O����
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        // �A�j���[�V�����̍Đ�
        entranceUIcontroller.ActiveUIGroup(MenuStatus.Sortie);
        if (sortieDirector != null)
        {
            sortieDirector.Play();

            // �o�����o�I���܂ő҂�
            await UniTask.WaitUntil(() => sortieDirector.state != PlayState.Playing, cancellationToken: token);
        }

        changeSceneAcync.allowSceneActivation = true;
    }

    /// <summary>
    /// ���C���V�[���̓ǂݍ���
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTaskVoid LoadMainScene(AsyncOperation sceneChangeOperation, CancellationToken token)
    {
        if (token.IsCancellationRequested) { throw new Exception("�V�[���ǂݍ��݂����f����܂���"); }

        // �ǂݍ��݂̊J�n
        Debug.Log("�ySystem�z���C���V�[���ǂݍ��݊J�n");
        await sceneChangeOperation;

        Debug.Log("�ySystem�z���C���V�[���ǂݍ��݊���");
    }
}
