using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyItemHeart : MonoBehaviour, IDamager, ISoundPlayable
{
    [SerializeField] EnemyManager enemy;
    [SerializeField] GameObject heartObject;
    [SerializeField] Animator anim;
    [SerializeField] int recoverAmount = 30;
    [SerializeField] float moveDuration = 0.75f;
    [Header("����")]
    [SerializeField] AudioClip activeClip;
    [SerializeField] SoundPlayer soundPlayer;

    bool isRecoverPermit;
    Transform playerTransform;
    IAttachableItemOnDestroy attachableItem;

    private void Start()
    {
        // EnemyManager���C���^�[�t�F�[�X���p�����Ė���������
        if(!enemy.transform.TryGetComponent<IAttachableItemOnDestroy>(out attachableItem))
        {
            Debug.LogWarning($"�yEnemy�z�C���^�[�t�F�[�X IAttachableItemOnDestroy �� {enemy.gameObject.name} �ɂ���܂���");
            return;
        }

        SetEvent();
    }

    private void SetEvent()
    {
        attachableItem.OnDestroyEvent += Heal;
    }

    /// <summary>
    /// �񕜂���
    /// </summary>
    /// <param name="holder"></param>
    private void Heal(Transform playerTransform)
    {
        // �e�q�֌W���O��
        this.transform.parent = null;

        isRecoverPermit = true;
        this.playerTransform = playerTransform;
        PlayOnShot(activeClip);

        anim.SetTrigger("Active");
    }

    /// <summary>
    /// �v���C���[�܂ňړ�����
    /// </summary>
    /// <param name="target">�ړI�n��Transform</param>
    /// <param name="duration">�ړ��ɂ����鎞�ԁi�b�j</param>
    public void MoveToPlayer()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Target Transform is null. Aborting movement.");
            return;
        }

        transform.DOMove(playerTransform.position, moveDuration)
                 .SetEase(Ease.Linear);
    }

    void IDamager.GiveDamage(IStatus status)
    {
        // �v���C���[���`�F�b�N
        if (status.Layer != MobLayer.Player) { return; }
        if (!isRecoverPermit) { return; }

        // �񕜂�����
        status.SetHP(status.HP.Value + recoverAmount);

        // �ق�ŏ�����
        Destroy(this.gameObject);
    }

    void IDamager.GiveDamage(IKanjiStatus status)
    {
        // �v���C���[���`�F�b�N
        if (status.Layer != MobLayer.Player) { return; }
        if (!isRecoverPermit) { return; }

        Debug.Log("�����̓ǂ݊֌W�Ȃ��̂ɌĂяo����Ă��܂�");

        // �񕜂�����
        status.SetHP(status.HP.Value + recoverAmount);

        // �ق�ŏ�����
        Destroy(this.gameObject);
    }

    public void PlayOnShot(AudioClip audioClip)
    {
        ((ISoundPlayable)soundPlayer).PlayOnShot(audioClip);
    }
}
