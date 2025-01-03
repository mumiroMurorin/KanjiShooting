using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityFx.Outline;
using System;

public class FasterEnemyManager : EnemyManager, IAttachableItemOnDestroy, ISoundPlayable
{
    [SerializeField] Animator animator;

    [Header("�^�C��")]
    [SerializeField] GameObject[] wheelsRight;
    [SerializeField] GameObject[] wheelsLeft;

    [Header("�A�E�g���C��")]
    [SerializeField] OutlineSettings outlineSettings;
    [Header("�A�^�b�N���̐k��")]
    [SerializeField] ShakeSettings shakeSettingsOnAttack;

    [Header("�ړ����ԑ̗̂h��")]
    [SerializeField] ShakeSettings shakeSettingsOnMove;

    [Header("����")]
    [SerializeField] SoundPlayer soundPlayer;

    bool isMoving = false;
    IStatus playerStatus;

    public Action<Transform> OnDestroyEvent { get; set; }

    private void Start()
    {
        Move?.Value.SetActive(false);
    }

    public override void GiveDamage(IStatus status)
    {
        //�v���C���[���`�F�b�N
        if (status.Layer != MobLayer.Player) { return; }

        //�^�_���O�ɃA�j���[�V�����Đ�
        playerStatus = status;

        // �̂̐k�����~�߂�
        shakeSettingsOnMove.StopShake();

        //�U���A�j���[�V����
        if (animator) { animator.SetTrigger("Attack"); }
    }

    protected override void AfterGiveDamage()
    {
        // �s�������R�[�h�̒ǉ�
        StageManager.Instance.AddAnswerStatus(new AnswerStatus { questionData = questionData, state = AnswerState.Incorrected });

        // �A�j���[�V�������Ȃ����̏��łƗ^�_��
        if (!animator) 
        {
            GiveDamageTrigger();
            Destroy(this.gameObject);
        }
    }

    protected override void AfterSpawn()
    {
        this.gameObject.transform.LookAt(TargetTransform);
        SetOutline();
        SetWheel();
    }

    protected override void OnDeath()
    {
        // �̂̐k�����~�߂�
        shakeSettingsOnMove.StopShake();

        // �A�C�e�����ʔ���
        OnDestroyEvent?.Invoke(TargetTransform);

        //���ʃA�j���[�V����
        if (animator) { animator.SetTrigger("Destroy"); }
        else { Destroy(this.gameObject); }
    }

    public override void Despawn()
    {
        // �̂̐k�����~�߂�
        shakeSettingsOnMove.StopShake();

        //�f�X�|�[���A�j���[�V����
        if (animator) { animator.SetTrigger("Despawn"); }
        else { Destroy(this.gameObject); }
    }

    private void SetOutline()
    {
        outlineSettings.ApplyOutline(kanjiObject.GameObject);
    }

    /// <summary>
    /// �^�C���̈ʒu�𒲐�
    /// </summary>
    private void SetWheel()
    {
        float center = kanjiObject.KanjiCollider.center.x;
        float width = kanjiObject.KanjiCollider.size.x;

        foreach(GameObject obj in wheelsRight)
        {
            obj.transform.localPosition = new Vector3(center + width / 2f, obj.transform.localPosition.y, obj.transform.localPosition.z);
        }

        foreach (GameObject obj in wheelsLeft)
        {
            obj.transform.localPosition = new Vector3(center - width / 2f, obj.transform.localPosition.y, obj.transform.localPosition.z);
        }
    }

    public void PlayOnShot(AudioClip audioClip)
    {
        ((ISoundPlayable)soundPlayer).PlayOnShot(audioClip);
    }

    protected override void FixedUpdate()
    {
        if (!TargetTransform) { return; }
        if (!isMoving) { return; }

        Move?.Value.AddForce((TargetTransform.position - this.gameObject.transform.position).normalized * Time.fixedDeltaTime);
        this.gameObject.transform.LookAt(TargetTransform);
    }

    private void OnDestroy()
    {
        shakeSettingsOnAttack.StopShake();
        shakeSettingsOnMove.StopShake();
    }

    //----------------�A�j���[�V����----------------

    /// <summary>
    /// �o�����o��Ɉړ��J�n������g���K�[
    /// </summary>
    public void PermitMoveTrigger()
    {
        Move?.Value.SetActive(true);
        isMoving = true;
        shakeSettingsOnMove.ApplyShake(kanjiTransform);
    }

    /// <summary>
    /// �U�����ړ��֎~������g���K�[
    /// </summary>
    public void ProhibitMoveTriggerAttacking()
    {
        isMoving = false;
    }

    /// <summary>
    /// �������ړ��֎~������g���K�[
    /// </summary>
    public void ProhibitMoveTriggerDestroying()
    {
        Move?.Value.SetActive(false);
        isMoving = false;
    }


    /// <summary>
    /// ���ł̃g���K�[
    /// </summary>
    public void DestroyTrigger()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// �^�_���g���K�[
    /// </summary>
    public void GiveDamageTrigger()
    {
        playerStatus.SetHP(playerStatus.HP.Value - KanjiStatus.Value.Attack.Value);

        //�ق�ŏ�������Ȃ񂾂�
        AfterGiveDamage();
    }

    /// <summary>
    /// �U�����ɑ̂�k�킹��g���K�[
    /// </summary>
    public void VibrateTrigger()
    {
        shakeSettingsOnAttack.ApplyShake(this.transform);
    }
}
