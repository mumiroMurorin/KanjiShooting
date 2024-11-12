using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject cameraObj;
    [SerializeField] SerializeInterface<IStatus> status;

    [Header("揺れステータス")]
    [SerializeField] ShakeSettings shakeSettings; 

    private void Start()
    {
        Bind();        
    }

    private void Bind()
    {
        status.Value.HPNormalized
            .Pairwise()
            .Where(pair => pair.Current < pair.Previous)
            .Subscribe(pair => ShakeCamera())
            .AddTo(this.gameObject);
    }

    /// <summary>
    /// カメラの揺れ演出
    /// </summary>
    public void ShakeCamera()
    {
        shakeSettings.ApplyShake(cameraObj.transform);
    }

}
