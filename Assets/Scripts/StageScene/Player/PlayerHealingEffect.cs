using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerHealingEffect : MonoBehaviour, ISoundPlayable
{
    [SerializeField] SerializeInterface<IStatus> playerStatus;
    [SerializeField] ParticleSystem healParticle;
    [Header("音声")]
    [SerializeField] AudioClip healAudio;
    [SerializeField] SoundPlayer soundPlayer;

    private void Start()
    {
        playerStatus.Value.HPNormalized
            .Pairwise()
            // 回復時(現在HPの方が多い時)
            .Where(value => value.Previous < value.Current)
            .Subscribe(value => Healiing())
            .AddTo(this.gameObject);
    }

    /// <summary>
    /// 回復エフェクトの再生
    /// </summary>
    public void Healiing()
    {
        if (healParticle == null) { return; }
        if (healParticle.isPlaying) { return; }

        PlayOnShot(healAudio);
        healParticle?.Play();
    }

    public void PlayOnShot(AudioClip audioClip)
    {
        ((ISoundPlayable)soundPlayer).PlayOnShot(audioClip);
    }
}
