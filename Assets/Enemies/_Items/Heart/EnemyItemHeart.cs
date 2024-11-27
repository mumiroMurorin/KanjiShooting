using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyItemHeart : MonoBehaviour, IDamager, ISoundPlayable
{
    [SerializeField] EnemyManager enemy;
    [SerializeField] GameObject heartObject;
    [SerializeField] Animator anim;
    [SerializeField] int recoverAmount = 30;
    [SerializeField] float moveDuration = 0.75f;
    [Header("音声")]
    [SerializeField] AudioClip activeClip;
    [SerializeField] SoundPlayer soundPlayer;

    bool isRecoverPermit;
    Transform playerTransform;
    IAttachableItemOnDestroy attachableItem;

    private void Start()
    {
        // EnemyManagerがインターフェースを継承して無かった時
        if(!enemy.transform.TryGetComponent<IAttachableItemOnDestroy>(out attachableItem))
        {
            Debug.LogWarning($"【Enemy】インターフェース IAttachableItemOnDestroy が {enemy.gameObject.name} にありません");
            return;
        }

        SetEvent();
    }

    private void SetEvent()
    {
        attachableItem.OnDestroyEvent += Heal;
    }

    /// <summary>
    /// 回復する
    /// </summary>
    /// <param name="holder"></param>
    private void Heal(Transform playerTransform)
    {
        // 親子関係を外す
        this.transform.parent = null;

        isRecoverPermit = true;
        this.playerTransform = playerTransform;
        PlayOnShot(activeClip);

        anim.SetTrigger("Active");
    }

    /// <summary>
    /// プレイヤーまで移動する
    /// </summary>
    /// <param name="target">目的地のTransform</param>
    /// <param name="duration">移動にかける時間（秒）</param>
    public void MoveToPlayer()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Target Transform is null. Aborting movement.");
            return;
        }

        transform.DOMove(playerTransform.position, moveDuration)
                 .SetEase(Ease.Linear);
    }

    void IDamager.GiveDamage(IStatus status)
    {
        // プレイヤーかチェック
        if (status.Layer != MobLayer.Player) { return; }
        if (!isRecoverPermit) { return; }

        // 回復させる
        status.SetHP(status.HP.Value + recoverAmount);

        // ほんで消える
        Destroy(this.gameObject);
    }

    void IDamager.GiveDamage(IKanjiStatus status)
    {
        // プレイヤーかチェック
        if (status.Layer != MobLayer.Player) { return; }
        if (!isRecoverPermit) { return; }

        Debug.Log("漢字の読み関係ないのに呼び出されています");

        // 回復させる
        status.SetHP(status.HP.Value + recoverAmount);

        // ほんで消える
        Destroy(this.gameObject);
    }

    public void PlayOnShot(AudioClip audioClip)
    {
        ((ISoundPlayable)soundPlayer).PlayOnShot(audioClip);
    }
}
