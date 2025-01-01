using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EntranceTransition;

public class EntranceUIController : MonoBehaviour , IEntranceUIController
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
    /// 該当UIグループを表示、それ以外は非表示する
    /// </summary>
    /// <param name="status"></param>
    public void ActiveUIGroup(MenuStatus status)
    {
        // 一旦全部非表示
        foreach(UIGroup ui in uiGroups)
        {
            ui.SetActiveUI(false);
        }

        // 該当UIのみ表示
        foreach (UIGroup ui in uiGroups)
        {
            if(ui.Status == status) { ui.SetActiveUI(true); }
        }
    }
}