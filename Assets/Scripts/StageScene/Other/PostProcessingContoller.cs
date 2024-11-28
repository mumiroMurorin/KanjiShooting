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
    [Header("死亡時")]
    [SerializeReference, SubclassSelector] IPostProcessingEffect[] gameOverEffects;
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
        allEffect.AddRange(gameOverEffects);

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

        // 死亡時ポストエフェクトをオン
        playerStatus.Value.HPNormalized
            .Where(value => value <= 0f)
            .Subscribe(_ =>
            {
                foreach (var effect in gameOverEffects) { effect.SetEnableEffect(true); }
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
            Debug.Log("Vignette is not set");
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

public class ColorGrandingEffect : IPostProcessingEffect
{
    ColorGrading colorGranding;

    [SerializeField] AnimationCurve saturationCurve;
    [SerializeField] AnimationCurve brightnessCurve;
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
        if (!profile.TryGetSettings<ColorGrading>(out colorGranding))
        {
            Debug.Log("ColorGrading is not set");
            return;
        }
    }

    public void ApplyEffect()
    {
        if (colorGranding == null) { return; }

        colorGranding.saturation.value = saturationCurve.Evaluate(NormalizedTime);
        colorGranding.brightness.value = brightnessCurve.Evaluate(NormalizedTime);
    }

    public void SetEnableEffect(bool isEnabled)
    {
        if (colorGranding == null) { return; }
        colorGranding.enabled.value = isEnabled;
    }
}