using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MainUIPresenter : MonoBehaviour
{
    [SerializeField] PlayerHPBarView playerHPBarView;
    [SerializeField] PlayerHPTextView playerHPTextView;
    [SerializeField] BulletReloadGageView bulletReloadGageView;
    [SerializeField] KillCountTextView killCountTextView;
    [SerializeField] DamageEffectView damageEffectView;
    [SerializeField] AnswerBoxSpawner[] answerBoxSpawners;

    [SerializeField] SerializeInterface<IStatus> playerStatus_model;
    [SerializeField] ScoreManager scoreManager_model;
    [SerializeField] GunManager gunManager_model;

    private void Start()
    {
        SetEvents();
        Bind();
    }

    private void SetEvents()
    {

    }

    private void Bind()
    {
        //�v���C���[HP �� HP�o�[
        playerStatus_model.Value.HPNormalized
            .Subscribe(playerHPBarView.OnChangeHP)
            .AddTo(this.gameObject);

        //�v���C���[HP �� �_���[�W����ʌ���
        playerStatus_model.Value.HPNormalized
            .Pairwise()
            .Where(pair => pair.Current < pair.Previous) // �������Ă���Ƃ��̂�
            .Subscribe(pair => damageEffectView.OnDamage(pair.Previous - pair.Current))
            .AddTo(this.gameObject);

        //�v���C���[HP �� HP�e�L�X�g
        playerStatus_model.Value.HP
            .Subscribe(playerHPTextView.OnChangeHP)
            .AddTo(this.gameObject);

        //�e�����[�h���� �� �����[�hUI
        gunManager_model.ReloadValue
            .Subscribe(bulletReloadGageView.OnChangeReloadValue)
            .AddTo(this.gameObject);

        //�L���J�E���g�X�R�A �� �L���J�E���g�e�L�X�g
        scoreManager_model.KillCountReactiveProperty
            .Subscribe(killCountTextView.OnChangeKillCount)
            .AddTo(this.gameObject);

        //�� �� �𓚃��O
        scoreManager_model.AnswerStatesReactiveCollection
            .ObserveAdd().Subscribe(addState => SpawnAnswerBox(addState.Value))
            .AddTo(this);
    }

    /// <summary>
    /// �����ɍ������X�|�i�[�ŃA���T�[�{�b�N�X���X�|�[��������
    /// </summary>
    /// <param name="answerState"></param>
    private void SpawnAnswerBox(AnswerState answerState)
    {
        foreach(AnswerBoxSpawner a in answerBoxSpawners)
        {
            if (a.CanSpawn(answerState)) 
            { 
                a.Spawn(answerState);
                break;
            }
        }
    }
}
