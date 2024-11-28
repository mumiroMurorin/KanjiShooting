using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StageTransition;
using UniRx;

public class StageUIController : MonoBehaviour , IStageUIcontroller
{
    [System.Serializable]
    class UIGroup
    {
        [SerializeField] StageStatus status;
        [SerializeField] GameObject uiObject;

        public StageStatus Status { get { return status; } }

        public void SetActiveUI(bool isActive)
        {
            if (uiObject != null) { uiObject.SetActive(isActive); }
        }
    }

    [SerializeField] List<UIGroup> uiGroups;

    /// <summary>
    /// 該当UIグループを表示、それ以外は非表示する
    /// </summary>
    /// <param name="status"></param>
    public void ActiveUIGroup(StageStatus status)
    {
        foreach(UIGroup ui in uiGroups)
        {
            ui.SetActiveUI(ui.Status == status);
        }
    }

    private void Start()
    {
        Bind();
    }

    private void Bind()
    {
        StageManager.Instance.CurrentStageStatusreactiveproperty
            .Subscribe(ActiveUIGroup)
            .AddTo(this.gameObject);
    }
}