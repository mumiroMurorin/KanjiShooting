using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerHealingEffect : MonoBehaviour, ISoundPlayable
{
    [SerializeField] SerializeInterface<IStatus> playerStatus;
    [SerializeField] ParticleSystem healParticle;
    [Header("����")]
    [SerializeField] AudioClip healAudio;
    [SerializeField] SoundPlayer soundPlayer;

    private void Start()
    {
        playerStatus.Value.HPNormalized
            .Pairwise()
            // �񕜎�(����HP�̕���������)
            .Where(value => value.Previous < value.Current)
            .Subscribe(value => Healiing())
            .AddTo(this.gameObject);
    }

    /// <summary>
    /// �񕜃G�t�F�N�g�̍Đ�
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
