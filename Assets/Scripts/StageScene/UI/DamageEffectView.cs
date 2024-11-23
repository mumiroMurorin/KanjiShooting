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

        //�_���[�W���󂯂������\�b�h
        public void OnDamage(float decrementAmount)
        {
            animator.SetTrigger("Damage");
        }
    }

}
