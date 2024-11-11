using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StageTransition;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] SerializeInterface<IStatus> status;

    /// <summary>
    /// HP‚ª•Ï‚í‚Á‚½‚Æ‚«‚Ìˆ—
    /// </summary>
    /// <param name="hpNormalized"></param>
    public void OnChangeHPNormalized(float hpNormalized)
    {
        if(hpNormalized <= 0) { StageManager.Instance.ChangeStageScene(StageSceneTag.StageFailed); }
    }
}
