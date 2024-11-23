using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kanji;

public abstract class EnemyManager : MonoBehaviour, IEnemy, IDamager
{
    [SerializeField] protected SerializeInterface<IForceAddable> Move;
    [SerializeField] protected SerializeInterface<IKanjiStatus> KanjiStatus;
    [SerializeField] protected Transform kanjiTransform;
    
    protected GameObject kanjiObject;
    protected BoxCollider kanjiCollider;
    protected Transform TargetTransform;
    protected QuestionData questionData;
    protected ScoreHolder scoreHolder;

    /// <summary>
    /// HPが変わったときの処理
    /// </summary>
    /// <param name="hp"></param>
    public virtual void OnChangeHP(int hp)
    {
        if(hp <= 0)
        {
            scoreHolder.AddAnswerState(new AnswerState { questionData = questionData, isCorrect = true });
            scoreHolder.IncrementKillCount(); // キル数をプラス
            kanjiCollider.enabled = false;
            OnDeath();
        }
    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="initialData"></param>
    public void Initialize(EnemyInitializationData initialData)
    {
        TargetTransform = initialData.target;
        questionData = initialData.questionData;
        scoreHolder = initialData.scoreHolder;
        KanjiStatus.Value.SetAnswers(initialData.questionData.answers);

        SetKanjiObject(initialData.kanjiObject);
        AfterSpawn();
    }

    /// <summary>
    /// 漢字オブジェクトのセット
    /// </summary>
    /// <param name="kanjiObject"></param>
    private void SetKanjiObject(GameObject kanjiObject)
    {
        this.kanjiObject = kanjiObject;
        kanjiCollider = kanjiObject.GetComponentInChildren<BoxCollider>();
        kanjiObject.transform.position = kanjiTransform.position;
        kanjiObject.transform.SetParent(kanjiTransform);
    }

    /// <summary>
    /// デスポーン(消失)
    /// </summary>
    public virtual void Despawn() 
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// プレイヤーにダメージを与える
    /// </summary>
    /// <param name="status"></param>
    public virtual void GiveDamage(IStatus status)
    {
        //プレイヤーかチェック
        if(status.Layer != MobLayer.Player) { return; }

        //ダメージを食らわせる
        status.SetHP(status.HP.Value - KanjiStatus.Value.Attack.Value);

        //ほんで消えたりなんだり
        AfterGiveDamage();
    }

    public void GiveDamage(IKanjiStatus kanjiStatus)
    {
        //プレイヤーかチェック
        if (kanjiStatus.Layer != MobLayer.Player) { return; }

        Debug.Log("漢字の読み関係ないのに呼び出されています");

        //ダメージを食らわせる
        kanjiStatus.SetHP(kanjiStatus.HP.Value - KanjiStatus.Value.Attack.Value);

        //ほんで消えたりなんだり
        AfterGiveDamage();
    }

    protected virtual void FixedUpdate()
    {
        if (!TargetTransform) { return; }
        
        Move?.Value.AddForce((TargetTransform.position - this.gameObject.transform.position).normalized * Time.fixedDeltaTime);
        this.gameObject.transform.LookAt(TargetTransform);
    }

    /// <summary>
    /// ダメージを与え終わった後の挙動
    /// </summary>
    protected abstract void AfterGiveDamage();

    /// <summary>
    /// スポーンし終わった後の挙動
    /// </summary>
    protected abstract void AfterSpawn();

    /// <summary>
    /// 死んだときの挙動
    /// </summary>
    protected abstract void OnDeath();
}
