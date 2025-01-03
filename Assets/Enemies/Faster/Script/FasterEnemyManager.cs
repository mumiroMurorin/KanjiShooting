using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityFx.Outline;
using System;

public class FasterEnemyManager : EnemyManager, IAttachableItemOnDestroy, ISoundPlayable
{
    [SerializeField] Animator animator;

    [Header("タイヤ")]
    [SerializeField] GameObject[] wheelsRight;
    [SerializeField] GameObject[] wheelsLeft;

    [Header("アウトライン")]
    [SerializeField] OutlineSettings outlineSettings;
    [Header("アタック時の震え")]
    [SerializeField] ShakeSettings shakeSettingsOnAttack;

    [Header("移動時車体の揺れ")]
    [SerializeField] ShakeSettings shakeSettingsOnMove;

    [Header("音声")]
    [SerializeField] SoundPlayer soundPlayer;

    bool isMoving = false;
    IStatus playerStatus;

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

        // 体の震えを止める
        shakeSettingsOnMove.StopShake();

        //攻撃アニメーション
        if (animator) { animator.SetTrigger("Attack"); }
    }

    protected override void AfterGiveDamage()
    {
        // 不正解レコードの追加
        StageManager.Instance.AddAnswerStatus(new AnswerStatus { questionData = questionData, state = AnswerState.Incorrected });

        // アニメーションがない時の消滅と与ダメ
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
        SetWheel();
    }

    protected override void OnDeath()
    {
        // 体の震えを止める
        shakeSettingsOnMove.StopShake();

        // アイテム効果発動
        OnDestroyEvent?.Invoke(TargetTransform);

        //死ぬアニメーション
        if (animator) { animator.SetTrigger("Destroy"); }
        else { Destroy(this.gameObject); }
    }

    public override void Despawn()
    {
        // 体の震えを止める
        shakeSettingsOnMove.StopShake();

        //デスポーンアニメーション
        if (animator) { animator.SetTrigger("Despawn"); }
        else { Destroy(this.gameObject); }
    }

    private void SetOutline()
    {
        outlineSettings.ApplyOutline(kanjiObject.GameObject);
    }

    /// <summary>
    /// タイヤの位置を調整
    /// </summary>
    private void SetWheel()
    {
        float center = kanjiObject.KanjiCollider.center.x;
        float width = kanjiObject.KanjiCollider.size.x;

        foreach(GameObject obj in wheelsRight)
        {
            obj.transform.localPosition = new Vector3(center + width / 2f, obj.transform.localPosition.y, obj.transform.localPosition.z);
        }

        foreach (GameObject obj in wheelsLeft)
        {
            obj.transform.localPosition = new Vector3(center - width / 2f, obj.transform.localPosition.y, obj.transform.localPosition.z);
        }
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

    private void OnDestroy()
    {
        shakeSettingsOnAttack.StopShake();
        shakeSettingsOnMove.StopShake();
    }

    //----------------アニメーション----------------

    /// <summary>
    /// 出現演出後に移動開始させるトリガー
    /// </summary>
    public void PermitMoveTrigger()
    {
        Move?.Value.SetActive(true);
        isMoving = true;
        shakeSettingsOnMove.ApplyShake(kanjiTransform);
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
        shakeSettingsOnAttack.ApplyShake(this.transform);
    }
}
