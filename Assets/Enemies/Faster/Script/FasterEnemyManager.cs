using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FasterEnemyManager : EnemyManager
{
    [SerializeField] Animator animator;
    [SerializeField] Color outlineColor;
    [SerializeField] float outlineWidth;

    [Header("�^�C��")]
    [SerializeField] GameObject wheelRight;
    [SerializeField] GameObject wheelLeft;

    [Header("�A�^�b�N���̐k��")]
    [SerializeField] ShakeSettings shakeSettings;

    bool isMoving = false;
    IStatus playerStatus;

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
        ScoreManager.Instance.AddAnswerState(new AnswerState { questionData = questionData, isCorrect = false });

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
        SetWheel();
    }

    protected override void OnDeath()
    {
        //���ʃA�j���[�V����
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
        Outline outline = kanjiObject.AddComponent<Outline>();
        outline.OutlineColor = outlineColor;
        outline.OutlineWidth = outlineWidth;
        outline.OutlineMode = Outline.Mode.OutlineVisible;
    }

    /// <summary>
    /// �^�C���̈ʒu�𒲐�
    /// </summary>
    private void SetWheel()
    {
        float center = kanjiCollider.center.x;
        float width = kanjiCollider.size.x;

        wheelRight.transform.localPosition = new Vector3(center + width / 2f, wheelRight.transform.localPosition.y, wheelRight.transform.localPosition.z);
        wheelLeft.transform.localPosition = new Vector3(center - width / 2f, wheelLeft.transform.localPosition.y, wheelLeft.transform.localPosition.z);
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
