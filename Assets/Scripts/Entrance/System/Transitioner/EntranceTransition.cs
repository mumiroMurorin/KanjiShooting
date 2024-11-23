using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace EntranceTransition
{
    public enum MenuStatus
    {
        //GameDataLoad,
        Title,
        MainMenu,
        StageSelect,
        Option,
        Sortie
    }

    /// <summary>
    /// �X�e�[�W�V�[�����t�F�[�Y�̑J�ڂ�S���N���X
    /// </summary>
    public class MenuTransitionManager
    {
        Dictionary<MenuStatus, IPhaseTransitioner> transitions = new Dictionary<MenuStatus, IPhaseTransitioner>();

        /// <summary>
        /// �J�ڏ����̒ǉ�
        /// </summary>
        /// <param name="transitioner"></param>
        public void AddTransition(MenuStatus menuStatus, IPhaseTransitioner transitioner)
        {
            transitions.Add(menuStatus, transitioner);
        }

        /// <summary>
        /// �V�[�P���X�̎��s
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async UniTask ExecuteAsync(MenuStatus menuStatus, CancellationToken cancellationToken)
        {
            if(!transitions.TryGetValue(menuStatus,out IPhaseTransitioner phaseTransitioner))
            {
                Debug.LogError($"�ySystem�z�X�e�[�^�X {menuStatus}�͓o�^����Ă��܂���");
                return;
            }

            Debug.Log($"�ySystem�z���j���[�ړ��J�n: {menuStatus}");
            await phaseTransitioner.ExecuteAsync(cancellationToken);
            Debug.Log($"�ySystem�z���j���[�ړ��I��: {menuStatus}");
        }
    }

    public interface IEntranceUIcontroller
    {
        public void ActiveUIGroup(MenuStatus status);
    }
}