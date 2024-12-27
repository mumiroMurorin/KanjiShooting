using System;
using UnityEngine;
using UniRx;
using StageTransition;
using VContainer;

public abstract class WaveManager : MonoBehaviour
{
    [SerializeField] protected float maxTime = 30f;
    [SerializeReference, SubclassSelector] protected IWaveStatus[] waveStatuses;

    //WAVE�I�����̃R�[���o�b�N
    private Subject<Unit> onEndWaveSubject = new Subject<Unit>();
    public IObservable<Unit> OnEndWaveAsObservable => onEndWaveSubject;

    //WAVE�I����
    private Subject<Unit> onEndWaveFinishingSubject = new Subject<Unit>();
    public IObservable<Unit> OnEndWaveFinishingAsObservable => onEndWaveFinishingSubject;

    //�e��X�e�[�^�X
    protected float TimeRatio => time / maxTime;
    protected Transform PlayerTransform;
    protected bool isWorking = false;
    protected float time;

    ScoreHolder scoreHolder;

    [Inject]
    public void Construct(ScoreHolder holder)
    {
        scoreHolder = holder;
    }

    /// <summary>
    /// ������
    /// </summary>
    public void Initialize(Transform playerTransform)
    {
        time = 0;
        //intervalCount = float.MaxValue;
        this.PlayerTransform = playerTransform;

        Bind();
        AfterInitialize();
    }

    protected abstract void AfterInitialize();

    private void Bind()
    {
        //�X�e�[�W���I�������Ƃ�Wave���~����
        StageManager.Instance.CurrentStageStatusreactiveproperty
            .Where(status => status == StageStatus.StageFinish)
            .Subscribe(status => { isWorking = false; })
            .AddTo(this.gameObject);
    }

    /// <summary>
    /// Wave�J�n���̏���
    /// </summary>
    public abstract void StartWave();

    /// <summary>
    /// Wave�I�����̏���
    /// </summary>
    public abstract void FinishWave();

    /// <summary>
    /// �G�̃X�|�[��
    /// </summary>
    protected abstract void SpawnEnemy();

    /// <summary>
    /// �A�b�v�f�[�g��̏���
    /// </summary>
    protected abstract void AfterUpdate();

    /// <summary>
    /// ���Ԃ̍X�V�ɔ�������
    /// </summary>
    protected virtual void Update()
    {
        if (!isWorking) { return; }

        //���Ԃ̉��Z
        time += Time.deltaTime;
        scoreHolder.AddTime(Time.deltaTime);

        // ���ꂼ���WaveStatus�̃J�E���g��i�߂�
        foreach(IWaveStatus waveStatus in waveStatuses)
        {
            waveStatus.CountTime(Time.deltaTime);
        }

        //���ꂼ��̏�����
        AfterUpdate();
    }

    /// <summary>
    /// WAVE�I����
    /// </summary>
    protected void EndOfWave()
    {
        //�R�[���o�b�N����
        onEndWaveSubject.OnNext(Unit.Default);
    }

    /// <summary>
    /// WAVE�I������������
    /// </summary>
    protected void EndOfWaveFinish()
    {
        //�R�[���o�b�N����
        onEndWaveFinishingSubject.OnNext(Unit.Default);
    }

    /// <summary>
    /// �G�l�~�[�̏������f�[�^�𐶐�
    /// </summary>
    /// <param name="kanjiObject"></param>
    /// <param name="questionData"></param>
    /// <returns></returns>
    protected EnemyInitializationData GenerateEnemyInitializationData()
    {
        EnemyInitializationData enemyInitializationData = new EnemyInitializationData
        {
            target = PlayerTransform,
            scoreHolder = this.scoreHolder
        };

        return enemyInitializationData;
    }
}