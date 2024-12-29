using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kanji;
using System.Threading;
using Cysharp.Threading.Tasks;

public class WaveManagerStageChangable : WaveManager
{
    [SerializeField] KanjiObjectSpawner kanjiSpawner;
    [SerializeReference, SubclassSelector] IQuestionSelector questionSelector;
    [SerializeField] AudioClip bgm;

    [Header("�X�e�[�W�J�ڂ܂ł̑҂�����")]
    [SerializeField] float waitingTime;

    [SerializeField] SerializeInterface<IStageChanger> stageChanger;

    CancellationTokenSource cts;

    /// <summary>
    /// ������
    /// </summary>
    protected override void AfterInitialize()
    {
        questionSelector.Initialize();
        cts = new CancellationTokenSource();

        //�X�|�i�[�̏�����
        foreach (IWaveStatus w in waveStatuses)
        {
            w.Initialize(questionSelector, kanjiSpawner);
        }
    }

    /// <summary>
    /// Wave�J�n
    /// </summary>
    public override void StartWave()
    {
        isWorking = true;
        Sound.SoundManager.Instance.PlayBGM(bgm); //BGM�̍Đ�
    }

    /// <summary>
    /// Wave�I��
    /// </summary>
    public override void FinishWave()
    {
        isWorking = false;

        // BGM�̒�~
        Sound.SoundManager.Instance.StopBGM(true);
        // �S�Ă̓G���f�X�|�[��������
        foreach (IWaveStatus wave in waveStatuses) { wave.DespawnEnemy(); }

        // �X�e�[�W�ύX
        ChangeStage(cts.Token).Forget();
    }

    protected override void SpawnEnemy()
    {
        if (questionSelector == null) { return; }

        foreach(IWaveStatus waveStatus in waveStatuses)
        {
            waveStatus.SpawnEnemy(TimeRatio, GenerateEnemyInitializationData());
        }
    }

    protected override void AfterUpdate()
    {
        SpawnEnemy();

        //Wave�I��
        if (time > maxTime)
        {
            //�Đ���~
            //Sound.SoundManager.Instance.StopBGM(true);
            EndOfWave();
        }
    }

    /// <summary>
    /// �X�e�[�W�J�ڂ̔񓯊�����(���b�v)
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTaskVoid ChangeStage(CancellationToken token)
    {
        // �w�肳�ꂽ�b���҂�
        await UniTask.WaitForSeconds(waitingTime, cancellationToken: token);

        // �X�e�[�W�ύX�J�n
        stageChanger.Value.ChangeStage(EndOfWaveFinish);
    }

    private void OnDestroy()
    {
        cts.Cancel();
        cts.Dispose();
    }
}