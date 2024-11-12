using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EntranceTransition;

public class EntranceUIController : MonoBehaviour , IEntranceUIcontroller
{
    [System.Serializable]
    class UIGroup
    {
        [SerializeField] MenuStatus status;
        [SerializeField] GameObject uiObject;

        public MenuStatus Status { get { return status; } }

        public void SetActiveUI(bool isActive)
        {
            if (uiObject != null) { uiObject.SetActive(isActive); }
        }
    }

    [SerializeField] List<UIGroup> uiGroups;

    /// <summary>
    /// �Y��UI�O���[�v��\���A����ȊO�͔�\������
    /// </summary>
    /// <param name="status"></param>
    public void ActiveUIGroup(MenuStatus status)
    {
        foreach(UIGroup ui in uiGroups)
        {
            ui.SetActiveUI(ui.Status == status);
        }
    }
}

public interface IEntranceUIcontroller
{
    public void ActiveUIGroup(MenuStatus status);
}