using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletController : MonoBehaviour, IDamager
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected SerializeInterface<IKanjiStatus> KanjiStatus;

    protected bool isShooted;

    //読み仮名を橋渡し(正しい使い方かどうかは分からない…)
    public string Yomigana { set { KanjiStatus.Value.SetAnswers(new string[]{ value }); } } 

    public abstract void Shoot();

    /// <summary>
    /// 漢字オブジェクトにダメージを与える
    /// </summary>
    /// <param name="enemyStatus"></param>
    public void GiveDamage(IKanjiStatus enemyStatus)
    {
        //発射済み？
        if (!isShooted) { return; }

        //敵チェック
        if (enemyStatus.Layer != MobLayer.Enemy) { return; }

        //正答チェック
        bool isCollect = IsCollectAnswer(KanjiStatus.Value.Answers.Value, enemyStatus.Answers.Value);
        if (!isCollect)
        { AfterBouncedBack(); return; }

        //ログの生成
        string log = isCollect ? "【解答】〇正解 回答:" : "【解答】×不正解 回答:";
        foreach (string str in KanjiStatus.Value.Answers.Value) { log += $"{str},"; }
        log += " 正答:";
        foreach (string str in enemyStatus.Answers.Value) { log += $"{str},"; }
        Debug.Log(log);

        //HP削るよ
        enemyStatus.SetHP(enemyStatus.HP.Value - KanjiStatus.Value.Attack.Value);
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

        enemyStatus.SetHP(enemyStatus.HP.Value - KanjiStatus.Value.Attack.Value);
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
    private bool IsCollectAnswer(string[] answer1, string[] answer2)
    {
        foreach (string b_answer in answer1)
        {
            foreach (string e_answer in answer2)
            {
                if (b_answer == e_answer) { return true; }
            }
        }
        return false;
    }
}
