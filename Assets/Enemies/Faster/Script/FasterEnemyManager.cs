using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityFx.Outline;

public class FasterEnemyManager : EnemyManager
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

    bool isMoving = false;
    IStatus playerStatus;

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
        //レコードの追加
        ScoreManager.Instance.AddAnswerState(new AnswerState { questionData = questionData, isCorrect = false });

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
        SetWheel();
    }

    protected override void OnDeath()
    {
        // 体の震えを止める
        shakeSettingsOnMove.StopShake();

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
        outlineSettings.ApplyOutline(kanjiObject);
    }

    /// <summary>
    /// タイヤの位置を調整
    /// </summary>
    private void SetWheel()
    {
        float center = kanjiCollider.center.x;
        float width = kanjiCollider.size.x;

        foreach(GameObject obj in wheelsRight)
        {
            obj.transform.localPosition = new Vector3(center + width / 2f, obj.transform.localPosition.y, obj.transform.localPosition.z);
        }

        foreach (GameObject obj in wheelsLeft)
        {
            obj.transform.localPosition = new Vector3(center - width / 2f, obj.transform.localPosition.y, obj.transform.localPosition.z);
        }
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
    /// 移動禁止させるトリガー
    /// </summary>
    public void ProhibitMoveTrigger()
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
