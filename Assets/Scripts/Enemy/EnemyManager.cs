using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyManager : MonoBehaviour, IEnemy, IDamager
{
    [SerializeField] protected SerializeInterface<IForceAddable> Move;
    [SerializeField] protected SerializeInterface<IKanjiStatus> KanjiStatus;
    [SerializeField] protected Transform kanjiTransform;
    
    protected GameObject kanjiObject;
    protected Transform TargetTransform;
    protected QuestionData questionData;

    /// <summary>
    /// HP���ς�����Ƃ��̏���
    /// </summary>
    /// <param name="hp"></param>
    public virtual void OnChangeHP(int hp)
    {
        if(hp <= 0)
        {
            ScoreManager.Instance.IncrementKillCount(); //�L�������v���X
            OnDeath();
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="initialData"></param>
    public void Initialize(EnemyInitializationData initialData)
    {
        TargetTransform = initialData.target;
        questionData = initialData.questionData;
        KanjiStatus.Value.SetAnswers(initialData.questionData.answers);

        SetKanjiObject(initialData.kanjiObject);
        AfterSpawn();
    }

    /// <summary>
    /// �����I�u�W�F�N�g�̃Z�b�g
    /// </summary>
    /// <param name="kanjiObject"></param>
    private void SetKanjiObject(GameObject kanjiObject)
    {
        this.kanjiObject = kanjiObject;
        kanjiObject.transform.position = kanjiTransform.position;
        kanjiObject.transform.SetParent(kanjiTransform);
    }

    /// <summary>
    /// �v���C���[�Ƀ_���[�W��^����
    /// </summary>
    /// <param name="status"></param>
    public virtual void GiveDamage(IStatus status)
    {
        //�v���C���[���`�F�b�N
        if(status.Layer != MobLayer.Player) { return; }

        //�_���[�W��H��킹��
        status.SetHP(status.HP.Value - KanjiStatus.Value.Attack.Value);

        //�ق�ŏ�������Ȃ񂾂�
        AfterGiveDamage();
    }

    public void GiveDamage(IKanjiStatus kanjiStatus)
    {
        //�v���C���[���`�F�b�N
        if (kanjiStatus.Layer != MobLayer.Player) { return; }

        Debug.Log("�����̓ǂ݊֌W�Ȃ��̂ɌĂяo����Ă��܂�");

        //�_���[�W��H��킹��
        kanjiStatus.SetHP(kanjiStatus.HP.Value - KanjiStatus.Value.Attack.Value);

        //�ق�ŏ�������Ȃ񂾂�
        AfterGiveDamage();
    }

    protected virtual void FixedUpdate()
    {
        if (!TargetTransform) { return; }
        
        Move?.Value.AddForce((TargetTransform.position - this.gameObject.transform.position).normalized * Time.fixedDeltaTime);
        this.gameObject.transform.LookAt(TargetTransform);
    }

    /// <summary>
    /// �_���[�W��^���I�������̋���
    /// </summary>
    protected abstract void AfterGiveDamage();

    /// <summary>
    /// �X�|�[�����I�������̋���
    /// </summary>
    protected abstract void AfterSpawn();

    /// <summary>
    /// ���񂾂Ƃ��̋���
    /// </summary>
    protected abstract void OnDeath();
}
