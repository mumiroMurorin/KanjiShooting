using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StageUI
{
    public class DamageEffectView : MonoBehaviour
    {
        [Header("DamageEffectAnimator")]
        [SerializeField] Animator animator;

        //ダメージを受けた時メソッド
        public void OnDamage(float decrementAmount)
        {
            animator.SetTrigger("Damage");
        }
    }

}
