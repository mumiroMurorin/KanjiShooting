using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DG.Tweening
{
    [System.Serializable]
    public class ShakeSettings
    {
        [SerializeField] float duration = 1f;
        [SerializeField] float strength = 1f;
        [SerializeField] int vibrato = 10;
        [SerializeField] float randomness = 90f;

        public void ApplyShake(Transform target)
        {
            target.DOShakePosition(duration, strength, vibrato, randomness);
        }
    }

}