using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace StageUI
{
    public class PlayerHPBarView : MonoBehaviour
    {
        [Header("HPGage親オブジェクト")]
        [SerializeField] Transform parentTransform;

        [Header("ダメージ速度")]
        [SerializeField] float damageDuration;
        [SerializeField] float damageBackDuration;
        [Header("ダメージ時ゲージ→バックの秒間")]
        [SerializeField] float waitDurationOnDamage;
        [Header("ダメージ時揺れステータス")]
        [SerializeField] ShakeSettings shakeSettings;
        [Header("ダメージ時のバックの色")]
        [SerializeField] Color backColorOnDamage;

        [Space(50)]
        [Header("回復速度")]
        [SerializeField] float healDuration;
        [SerializeField] float healBackDuration;
        [Header("回復時バック→ゲージの秒間")]
        [SerializeField] float waitDurationOnHeal;
        [Header("回復時のバックの色")]
        [SerializeField] Color backColorOnHeal;

        [Header("HPSlider")]
        [SerializeField] Image gaugeImage;
        [SerializeField] Image backImage;

        float currentRate;

        /// <summary>
        /// HPが変わったとき
        /// </summary>
        /// <param name="value"></param>
        public void OnChangeHP(float value)
        {
            // HPが減ったとき
            if (currentRate > value)
            {
                OnDamage(value);
                shakeSettings.ApplyShake(parentTransform);
            }
            else
            {
                OnHeal(value);
            }

            currentRate = value;
        }

        /// <summary>
        /// ダメージ時のHPゲージの処理
        /// </summary>
        /// <param name="value"></param>
        public void OnDamage(float value)
        {
            backImage.color = backColorOnDamage;
            // ゲージ減らす → バック減らす
            gaugeImage.DOFillAmount(value, damageDuration)
                .OnComplete(() =>
                {
                    backImage
                        .DOFillAmount(value, damageBackDuration)
                        .SetDelay(waitDurationOnDamage);
                });
        }

        /// <summary>
        /// 回復時のHPゲージの処理
        /// </summary>
        /// <param name="value"></param>
        public void OnHeal(float value)
        {
            backImage.color = backColorOnHeal;
            // バック増やす → ゲージ増やす
            backImage.DOFillAmount(value, healBackDuration)
                .OnComplete(() =>
                {
                    gaugeImage
                        .DOFillAmount(value, healDuration)
                        .SetDelay(waitDurationOnHeal);
                });
        }

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        OnHeal(1f);
        //    }
        //    else if (Input.GetKeyDown(KeyCode.D))
        //    {
        //        OnDamage(0.4f);
        //    }
        //}

    }

}
