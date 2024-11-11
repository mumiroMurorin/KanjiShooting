using System;
using UnityEngine;
using UniRx;
using StageTransition;

public abstract class WaveManager : MonoBehaviour
{
    [SerializeField] protected float maxTime = 30f;
    [SerializeField] protected Transform[] spawnPoints;

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
    protected float intervalCount;

    /// <summary>
    /// ������
    /// </summary>
    public void Initialize(Transform playerTransform)
    {
        time = 0;
        intervalCount = float.MaxValue;
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
        intervalCount += Time.deltaTime;
        ScoreManager.Instance.AddTime(Time.deltaTime);

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
            spawnPoint = SelectionSpawnpoint(),
            target = PlayerTransform,
        };

        return enemyInitializationData;
    }

    /// <summary>
    /// �X�|�[���|�C���g�̃����_���I�o (�ʃN���X�ɂ��ׂ�������U)
    /// </summary>
    /// <returns></returns>
    private Transform SelectionSpawnpoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
    }
}