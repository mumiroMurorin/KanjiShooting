using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UniRx;

/// <summary>
/// Strategyパターンを用いてPostProcessingの切り替えを実装
/// </summary>
public class PostProcessingContoller : MonoBehaviour
{
    [SerializeField] PostProcessVolume volume;
    [Header("瀕死時")]
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
        // 瀕死時ポストエフェクトをオン
        playerStatus.Value.HPNormalized
            .Where(value => value <= dyingHpRatio)
            .Subscribe(_ => 
            {
                foreach(var effect in dyingEffects) { effect.SetEnableEffect(true); }
            })
            .AddTo(this.gameObject);

        // 通常時ポストエフェクトをオフ
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
        // 時間を追加する
        foreach(IPostProcessingEffect effect in allEffect)
        {
            effect.AddTime(Time.deltaTime);
            effect.ApplyEffect();
        }
    }
}

/// <summary>
/// ポストエフェクトの切り替え
/// </summary>
public interface IPostProcessingEffect
{
    /// <summary>
    /// Volume.profileのセット
    /// </summary>
    /// <param name="profile"></param>
    void SetProfile(PostProcessProfile profile);

    /// <summary>
    /// エフェクトの適用 (マイフレーム動かす)
    /// </summary>
    /// <param name="volume"></param>
    void ApplyEffect();

    /// <summary>
    /// エフェクトのオンオフ
    /// </summary>
    /// <param name="isEnabled"></param>
    void SetEnableEffect(bool isEnabled);

    /// <summary>
    /// 時間tの追加
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