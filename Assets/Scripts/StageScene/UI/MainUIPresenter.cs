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
    [SerializeField] ExplosionChargeGageView explosionChargeGageView;
    [SerializeField] SpecialChargeGaugeView specialChargeGaugeView;
    [SerializeField] SpecialActionGuideView specialActionGuideView;
    [SerializeField] KillCountTextView killCountTextView;
    [SerializeField] StageDataView stageDataView;
    [SerializeField] WaveTextView waveTextView;
    [SerializeField] TimerTextView timerTextView;
    [SerializeField] DamageEffectView damageEffectView;
    [SerializeField] PreviousWaveView previousWaveView;
    [SerializeField] NextWaveView nextWaveView;
    [SerializeField] BackGameButtonView backGameButtonView;
    [SerializeField] BackMainMenuButtonView backMainMenuButtonView;
    [SerializeField] AnswerBoxSpawner[] answerBoxSpawners;

    [SerializeField] SerializeInterface<IStatus> playerStatus_model;
    [SerializeField] GunManager gunManager_model;
    [SerializeField] SerializeInterface<IBulletReloadCharger> bulletReloadCharger_model;
    [SerializeField] SerializeInterface<IBulletShootCharger> explosionCharger_model;
    [SerializeField] SerializeInterface<IJapaneseInputHolder> inputHolder;

    ScoreHolder scoreHolder;
    OptionHolder optionHolder;

    [Inject]
    public void Construct(ScoreHolder scoreHolder, OptionHolder optionHolder)
    {
        this.scoreHolder = scoreHolder;
        this.optionHolder = optionHolder;
    }

    private void Start()
    {
        SetEvents();
        Bind();
    }

    private void SetEvents()
    {
        // �u�Q�[���ɖ߂�v�{�^��
        backGameButtonView.OnBackGameButtonClickedListener += () => StageManager.Instance.ResumeTime();

        // �u�X�e�[�W���f�v�{�^��
        backMainMenuButtonView.OnBackMainMenuButtonClickedListener += () => OnBackMainMenuButtonPushed();
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

        // �����`���[�W���� �� �`���[�WUI
        explosionCharger_model?.Value.ChargeCount
            .Subscribe(explosionChargeGageView.OnChangeChargeValue)
            .AddTo(this.gameObject);

        // �X�y�V�����e�`���[�W �� �`���[�WUI
        bulletReloadCharger_model?.Value.ChargeCount
            .Subscribe(specialChargeGaugeView.OnChangeChargeValue)
            .AddTo(this.gameObject);

        // �X�y�V�����e�`���[�W �� �A�N�V�����K�C�hUI
        bulletReloadCharger_model?.Value.ChargeCount
            .Subscribe(specialActionGuideView.OnChangeChargeValue)
            .AddTo(this.gameObject);

        inputHolder.Value.AnswerReactiveProperty
            .Subscribe(answer => specialActionGuideView.OnChangeActionMode(answer.Length > 0))
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

        // Wave�J�E���g �� Wave�J�ڃA�j���[�V����
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
        if (!optionHolder.AnswerDisplayValidityReactiveProperty.Value) { return; }
        
        answerStatus.answerDisplayTime = optionHolder.AnswerDispleyTime.Value;
        foreach (AnswerBoxSpawner a in answerBoxSpawners)
        {
            if (a.CanSpawn(answerStatus)) 
            { 
                a.Spawn(answerStatus);
                break;
            }
        }
    }

    /// <summary>
    /// �u���C�����j���[�ɖ߂�v�{�^�����N���b�N���ꂽ�Ƃ�
    /// </summary>
    private void OnBackMainMenuButtonPushed()
    {
        StageManager.Instance.ChangeStageScene(StageTransition.StageSceneTag.Interrupt);
    }

}
