using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace StageTransition
{
    public enum StageSceneTag
    {
        Fighting,
        StageFailed
    }

    public enum StageStatus
    {
        Loading,
        Fighting,
        StageFinish,
    }

    /// <summary>
    /// �X�e�[�W���V�[���̊Ǘ��N���X
    /// </summary>
    public class StageSceneTransitionManager
    {
        readonly Dictionary<StageSceneTag, ScenePhaseTransitionManager> stagePhases = new Dictionary<StageSceneTag, ScenePhaseTransitionManager>();
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        /// <summary>
        /// �X�e�[�W���V�[���̒ǉ�
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="phaseTransitionManager"></param>
        public void AddPhaseTransitioner(StageSceneTag sceneTag, ScenePhaseTransitionManager phaseTransitionManager)
        {
            if (!stagePhases.TryAdd(sceneTag, phaseTransitionManager))
            {
                Debug.LogWarning($"�ySystem�zSceneTag:{sceneTag} �ɑΉ�����X�e�[�W���V�[���͊��ɓo�^����Ă��܂�");
                return;
            }
        }

        /// <summary>
        /// �V�[���̑J�ځA�L�����Z������
        /// </summary>
        /// <param name="sceneTag"></param>
        public void ExecuteStageScene(StageSceneTag sceneTag)
        {
            if (!stagePhases.TryGetValue(sceneTag, out ScenePhaseTransitionManager manager))
            {
                Debug.LogError($"�ySystem�z�X�e�[�W���V�[�����o�^����Ă��܂���: {sceneTag}");
                return;
            }

            //�X�e�[�W�V�[���̕ύX
            //���ݎ��s���̃V�[�P���X�𒆒f�A���Z�b�g
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            cancellationTokenSource = new CancellationTokenSource();

            manager.ExecuteAsync(cancellationTokenSource.Token).Forget();
        }

        public void OnDestroy()
        {
            cancellationTokenSource.Cancel();
        }
    }

    /// <summary>
    /// �X�e�[�W�V�[�����t�F�[�Y�̑J�ڂ�S���N���X
    /// </summary>
    public class ScenePhaseTransitionManager
    {
        readonly List<IPhaseTransitioner> transitions = new List<IPhaseTransitioner>();

        /// <summary>
        /// �J�ڏ����̒ǉ�
        /// </summary>
        /// <param name="transitioner"></param>
        public void AddTransition(IPhaseTransitioner transitioner)
        {
            transitions.Add(transitioner);
        }

        /// <summary>
        /// �V�[�P���X�̎��s
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async UniTask ExecuteAsync(CancellationToken cancellationToken)
        {
            Debug.Log("�ySystem�z�X�e�[�W���V�[���J�n");
            foreach (var transition in transitions)
            {
                await transition.ExecuteAsync(cancellationToken);
            }
            Debug.Log("�ySystem�z�X�e�[�W���V�[���I��");
        }
    }

    public interface IStageUIcontroller
    {
        public void ActiveUIGroup(StageStatus status);
    }
}