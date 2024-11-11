using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject cameraObj;
    [SerializeField] SerializeInterface<IStatus> status;

    [Header("�h��X�e�[�^�X")]
    [SerializeField] float duration = 0.5f;
    [SerializeField] float strength = 1f;
    [SerializeField] int vibrato = 10;
    [SerializeField] float randomness = 90f;

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
    /// �J�����̗h�ꉉ�o
    /// </summary>
    public void ShakeCamera()
    {
        cameraObj.transform.DOShakePosition(duration, strength, vibrato, randomness, false, true);
    }

}
