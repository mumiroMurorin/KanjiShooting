using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletController : MonoBehaviour, IDamager
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected SerializeInterface<IBulletStatus> status;

    protected bool isShooted;

    //読み仮名を橋渡し(正しい使い方かどうかは分からない…)
    public string Answer { set { status.Value.SetAnswer(value); } } 

    public abstract void Shoot();

    private void Start()
    {
        Initialize();
    }

    protected abstract void Initialize();

    /// <summary>
    /// 漢字オブジェクトにダメージを与える
    /// </summary>
    /// <param name="enemyStatus"></param>
    public void GiveDamage(IKanjiStatus enemyStatus)
    {
        // 発射済み？
        if (!isShooted) { return; }

        // 敵チェック
        if (enemyStatus.Layer != MobLayer.Enemy) { return; }

        bool isCollect = IsCollectAnswer(status.Value.Answer.Value, enemyStatus.Question.Value.answers);

        // 正答チェック
        if (!isCollect)
        {
            Sound.SoundManager.Instance.PlaySE(Sound.SE_Type.Incorrect);
            AfterBouncedBack();
            return;
        }
        else
        {
            Sound.SoundManager.Instance.PlaySE(Sound.SE_Type.Correct);
        }

        // 記録
        StageManager.Instance.AddAnswerStatus(new AnswerStatus { questionData = enemyStatus.Question.Value, state = AnswerState.Corrected });

        // HP削るよ
        enemyStatus.SetHP(enemyStatus.HP.Value - status.Value.Attack.Value);
        AfterKillEnemy();
    }

    /// <summary>
    /// オーバーロードしたので仕方なく実装
    /// </summary>
    /// <param name="enemyStatus"></param>
    public void GiveDamage(IStatus enemyStatus)
    {
        //敵チェック
        if (enemyStatus.Layer != MobLayer.Enemy) { return; }

        Debug.Log("漢字ステータスを無視して攻撃します");

        enemyStatus.SetHP(enemyStatus.HP.Value - status.Value.Attack.Value);
        AfterKillEnemy();
    }

    /// <summary>
    /// 答えが違って弾が跳ね返されたときの挙動
    /// </summary>
    protected virtual void AfterBouncedBack()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 敵を倒した後の挙動
    /// </summary>
    protected virtual void AfterKillEnemy()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 答えがあってるか返す
    /// </summary>
    /// <param name="answer1"></param>
    /// <param name="answer2"></param>
    /// <returns></returns>
    private bool IsCollectAnswer(string answer, string[] answer2)
    {
        foreach (string e_answer in answer2)
        {
            if (answer == e_answer) { return true; }
        }

        return false;
    }
}
