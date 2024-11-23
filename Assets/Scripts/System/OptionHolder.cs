using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class OptionHolder
{
    // �}�E�X�L���H
    ReactiveProperty<bool> isMouseValidity = new ReactiveProperty<bool>(true);
    public IReadOnlyReactiveProperty<bool> IsMouseValidityReactiveProperty { get { return isMouseValidity; } }
    public void SetMouseValidity(bool isValid)
    {
        isMouseValidity.Value = isValid;
    }

    // �}�E�X���x
    ReactiveProperty<float> mouseSensitivity = new ReactiveProperty<float>(0.5f);
    public IReadOnlyReactiveProperty<float> MouseSensitivityReactiveProperty { get { return mouseSensitivity; } }
    public void SetMouseSensitivity(float value)
    {
        mouseSensitivity.Value = value;
    }

    // ���L�[�L���H
    ReactiveProperty<bool> isArrawValidity = new ReactiveProperty<bool>(true);
    public IReadOnlyReactiveProperty<bool> IsArrawValidityReactiveProperty { get { return isArrawValidity; } }
    public void SetArrawValidity(bool isValid)
    {
        isArrawValidity.Value = isValid;
    }

    // ���L�[���x
    ReactiveProperty<float> arrawSensitivity = new ReactiveProperty<float>(0.5f);
    public IReadOnlyReactiveProperty<float> ArrawSensitivityReactiveProperty { get { return arrawSensitivity; } }
    public void SetArrawSensitivity(float value)
    {
        arrawSensitivity.Value = value;
    }

    //�X�y�[�X�L�[�L���H
    ReactiveProperty<bool> isSpaceValidity = new ReactiveProperty<bool>(true);
    public IReadOnlyReactiveProperty<bool> IsSpaceValidityReactiveProperty { get { return isSpaceValidity; } }
    public void SetSpaceValidity(bool isValid)
    {
        isSpaceValidity.Value = isValid;
    }

    // �}�E�X���x
    ReactiveProperty<float> spaceSensitivity = new ReactiveProperty<float>(0.5f);
    public IReadOnlyReactiveProperty<float> SpaceSensitivityReactiveProperty { get { return spaceSensitivity; } }
    public void SetSpaceSensitivity(float value)
    {
        spaceSensitivity.Value = value;
    }

    // BGM�{�����[��
    ReactiveProperty<float> bgmVolume = new ReactiveProperty<float>(0.75f);
    public IReadOnlyReactiveProperty<float> BGMVolumeReactiveProperty { get { return bgmVolume; } }
    public void SetBGMVolume(float value)
    {
        bgmVolume.Value = value;
    }

    // SE�{�����[��
    ReactiveProperty<float> seVolume = new ReactiveProperty<float>(0.75f);
    public IReadOnlyReactiveProperty<float> SEVolumeReactiveProperty { get { return seVolume; } }
    public void SetSEVolume(float value)
    {
        seVolume.Value = value;
    }
}
