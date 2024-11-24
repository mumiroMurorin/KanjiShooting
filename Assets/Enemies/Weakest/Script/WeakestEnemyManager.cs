using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class WeakestEnemyManager : EnemyManager , IAttachableItemOnDestroy
{
    [SerializeField] Animator animator;

    [Header("�A�E�g���C��")]
    [SerializeField] OutlineSettings outlineSettings;
    [Header("�A�^�b�N���̐k��")]
    [SerializeField] ShakeSettings shakeSettings;

    bool isMoving = false;
    IStatus playerStatus;

    // �G�l�~�[�����񂾂Ƃ��̃R�[���o�b�N
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
        
        //�U���A�j���[�V����
        if (animator) { animator.SetTrigger("Attack"); }
    }

    protected override void AfterGiveDamage()
    {
        //���R�[�h�̒ǉ�
        scoreHolder.AddAnswerState(new AnswerState { questionData = questionData, isCorrect = false });

        //�A�j���[�V�������Ȃ����̏��łƗ^�_��
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
    }

    protected override void OnDeath()
    {
        // ���񂾂Ƃ��̃R�[���o�b�N����
        OnDestroyEvent?.Invoke(TargetTransform);

        // ���ʃA�j���[�V����
        if (animator) { animator.SetTrigger("Destroy"); }
        else { Destroy(this.gameObject); }
    }

    public override void Despawn()
    {
        //�f�X�|�[���A�j���[�V����
        if (animator) { animator.SetTrigger("Despawn"); }
        else { Destroy(this.gameObject); }
    }

    private void SetOutline()
    {
        outlineSettings.ApplyOutline(kanjiObject);
    }

    protected override void FixedUpdate()
    {
        if (!TargetTransform) { return; }
        if (!isMoving) { return; }

        Move?.Value.AddForce((TargetTransform.position - this.gameObject.transform.position).normalized * Time.fixedDeltaTime);
        this.gameObject.transform.LookAt(TargetTransform);
    }

    //----------------�A�j���[�V����----------------

    /// <summary>
    /// �o�����o��Ɉړ��J�n������g���K�[
    /// </summary>
    public void PermitMoveTrigger()
    {
        Move?.Value.SetActive(true);
        isMoving = true;
    }

    /// <summary>
    /// �ړ��֎~������g���K�[
    /// </summary>
    public void ProhibitMoveTrigger()
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
        shakeSettings.ApplyShake(this.transform);
    }
}
