using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletController : MonoBehaviour, IDamager
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected SerializeInterface<IKanjiStatus> KanjiStatus;

    protected bool isShooted;

    //�ǂ݉��������n��(�������g�������ǂ����͕�����Ȃ��c)
    public string Yomigana { set { KanjiStatus.Value.SetAnswers(new string[]{ value }); } } 

    public abstract void Shoot();

    private void Start()
    {
        Initialize();
    }

    protected abstract void Initialize();

    /// <summary>
    /// �����I�u�W�F�N�g�Ƀ_���[�W��^����
    /// </summary>
    /// <param name="enemyStatus"></param>
    public void GiveDamage(IKanjiStatus enemyStatus)
    {
        // ���ˍς݁H
        if (!isShooted) { return; }

        // �G�`�F�b�N
        if (enemyStatus.Layer != MobLayer.Enemy) { return; }

        bool isCollect = IsCollectAnswer(KanjiStatus.Value.Answers.Value, enemyStatus.Answers.Value);

        // ���O�̐���
        string log = isCollect ? "�y�𓚁z�Z���� ��:" : "�y�𓚁z�~�s���� ��:";
        log += $"{string.Join(",", KanjiStatus.Value.Answers.Value)}";
        log += $" ����: {string.Join(",", enemyStatus.Answers.Value)}";
        Debug.Log(log);

        // �����`�F�b�N
        if (!isCollect)
        { AfterBouncedBack(); return; }

        // HP����
        enemyStatus.SetHP(enemyStatus.HP.Value - KanjiStatus.Value.Attack.Value);
        AfterKillEnemy();
    }

    /// <summary>
    /// �I�[�o�[���[�h�����̂Ŏd���Ȃ�����
    /// </summary>
    /// <param name="enemyStatus"></param>
    public void GiveDamage(IStatus enemyStatus)
    {
        //�G�`�F�b�N
        if (enemyStatus.Layer != MobLayer.Enemy) { return; }

        Debug.Log("�����X�e�[�^�X�𖳎����čU�����܂�");

        enemyStatus.SetHP(enemyStatus.HP.Value - KanjiStatus.Value.Attack.Value);
        AfterKillEnemy();
    }

    /// <summary>
    /// ����������Ēe�����˕Ԃ��ꂽ�Ƃ��̋���
    /// </summary>
    protected virtual void AfterBouncedBack()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// �G��|������̋���
    /// </summary>
    protected virtual void AfterKillEnemy()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// �����������Ă邩�Ԃ�
    /// </summary>
    /// <param name="answer1"></param>
    /// <param name="answer2"></param>
    /// <returns></returns>
    private bool IsCollectAnswer(string[] answer1, string[] answer2)
    {
        foreach (string b_answer in answer1)
        {
            foreach (string e_answer in answer2)
            {
                if (b_answer == e_answer) { return true; }
            }
        }
        return false;
    }
}
