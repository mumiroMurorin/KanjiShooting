using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Playables;
using System;
using UniRx;

namespace EntranceTransition
{
    public class MainSceneTransition : IPhaseTransitioner
    {
        IEntranceUIController entranceUIController;
        IReadOnlyReactiveProperty<StageDetailData> stageTransitionData;
        PlayableDirector sortieDirector;
        AsyncOperation changeSceneAcync;

        public MainSceneTransition(IEntranceUIController uiController, PlayableDirector director, IReadOnlyReactiveProperty<StageDetailData> stageData)
        {
            entranceUIController = uiController;
            sortieDirector = director;
            stageTransitionData = stageData;
        }

        public async UniTask ExecuteAsync(CancellationToken token)
        {
            if (stageTransitionData == null) { return; }

            // BGM�̒�~
            Sound.SoundManager.Instance.StopBGM(true);

            // ���C���X���b�h�ɖ߂�
            await UniTask.SwitchToMainThread();
            // �I�y���[�V�����̓o�^
            changeSceneAcync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(stageTransitionData.Value.SceneName);
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
            entranceUIController.ActiveUIGroup(MenuStatus.Sortie);
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

}
