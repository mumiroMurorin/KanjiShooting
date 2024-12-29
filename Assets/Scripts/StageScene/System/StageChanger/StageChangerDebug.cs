using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChangerDebug : MonoBehaviour, IStageChanger
{
    [SerializeField] GameObject beforeStage;
    [SerializeField] GameObject afterStage;

    public void ChangeStage(Action callBackCompleted)
    {
        beforeStage.SetActive(false);
        afterStage.SetActive(true);
        callBackCompleted.Invoke();
    }
}
