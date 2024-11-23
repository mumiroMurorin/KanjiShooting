using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace StageUI
{
    public class PlayerHPBarView : MonoBehaviour
    {
        [Header("HPGage�e�I�u�W�F�N�g")]
        [SerializeField] Transform parentTransform;

        [Header("�_���[�W���x")]
        [SerializeField] float damageDuration;
        [SerializeField] float damageBackDuration;
        [Header("�_���[�W���Q�[�W���o�b�N�̕b��")]
        [SerializeField] float waitDurationOnDamage;
        [Header("�_���[�W���h��X�e�[�^�X")]
        [SerializeField] ShakeSettings shakeSettings;
        [Header("�_���[�W���̃o�b�N�̐F")]
        [SerializeField] Color backColorOnDamage;

        [Space(50)]
        [Header("�񕜑��x")]
        [SerializeField] float healDuration;
        [SerializeField] float healBackDuration;
        [Header("�񕜎��o�b�N���Q�[�W�̕b��")]
        [SerializeField] float waitDurationOnHeal;
        [Header("�񕜎��̃o�b�N�̐F")]
        [SerializeField] Color backColorOnHeal;

        [Header("HPSlider")]
        [SerializeField] Image gaugeImage;
        [SerializeField] Image backImage;

        float currentRate;

        /// <summary>
        /// HP���ς�����Ƃ�
        /// </summary>
        /// <param name="value"></param>
        public void OnChangeHP(float value)
        {
            // HP���������Ƃ�
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
        /// �_���[�W����HP�Q�[�W�̏���
        /// </summary>
        /// <param name="value"></param>
        public void OnDamage(float value)
        {
            backImage.color = backColorOnDamage;
            // �Q�[�W���炷 �� �o�b�N���炷
            gaugeImage.DOFillAmount(value, damageDuration)
                .OnComplete(() =>
                {
                    backImage
                        .DOFillAmount(value, damageBackDuration)
                        .SetDelay(waitDurationOnDamage);
                });
        }

        /// <summary>
        /// �񕜎���HP�Q�[�W�̏���
        /// </summary>
        /// <param name="value"></param>
        public void OnHeal(float value)
        {
            backImage.color = backColorOnHeal;
            // �o�b�N���₷ �� �Q�[�W���₷
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
