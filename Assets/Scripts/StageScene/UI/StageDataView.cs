using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace StageUI
{
    public class StageDataView : MonoBehaviour
    {
        [Header("StageTitleText")]
        [SerializeField] TextMeshProUGUI stageTitleTmp;

        /// <summary>
        /// ステージ情報が変わったときの処理
        /// </summary>
        /// <param name="number"></param>
        public void OnChangeStageData(StageDetailData data)
        {
            if (data == null) { return; }

            stageTitleTmp.text = data.StageTitle;
        }
    }

}
