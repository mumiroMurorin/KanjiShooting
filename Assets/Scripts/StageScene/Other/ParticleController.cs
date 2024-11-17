using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [System.Serializable]
    class ParticleGroup
    {
        [SerializeField] string name;
        [SerializeField] ParticleSystem[] particles;

        public string Name { get { return name; } }
        public ParticleSystem[] Particles { get { return particles; } }
    }

    [SerializeField] ParticleGroup[] particleGroups;

    /// <summary>
    /// パーティクルの再生
    /// </summary>
    /// <param name="index"></param>
    public void PlayParticle(int index)
    {
        if (particleGroups == null) { return; }
        if (index >= particleGroups.Length) { return; }

        foreach (ParticleSystem particleSystem in particleGroups[index].Particles)
        {
            particleSystem.Play();
        }
    }

    /// <summary>
    /// パーティクルの再生
    /// </summary>
    /// <param name="index"></param>
    public void PlayParticleFromName(string name)
    {
        for(int i = 0; i < particleGroups.Length; i++)
        {
            if (particleGroups[i].Name == name)
            {
                PlayParticle(i);
                return;
            }
        }
        Debug.LogWarning($"【Enemies】パーティクルグループ {name}が見つかりませんでした");
    }

    /// <summary>
    /// パーティクルの停止
    /// </summary>
    /// <param name="index"></param>
    public void StopParticle(int index)
    {
        if (particleGroups == null) { return; }
        if (index >= particleGroups.Length) { return; }

        foreach (ParticleSystem particleSystem in particleGroups[index].Particles)
        {
            particleSystem.Stop();
        }
    }

    /// <summary>
    /// パーティクルの停止
    /// </summary>
    /// <param name="index"></param>
    public void StopParticleFromName(string name)
    {
        for (int i = 0; i < particleGroups.Length; i++)
        {
            if (particleGroups[i].Name == name)
            {
                StopParticle(i);
                return;
            }
        }
        Debug.LogWarning($"【Enemies】パーティクルグループ {name}が見つかりませんでした");
    }

    public void PlayParticle(int[] indexes)
    {
        foreach(int index in indexes)
        {
            PlayParticle(index);
        }
    }
}
