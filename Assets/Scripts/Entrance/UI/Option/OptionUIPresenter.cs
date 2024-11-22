using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using EntranceTransition;
using Sound;

public class OptionUIPresenter : MonoBehaviour
{
    [Header("オプション")]
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
        mouseSensitivityView.OnChangeValidValueListener += (value) => { OnMouseValidableChanged(value); };
        // 矢印感度
        arrawSensitivityView.OnChangeSensitivityListener += (value) => { OnArrawSensitivityChanged(value); };
        // 矢印有効
        arrawSensitivityView.OnChangeValidValueListener += (value) => { OnArrawValidableChanged(value); };
        // スペース感度
        spaceSensitivityView.OnChangeSensitivityListener += (value) => { OnSpaceSensitivityChanged(value); };
        // スペース有効
        spaceSensitivityView.OnChangeValidValueListener += (value) => { OnSpaceValidableChanged(value); };
    }

    /// <summary>
    /// View → Model の購読
    /// </summary>
    private void Bind()
    {
        // BGMVolume → SliderUI
        Sound.SoundManager.Instance.BGMVolumeReactiveProperty
            .Subscribe(value => bgmVolumeView.OnBGMVolumeChanged(value))
            .AddTo(this.gameObject);

        // SEVolume → SliderUI
        Sound.SoundManager.Instance.SEVolumeReactiveProperty
            .Subscribe(value => seVolumeView.OnSEVolumeChanged(value))
            .AddTo(this.gameObject);

        // マウス感度 → 
    }

    //============ Events ============== 

    /// <summary>
    /// BGMVolumeが変えられたとき
    /// </summary>
    /// <param name="value"></param>
    private void OnBGMVolumeChanged(float value)
    {
        Sound.SoundManager.Instance.BGMMasterVolume = value;
    }

    /// <summary>
    /// SEVolumeが変えられたとき
    /// </summary>
    /// <param name="value"></param>
    private void OnSEVolumeChanged(float value)
    {
        Sound.SoundManager.Instance.SEMasterVolume = value;
    }

    /// <summary>
    /// マウス感度が変えられた時
    /// </summary>
    /// <param name="sensitivity"></param>
    private void OnMouseSensitivityChanged(float sensitivity)
    {

    }

    /// <summary>
    /// マウス入力有効が変えられたとき
    /// </summary>
    /// <param name="isValid"></param>
    private void OnMouseValidableChanged(bool isValid)
    {

    }

    /// <summary>
    /// 矢印感度が変えられた時
    /// </summary>
    /// <param name="sensitivity"></param>
    private void OnArrawSensitivityChanged(float sensitivity)
    {

    }

    /// <summary>
    /// 矢印入力有効が変えられたとき
    /// </summary>
    /// <param name="isValid"></param>
    private void OnArrawValidableChanged(bool isValid)
    {

    }

    /// <summary>
    /// スペースキー感度が変えられた時
    /// </summary>
    /// <param name="sensitivity"></param>
    private void OnSpaceSensitivityChanged(float sensitivity)
    {

    }

    /// <summary>
    /// スペースキー入力有効が変えられたとき
    /// </summary>
    /// <param name="isValid"></param>
    private void OnSpaceValidableChanged(bool isValid)
    {

    }
}
