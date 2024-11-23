using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using EntranceTransition;
using VContainer;

public class OptionUIPresenter : MonoBehaviour
{
    [Header("オプション")]
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
    /// Model → View のイベント登録
    /// </summary>
    private void SetEvents()
    {
        // BGMVolume
        bgmVolumeView.OnChangeValueListener += (value) => { OnBGMVolumeChanged(value); };
        // SEVolume
        seVolumeView.OnChangeValueListener += (value) => { OnSEVolumeChanged(value); };

        // マウス感度
        mouseSensitivityView.OnChangeSensitivityListener += (value) => { OnMouseSensitivityChanged(value); };
        // マウス有効
        mouseSensitivityView.OnChangeValidValueListener += (value) => { OnMouseValidityChanged(value); };
        // 矢印感度
        arrawSensitivityView.OnChangeSensitivityListener += (value) => { OnArrawSensitivityChanged(value); };
        // 矢印有効
        arrawSensitivityView.OnChangeValidValueListener += (value) => { OnArrawValidityChanged(value); };
        // スペース感度
        spaceSensitivityView.OnChangeSensitivityListener += (value) => { OnSpaceSensitivityChanged(value); };
        // スペース有効
        spaceSensitivityView.OnChangeValidValueListener += (value) => { OnSpaceValidityChanged(value); };
    }

    /// <summary>
    /// View → Model の購読
    /// </summary>
    private void Bind()
    {
        // BGMVolume → SliderUI
        optionHolder.BGMVolumeReactiveProperty
            .Subscribe(value => bgmVolumeView.OnBGMVolumeChanged(value))
            .AddTo(this.gameObject);

        // SEVolume → SliderUI
        optionHolder.SEVolumeReactiveProperty
            .Subscribe(value => seVolumeView.OnSEVolumeChanged(value))
            .AddTo(this.gameObject);

        // マウス感度 → 
        optionHolder.MouseSensitivityReactiveProperty
            .Subscribe(value => mouseSensitivityView.OnSensitivityChanged(value))
            .AddTo(this.gameObject);

        // マウス有効? →
        optionHolder.IsMouseValidityReactiveProperty
            .Subscribe(value => mouseSensitivityView.OnValidityChanged(value))
            .AddTo(this.gameObject);

        // 矢印キー感度 → 
        optionHolder.ArrawSensitivityReactiveProperty
            .Subscribe(value => arrawSensitivityView.OnSensitivityChanged(value))
            .AddTo(this.gameObject);

        // 矢印キー有効? →
        optionHolder.IsArrawValidityReactiveProperty
            .Subscribe(value => arrawSensitivityView.OnValidityChanged(value))
            .AddTo(this.gameObject);

        // スペースキー感度 → 
        optionHolder.SpaceSensitivityReactiveProperty
            .Subscribe(value => spaceSensitivityView.OnSensitivityChanged(value))
            .AddTo(this.gameObject);

        // スペースキー有効? →
        optionHolder.IsSpaceValidityReactiveProperty
            .Subscribe(value => spaceSensitivityView.OnValidityChanged(value))
            .AddTo(this.gameObject);
    }

    //============ Events ============== 

    /// <summary>
    /// BGMVolumeが変えられたとき
    /// </summary>
    /// <param name="value"></param>
    private void OnBGMVolumeChanged(float value)
    {
        optionHolder.SetBGMVolume(value);
    }

    /// <summary>
    /// SEVolumeが変えられたとき
    /// </summary>
    /// <param name="value"></param>
    private void OnSEVolumeChanged(float value)
    {
        optionHolder.SetSEVolume(value);
    }

    /// <summary>
    /// マウス感度が変えられた時
    /// </summary>
    /// <param name="sensitivity"></param>
    private void OnMouseSensitivityChanged(float sensitivity)
    {
        optionHolder.SetMouseSensitivity(sensitivity);
    }

    /// <summary>
    /// マウス入力有効が変えられたとき
    /// </summary>
    /// <param name="isValid"></param>
    private void OnMouseValidityChanged(bool isValid)
    {
        optionHolder.SetMouseValidity(isValid);
    }

    /// <summary>
    /// 矢印感度が変えられた時
    /// </summary>
    /// <param name="sensitivity"></param>
    private void OnArrawSensitivityChanged(float sensitivity)
    {
        optionHolder.SetArrawSensitivity(sensitivity);
    }

    /// <summary>
    /// 矢印入力有効が変えられたとき
    /// </summary>
    /// <param name="isValid"></param>
    private void OnArrawValidityChanged(bool isValid)
    {
        optionHolder.SetArrawValidity(isValid);
    }

    /// <summary>
    /// スペースキー感度が変えられた時
    /// </summary>
    /// <param name="sensitivity"></param>
    private void OnSpaceSensitivityChanged(float sensitivity)
    {
        optionHolder.SetSpaceSensitivity(sensitivity);
    }

    /// <summary>
    /// スペースキー入力有効が変えられたとき
    /// </summary>
    /// <param name="isValid"></param>
    private void OnSpaceValidityChanged(bool isValid)
    {
        optionHolder.SetSpaceValidity(isValid);
    }
}
