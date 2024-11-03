using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] SerializeInterface<IRotatable> rotatable;
    [SerializeField] SerializeInterface<IStatus> status;

    /// <summary>
    /// HP‚ª•Ï‚í‚Á‚½‚Æ‚«‚Ìˆ—
    /// </summary>
    /// <param name="hpNormalized"></param>
    public void OnChangeHPNormalized(float hpNormalized)
    {
        if(hpNormalized <= 0) { StageManager.Instance.ChangeStageScene(StageSceneTag.StageFailed); }
    }

    /// <summary>
    /// ‹“_ˆÚ“®
    /// </summary>
    /// <param name="rotation"></param>
    public void Rotate(Vector2 rotation)
    {
        rotatable.Value.Rotate(new Vector3(rotation.y, -rotation.x, 0));
    }
}
