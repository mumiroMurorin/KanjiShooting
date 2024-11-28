using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BombController : MonoBehaviour, IDamager
{
    [SerializeField] SphereCollider bombCollider;
    [SerializeField] SerializeInterface<IStatus> status;
    [SerializeField] float bombSizeMax;
    [SerializeField] float bombExtendDuration;
    [SerializeField] float bombDuration;
    [SerializeField] float killDuration;

    private void Start()
    {
        bombCollider.enabled = false;
    }

    public void Bomb()
    {
        this.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence()
            .OnStart(() => { bombCollider.enabled = true; })
            .Append(bombCollider.gameObject.transform.DOScale(new Vector3(bombSizeMax, bombSizeMax, bombSizeMax), bombExtendDuration))
            .AppendInterval(bombDuration)
            .AppendCallback(() => { bombCollider.enabled = false; })
            .AppendInterval(killDuration)
            .AppendCallback(() => { Destroy(this.gameObject); });

        sequence.Play();
    }

    /// <summary>
    /// �����I�u�W�F�N�g�Ƀ_���[�W��^����
    /// </summary>
    /// <param name="enemyStatus"></param>
    public void GiveDamage(IKanjiStatus enemyStatus)
    {
        // �G�`�F�b�N
        if (enemyStatus.Layer != MobLayer.Enemy) { return; }

        // �L�^
        StageManager.Instance.AddAnswerStatus(new AnswerStatus { questionData = enemyStatus.Question.Value, state = AnswerState.CollateralDamage });

        // HP����
        enemyStatus.SetHP(enemyStatus.HP.Value - status.Value.Attack.Value);
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
    }
}
