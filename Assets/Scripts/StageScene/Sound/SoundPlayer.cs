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
        if(audioClip == null) { Debug.LogWarning($"【Audio】クリップが設定されていません: {this.gameObject.transform.parent?.name}"); return; }
        if(audioSource == null) { Debug.LogWarning($"【Audio】AudioSourceが設定されていません: {this.gameObject.transform.parent?.name}"); return; }

        audioSource.PlayOneShot(audioClip);
    }
}