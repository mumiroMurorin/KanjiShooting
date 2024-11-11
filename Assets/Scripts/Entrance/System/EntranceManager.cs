using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EntranceTransition;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;

public class EntranceManager : LocalSingletonMonoBehaviour<EntranceManager>
{
    const MenuStatus FIRST_MENU_STATUS = MenuStatus.GameDataLoad;

    MenuTransitionManager menuTransitionManager = new MenuTransitionManager();
    ReactiveProperty<MenuStatus> currentStatus = new ReactiveProperty<MenuStatus>();
    CancellationTokenSource cts;

    private new void Awake()
    {
        Initialize();
        Bind();
        SetMenuStatus(FIRST_MENU_STATUS);
    }

    private void Initialize()
    {
        cts = new CancellationTokenSource();
    }

    private void Bind()
    {
        // �X�e�[�^�X�ύX �� ��ʑJ��
        currentStatus.Subscribe(status =>
            {
                menuTransitionManager.ExecuteAsync(status, cts.Token).Forget();
            })
            .AddTo(this.gameObject);
    }

    /// <summary>
    /// ���j���[�X�e�[�^�X�̕ύX(���j���[��ʂ̑J��)
    /// </summary>
    /// <param name="status"></param>
    public void SetMenuStatus(MenuStatus status)
    {
        currentStatus.Value = status;
    }

}
