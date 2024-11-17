using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DG.Tweening
{
    [System.Serializable]
    public class ShakeSettings
    {
        [SerializeField] bool isLoop;
        [SerializeField] bool isFade = true;
        [SerializeField] float duration = 1f;
        [SerializeField] float strength = 1f;
        [SerializeField] int vibrato = 10;
        [SerializeField] float randomness = 90f;

        Tweener tweener;

        public void ApplyShake(Transform target)
        {
            tweener = target.DOShakePosition(duration, strength, vibrato, randomness, fadeOut : isFade)
                .SetLoops(isLoop ? -1 : 1, LoopType.Restart);
        }

        public void StopShake()
        {
            if (tweener == null) { return; }
            if (!tweener.active) { return; }

            tweener.Kill();
        }
    }

}