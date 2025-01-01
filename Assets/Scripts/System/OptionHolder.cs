using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class OptionHolder
{
    // �}�E�X�L���H
    ReactiveProperty<bool> mouseValidity = new ReactiveProperty<bool>(true);
    public IReadOnlyReactiveProperty<bool> MouseValidityReactiveProperty { get { return mouseValidity; } }
    public void SetMouseValidity(bool isValid)
    {
        mouseValidity.Value = isValid;
    }

    // �}�E�X���x
    ReactiveProperty<float> mouseSensitivity = new ReactiveProperty<float>(0.35f);
    public IReadOnlyReactiveProperty<float> MouseSensitivityReactiveProperty { get { return mouseSensitivity; } }
    public void SetMouseSensitivity(float value)
    {
        mouseSensitivity.Value = value;
    }

    // ���L�[�L���H
    ReactiveProperty<bool> arrawValidity = new ReactiveProperty<bool>(true);
    public IReadOnlyReactiveProperty<bool> IsArrawValidityReactiveProperty { get { return arrawValidity; } }
    public void SetArrawValidity(bool isValid)
    {
        arrawValidity.Value = isValid;
    }

    // ���L�[���x
    ReactiveProperty<float> arrawSensitivity = new ReactiveProperty<float>(0.35f);
    public IReadOnlyReactiveProperty<float> ArrawSensitivityReactiveProperty { get { return arrawSensitivity; } }
    public void SetArrawSensitivity(float value)
    {
        arrawSensitivity.Value = value;
    }

    // �X�y�[�X�L�[�L���H
    ReactiveProperty<bool> spaceValidity = new ReactiveProperty<bool>(true);
    public IReadOnlyReactiveProperty<bool> IsSpaceValidityReactiveProperty { get { return spaceValidity; } }
    public void SetSpaceValidity(bool isValid)
    {
        spaceValidity.Value = isValid;
    }

    // �}�E�X���x
    ReactiveProperty<float> spaceSensitivity = new ReactiveProperty<float>(0.35f);
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

    // 3DSE�{�����[��
    ReactiveProperty<float> se3DVolume = new ReactiveProperty<float>(0.75f);
    public IReadOnlyReactiveProperty<float> SE3DVolumeReactiveProperty { get { return se3DVolume; } }
    public void SetSE3DVolume(float value)
    {
        se3DVolume.Value = value;
    }

    // �𓚕\������
    ReactiveProperty<float> answerDispleyTime = new ReactiveProperty<float>(1f);
    ReactiveProperty<float> answerDispleyTimeNormalized = new ReactiveProperty<float>(0.33334f);
    public IReadOnlyReactiveProperty<float> AnswerDispleyTime { get { return answerDispleyTime; } }
    public IReadOnlyReactiveProperty<float> AnswerDispleyTimeNormalized { get { return answerDispleyTimeNormalized; } }
    public void SetAnswerDisplayTime(float normalizedValue)
    {
        answerDispleyTimeNormalized.Value = normalizedValue;
        answerDispleyTime.Value = normalizedValue * 3f;
    }

    // �𓚕\���L���H
    ReactiveProperty<bool> answerDisplayValidity = new ReactiveProperty<bool>(true);
    public IReadOnlyReactiveProperty<bool> AnswerDisplayValidityReactiveProperty { get { return answerDisplayValidity; } }
    public void SetAnswerDisplayValidity(bool isValid)
    {
        answerDisplayValidity.Value = isValid;
    }
}
