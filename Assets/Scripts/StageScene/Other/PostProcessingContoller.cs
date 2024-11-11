using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UniRx;

/// <summary>
/// Strategy�p�^�[����p����PostProcessing�̐؂�ւ�������
/// </summary>
public class PostProcessingContoller : MonoBehaviour
{
    [SerializeField] PostProcessVolume volume;
    [Header("�m����")]
    [SerializeReference, SubclassSelector] IPostProcessingEffect[] dyingEffects;
    [SerializeField] float dyingHpRatio = 0.2f;
    [SerializeField] SerializeInterface<IStatus> playerStatus;

    List<IPostProcessingEffect> allEffect;

    private void Start()
    {
        Initialize();
        Bind();
    }

    private void Initialize()
    {
        allEffect = new List<IPostProcessingEffect>();
        allEffect.AddRange(dyingEffects);

        foreach (IPostProcessingEffect effect in allEffect)
        {
            effect.SetProfile(volume.profile);
            effect.SetEnableEffect(false);
        }
    }

    private void Bind()
    {
        // �m�����|�X�g�G�t�F�N�g���I��
        playerStatus.Value.HPNormalized
            .Where(value => value <= dyingHpRatio)
            .Subscribe(_ => 
            {
                foreach(var effect in dyingEffects) { effect.SetEnableEffect(true); }
            })
            .AddTo(this.gameObject);

        // �ʏ펞�|�X�g�G�t�F�N�g���I�t
        playerStatus.Value.HPNormalized
            .Where(value => value > dyingHpRatio)
            .Subscribe(_ =>
            {
                foreach (var effect in dyingEffects) { effect.SetEnableEffect(false); }
            })
            .AddTo(this.gameObject);
    }

    private void Update()
    {
        // ���Ԃ�ǉ�����
        foreach(IPostProcessingEffect effect in allEffect)
        {
            effect.AddTime(Time.deltaTime);
            effect.ApplyEffect();
        }
    }
}

/// <summary>
/// �|�X�g�G�t�F�N�g�̐؂�ւ�
/// </summary>
public interface IPostProcessingEffect
{
    /// <summary>
    /// Volume.profile�̃Z�b�g
    /// </summary>
    /// <param name="profile"></param>
    void SetProfile(PostProcessProfile profile);

    /// <summary>
    /// �G�t�F�N�g�̓K�p (�}�C�t���[��������)
    /// </summary>
    /// <param name="volume"></param>
    void ApplyEffect();

    /// <summary>
    /// �G�t�F�N�g�̃I���I�t
    /// </summary>
    /// <param name="isEnabled"></param>
    void SetEnableEffect(bool isEnabled);

    /// <summary>
    /// ����t�̒ǉ�
    /// </summary>
    /// <param name="value"></param>
    void AddTime(float value);
}

public class ChromaticAberrationEffect : IPostProcessingEffect
{
    ChromaticAberration chromatic;

    [SerializeField] AnimationCurve curve;
    [SerializeField] float duration;
    float NormalizedTime => timeElapsed / duration;
    float timeElapsed = 0;

    public void AddTime(float value)
    {
        timeElapsed += value;
        if (timeElapsed > duration) { timeElapsed = 0; }
    }

    public void SetProfile(PostProcessProfile profile)
    {
        if (!profile.TryGetSettings<ChromaticAberration>(out chromatic))
        {
            Debug.Log("ChromaticAberration is not set");
            return;
        }
    }

    public void ApplyEffect()
    {
        if(chromatic == null) { return; }

        chromatic.intensity.value = curve.Evaluate(NormalizedTime);
    }

    public void SetEnableEffect(bool isEnabled)
    {
        if(chromatic == null) { return; }
        chromatic.enabled.value = isEnabled;
    }
}

public class VignetteEffect : IPostProcessingEffect
{
    Vignette vignette;

    [SerializeField] AnimationCurve curve;
    [SerializeField] float duration;
    float NormalizedTime => timeElapsed / duration;
    float timeElapsed = 0;

    public void AddTime(float value)
    {
        timeElapsed += value;
        if (timeElapsed > duration) { timeElapsed = 0; }
    }

    public void SetProfile(PostProcessProfile profile)
    {
        if (!profile.TryGetSettings<Vignette>(out vignette))
        {
            Debug.Log("ChromaticAberration is not set");
            return;
        }
    }

    public void ApplyEffect()
    {
        if (vignette == null) { return; }

        vignette.intensity.value = curve.Evaluate(NormalizedTime);
    }

    public void SetEnableEffect(bool isEnabled)
    {
        if (vignette == null) { return; }
        vignette.enabled.value = isEnabled;
    }
}