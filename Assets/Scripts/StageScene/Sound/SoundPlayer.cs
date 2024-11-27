using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoundPlayable
{
    void PlayOnShot(AudioClip audioClip);
}

public class SoundPlayer : MonoBehaviour, ISoundPlayable
{
    [SerializeField] AudioSource audioSource;

    public void PlayOnShot(AudioClip audioClip)
    {
        if(audioClip == null) { Debug.LogWarning($"�yAudio�z�N���b�v���ݒ肳��Ă��܂���: {this.gameObject.transform.parent?.name}"); return; }
        if(audioSource == null) { Debug.LogWarning($"�yAudio�zAudioSource���ݒ肳��Ă��܂���: {this.gameObject.transform.parent?.name}"); return; }

        audioSource.PlayOneShot(audioClip);
    }
}