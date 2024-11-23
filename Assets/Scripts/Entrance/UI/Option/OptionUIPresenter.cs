using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using EntranceTransition;
using VContainer;

public class OptionUIPresenter : MonoBehaviour
{
    [Header("�I�v�V����")]
    [SerializeField] BGMVolumeView bgmVolumeView;
    [SerializeField] SEVolumeView seVolumeView;
    [SerializeField] MouseSensitivityView mouseSensitivityView;
    [SerializeField] ArrawSensitivityView arrawSensitivityView;
    [SerializeField] SpaceSensitivityView spaceSensitivityView;

    OptionHolder optionHolder;

    [Inject]
    public void Construct(OptionHolder holder)
    {
        optionHolder = holder;
    }

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
        mouseSensitivityView.OnChangeValidValueListener += (value) => { OnMouseValidityChanged(value); };
        // ��󊴓x
        arrawSensitivityView.OnChangeSensitivityListener += (value) => { OnArrawSensitivityChanged(value); };
        // ���L��
        arrawSensitivityView.OnChangeValidValueListener += (value) => { OnArrawValidityChanged(value); };
        // �X�y�[�X���x
        spaceSensitivityView.OnChangeSensitivityListener += (value) => { OnSpaceSensitivityChanged(value); };
        // �X�y�[�X�L��
        spaceSensitivityView.OnChangeValidValueListener += (value) => { OnSpaceValidityChanged(value); };
    }

    /// <summary>
    /// View �� Model �̍w��
    /// </summary>
    private void Bind()
    {
        // BGMVolume �� SliderUI
        optionHolder.BGMVolumeReactiveProperty
            .Subscribe(value => bgmVolumeView.OnBGMVolumeChanged(value))
            .AddTo(this.gameObject);

        // SEVolume �� SliderUI
        optionHolder.SEVolumeReactiveProperty
            .Subscribe(value => seVolumeView.OnSEVolumeChanged(value))
            .AddTo(this.gameObject);

        // �}�E�X���x �� 
        optionHolder.MouseSensitivityReactiveProperty
            .Subscribe(value => mouseSensitivityView.OnSensitivityChanged(value))
            .AddTo(this.gameObject);

        // �}�E�X�L��? ��
        optionHolder.IsMouseValidityReactiveProperty
            .Subscribe(value => mouseSensitivityView.OnValidityChanged(value))
            .AddTo(this.gameObject);

        // ���L�[���x �� 
        optionHolder.ArrawSensitivityReactiveProperty
            .Subscribe(value => arrawSensitivityView.OnSensitivityChanged(value))
            .AddTo(this.gameObject);

        // ���L�[�L��? ��
        optionHolder.IsArrawValidityReactiveProperty
            .Subscribe(value => arrawSensitivityView.OnValidityChanged(value))
            .AddTo(this.gameObject);

        // �X�y�[�X�L�[���x �� 
        optionHolder.SpaceSensitivityReactiveProperty
            .Subscribe(value => spaceSensitivityView.OnSensitivityChanged(value))
            .AddTo(this.gameObject);

        // �X�y�[�X�L�[�L��? ��
        optionHolder.IsSpaceValidityReactiveProperty
            .Subscribe(value => spaceSensitivityView.OnValidityChanged(value))
            .AddTo(this.gameObject);
    }

    //============ Events ============== 

    /// <summary>
    /// BGMVolume���ς���ꂽ�Ƃ�
    /// </summary>
    /// <param name="value"></param>
    private void OnBGMVolumeChanged(float value)
    {
        optionHolder.SetBGMVolume(value);
    }

    /// <summary>
    /// SEVolume���ς���ꂽ�Ƃ�
    /// </summary>
    /// <param name="value"></param>
    private void OnSEVolumeChanged(float value)
    {
        optionHolder.SetSEVolume(value);
    }

    /// <summary>
    /// �}�E�X���x���ς���ꂽ��
    /// </summary>
    /// <param name="sensitivity"></param>
    private void OnMouseSensitivityChanged(float sensitivity)
    {
        optionHolder.SetMouseSensitivity(sensitivity);
    }

    /// <summary>
    /// �}�E�X���͗L�����ς���ꂽ�Ƃ�
    /// </summary>
    /// <param name="isValid"></param>
    private void OnMouseValidityChanged(bool isValid)
    {
        optionHolder.SetMouseValidity(isValid);
    }

    /// <summary>
    /// ��󊴓x���ς���ꂽ��
    /// </summary>
    /// <param name="sensitivity"></param>
    private void OnArrawSensitivityChanged(float sensitivity)
    {
        optionHolder.SetArrawSensitivity(sensitivity);
    }

    /// <summary>
    /// �����͗L�����ς���ꂽ�Ƃ�
    /// </summary>
    /// <param name="isValid"></param>
    private void OnArrawValidityChanged(bool isValid)
    {
        optionHolder.SetArrawValidity(isValid);
    }

    /// <summary>
    /// �X�y�[�X�L�[���x���ς���ꂽ��
    /// </summary>
    /// <param name="sensitivity"></param>
    private void OnSpaceSensitivityChanged(float sensitivity)
    {
        optionHolder.SetSpaceSensitivity(sensitivity);
    }

    /// <summary>
    /// �X�y�[�X�L�[���͗L�����ς���ꂽ�Ƃ�
    /// </summary>
    /// <param name="isValid"></param>
    private void OnSpaceValidityChanged(bool isValid)
    {
        optionHolder.SetSpaceValidity(isValid);
    }
}
