using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using EntranceTransition;
using Sound;

public class OptionUIPresenter : MonoBehaviour
{
    [Header("�I�v�V����")]
    [SerializeField] BGMVolumeView bgmVolumeView;
    [SerializeField] SEVolumeView seVolumeView;
    [SerializeField] MouseSensitivityView mouseSensitivityView;
    [SerializeField] ArrawSensitivityView arrawSensitivityView;
    [SerializeField] SpaceSensitivityView spaceSensitivityView;

    private void Start()
    {
        SetEvents();
        Bind();
    }

    /// <summary>
    /// Model �� View �̃C�x���g�o�^
    /// </summary>
    private void SetEvents()
    {
        // BGMVolume
        bgmVolumeView.OnChangeValueListener += (value) => { OnBGMVolumeChanged(value); };
        // SEVolume
        seVolumeView.OnChangeValueListener += (value) => { OnSEVolumeChanged(value); };

        // �}�E�X���x
        mouseSensitivityView.OnChangeSensitivityListener += (value) => { OnMouseSensitivityChanged(value); };
        // �}�E�X�L��
        mouseSensitivityView.OnChangeValidValueListener += (value) => { OnMouseValidableChanged(value); };
        // ��󊴓x
        arrawSensitivityView.OnChangeSensitivityListener += (value) => { OnArrawSensitivityChanged(value); };
        // ���L��
        arrawSensitivityView.OnChangeValidValueListener += (value) => { OnArrawValidableChanged(value); };
        // �X�y�[�X���x
        spaceSensitivityView.OnChangeSensitivityListener += (value) => { OnSpaceSensitivityChanged(value); };
        // �X�y�[�X�L��
        spaceSensitivityView.OnChangeValidValueListener += (value) => { OnSpaceValidableChanged(value); };
    }

    /// <summary>
    /// View �� Model �̍w��
    /// </summary>
    private void Bind()
    {
        // BGMVolume �� SliderUI
        Sound.SoundManager.Instance.BGMVolumeReactiveProperty
            .Subscribe(value => bgmVolumeView.OnBGMVolumeChanged(value))
            .AddTo(this.gameObject);

        // SEVolume �� SliderUI
        Sound.SoundManager.Instance.SEVolumeReactiveProperty
            .Subscribe(value => seVolumeView.OnSEVolumeChanged(value))
            .AddTo(this.gameObject);

        // �}�E�X���x �� 
    }

    //============ Events ============== 

    /// <summary>
    /// BGMVolume���ς���ꂽ�Ƃ�
    /// </summary>
    /// <param name="value"></param>
    private void OnBGMVolumeChanged(float value)
    {
        Sound.SoundManager.Instance.BGMMasterVolume = value;
    }

    /// <summary>
    /// SEVolume���ς���ꂽ�Ƃ�
    /// </summary>
    /// <param name="value"></param>
    private void OnSEVolumeChanged(float value)
    {
        Sound.SoundManager.Instance.SEMasterVolume = value;
    }

    /// <summary>
    /// �}�E�X���x���ς���ꂽ��
    /// </summary>
    /// <param name="sensitivity"></param>
    private void OnMouseSensitivityChanged(float sensitivity)
    {

    }

    /// <summary>
    /// �}�E�X���͗L�����ς���ꂽ�Ƃ�
    /// </summary>
    /// <param name="isValid"></param>
    private void OnMouseValidableChanged(bool isValid)
    {

    }

    /// <summary>
    /// ��󊴓x���ς���ꂽ��
    /// </summary>
    /// <param name="sensitivity"></param>
    private void OnArrawSensitivityChanged(float sensitivity)
    {

    }

    /// <summary>
    /// �����͗L�����ς���ꂽ�Ƃ�
    /// </summary>
    /// <param name="isValid"></param>
    private void OnArrawValidableChanged(bool isValid)
    {

    }

    /// <summary>
    /// �X�y�[�X�L�[���x���ς���ꂽ��
    /// </summary>
    /// <param name="sensitivity"></param>
    private void OnSpaceSensitivityChanged(float sensitivity)
    {

    }

    /// <summary>
    /// �X�y�[�X�L�[���͗L�����ς���ꂽ�Ƃ�
    /// </summary>
    /// <param name="isValid"></param>
    private void OnSpaceValidableChanged(bool isValid)
    {

    }
}
