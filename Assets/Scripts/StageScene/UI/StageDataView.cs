using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageDataView : MonoBehaviour
{
    [Header("StageTitleText")]
    [SerializeField] TextMeshProUGUI stageTitleTmp;

    /// <summary>
    /// �X�e�[�W��񂪕ς�����Ƃ��̏���
    /// </summary>
    /// <param name="number"></param>
    public void OnChangeStageData(StageDetailData data)
    {
        stageTitleTmp.text = data.StageTitle;
    }
}
