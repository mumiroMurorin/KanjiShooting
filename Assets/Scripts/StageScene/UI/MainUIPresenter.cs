using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using VContainer;
using StageUI;

public class MainUIPresenter : MonoBehaviour
{
    [SerializeField] PlayerHPBarView playerHPBarView;
    [SerializeField] PlayerHPTextView playerHPTextView;
    [SerializeField] BulletReloadGageView bulletReloadGageView;
    [SerializeField] SpecialChargeGaugeView specialChargeGaugeView;
    [SerializeField] KillCountTextView killCountTextView;
    [SerializeField] StageDataView stageDataView;
    [SerializeField] WaveTextView waveTextView;
    [SerializeField] TimerTextView timerTextView;
    [SerializeField] DamageEffectView damageEffectView;
    [SerializeField] PreviousWaveView previousWaveView;
    [SerializeField] NextWaveView nextWaveView;
    [SerializeField] AnswerBoxSpawner[] answerBoxSpawners;

    [SerializeField] SerializeInterface<IStatus> playerStatus_model;
    [SerializeField] GunManager gunManager_model;
    [SerializeField] SerializeInterface<IBulletReloadCharger> bulletReloadCharger_model;

    ScoreHolder scoreHolder;

    [Inject]
    public void Construct(ScoreHolder holder)
    {
        scoreHolder = holder;
    }

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
        // �v���C���[HP �� HP�o�[
        playerStatus_model?.Value.HPNormalized
            .Subscribe(playerHPBarView.OnChangeHP)
            .AddTo(this.gameObject);

        // �v���C���[HP �� �_���[�W����ʌ���
        playerStatus_model?.Value.HPNormalized
            .Pairwise()
            .Where(pair => pair.Current < pair.Previous) // �������Ă���Ƃ��̂�
            .Subscribe(pair => damageEffectView.OnDamage(pair.Previous - pair.Current))
            .AddTo(this.gameObject);

        // �v���C���[HP �� HP�e�L�X�g
        playerStatus_model?.Value.HP
            .Subscribe(playerHPTextView.OnChangeHP)
            .AddTo(this.gameObject);

        // �v���C���[HP �� HP�e�L�X�g�F
        playerStatus_model?.Value.HPNormalized
            .Subscribe(playerHPTextView.OnChangeHP)
            .AddTo(this.gameObject);

        // �e�����[�h���� �� �����[�hUI
        gunManager_model?.ReloadValue
            .Subscribe(bulletReloadGageView.OnChangeReloadValue)
            .AddTo(this.gameObject);

        // �X�y�V�����e�`���[�W���� �� �`���[�WUI
        bulletReloadCharger_model?.Value.ChargeCount
            .Subscribe(specialChargeGaugeView.OnChangeChargeValue)
            .AddTo(this.gameObject);

        // �L���J�E���g�X�R�A �� �L���J�E���g�e�L�X�g
        scoreHolder.KillCountReactiveProperty
            .Subscribe(killCountTextView.OnChangeKillCount)
            .AddTo(this.gameObject);

        // �X�e�[�W�f�[�^ �� �X�e�[�W�^�C�g����
        scoreHolder.StageDataReactiveProperty
            .Subscribe(stageDataView.OnChangeStageData)
            .AddTo(this.gameObject);

        // Wave�J�E���g �� �o��Wave�e�L�X�g
        scoreHolder.WaveCountReactiveProperty
            .Subscribe(waveTextView.OnChangeWaveCount)
            .AddTo(this.gameObject);

        scoreHolder.WaveCountReactiveProperty
            .Subscribe(count => 
            {
                previousWaveView.OnChangePreviousWave(count);
                nextWaveView.OnChangePreviousWave(count);
            })
            .AddTo(this.gameObject);

        // �o�ߎ��� �� �^�C�}�[�e�L�X�g
        scoreHolder.TimeCountReactiveProperty
            .Subscribe(timerTextView.OnChangeTimeCount)
            .AddTo(this.gameObject);

        // �� �� �𓚃��O
        scoreHolder.AnswerStatesReactiveCollection
            .ObserveAdd().Subscribe(addState => SpawnAnswerBox(addState.Value))
            .AddTo(this);
    }

    /// <summary>
    /// �����ɍ������X�|�i�[�ŃA���T�[�{�b�N�X���X�|�[��������
    /// </summary>
    /// <param name="answerState"></param>
    private void SpawnAnswerBox(AnswerStatus answerStatus)
    {
        foreach(AnswerBoxSpawner a in answerBoxSpawners)
        {
            if (a.CanSpawn(answerStatus)) 
            { 
                a.Spawn(answerStatus);
                break;
            }
        }
    }
}
