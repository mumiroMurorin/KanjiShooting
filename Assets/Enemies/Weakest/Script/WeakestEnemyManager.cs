using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class WeakestEnemyManager : EnemyManager , IAttachableItemOnDestroy, ISoundPlayable
{
    [SerializeField] Animator animator;

    [Header("アウトライン")]
    [SerializeField] OutlineSettings outlineSettings;
    [Header("アタック時の震え")]
    [SerializeField] ShakeSettings shakeSettings;

    [Header("音声")]
    [SerializeField] SoundPlayer soundPlayer;

    bool isMoving = false;
    IStatus playerStatus;

    // エネミーが死んだときのコールバック
    public Action<Transform> OnDestroyEvent { get; set; }

    private void Start()
    {
        Move?.Value.SetActive(false);
    }

    public override void GiveDamage(IStatus status)
    {
        //プレイヤーかチェック
        if (status.Layer != MobLayer.Player) { return; }

        //与ダメ前にアニメーション再生
        playerStatus = status;
        
        //攻撃アニメーション
        if (animator) { animator.SetTrigger("Attack"); }
    }

    protected override void AfterGiveDamage()
    {
        // 不正解レコードの追加
        StageManager.Instance.AddAnswerStatus(new AnswerStatus { questionData = questionData, state = AnswerState.Incorrected });

        //アニメーションがない時の消滅と与ダメ
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
    }

    protected override void OnDeath()
    {
        // 死んだときのコールバック発火
        OnDestroyEvent?.Invoke(TargetTransform);

        // 死ぬアニメーション
        if (animator) { animator.SetTrigger("Destroy"); }
        else { Destroy(this.gameObject); }
    }

    public override void Despawn()
    {
        //デスポーンアニメーション
        if (animator) { animator.SetTrigger("Despawn"); }
        else { Destroy(this.gameObject); }
    }

    private void SetOutline()
    {
        outlineSettings.ApplyOutline(kanjiObject.GameObject);
    }

    public void PlayOnShot(AudioClip audioClip)
    {
        ((ISoundPlayable)soundPlayer).PlayOnShot(audioClip);
    }

    protected override void FixedUpdate()
    {
        if (!TargetTransform) { return; }
        if (!isMoving) { return; }

        Move?.Value.AddForce((TargetTransform.position - this.gameObject.transform.position).normalized * Time.fixedDeltaTime);
        this.gameObject.transform.LookAt(TargetTransform);
    }

    //----------------アニメーション----------------

    /// <summary>
    /// 出現演出後に移動開始させるトリガー
    /// </summary>
    public void PermitMoveTrigger()
    {
        Move?.Value.SetActive(true);
        isMoving = true;
    }

    /// <summary>
    /// 攻撃時移動禁止させるトリガー
    /// </summary>
    public void ProhibitMoveTriggerAttacking()
    {
        isMoving = false;
    }

    /// <summary>
    /// 討伐時移動禁止させるトリガー
    /// </summary>
    public void ProhibitMoveTriggerDestroying()
    {
        Move?.Value.SetActive(false);
        isMoving = false;
    }


    /// <summary>
    /// 消滅のトリガー
    /// </summary>
    public void DestroyTrigger()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 与ダメトリガー
    /// </summary>
    public void GiveDamageTrigger()
    {
        playerStatus.SetHP(playerStatus.HP.Value - KanjiStatus.Value.Attack.Value);

        //ほんで消えたりなんだり
        AfterGiveDamage();
    }

    /// <summary>
    /// 攻撃時に体を震わせるトリガー
    /// </summary>
    public void VibrateTrigger()
    {
        shakeSettings.ApplyShake(this.transform);
    }
}
