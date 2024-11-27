using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemBomb : MonoBehaviour , ISoundPlayable
{
    [SerializeField] float killDuration = 5f;
    [SerializeField] EnemyManager enemy;
    [SerializeField] BombController bombController;
    [SerializeField] GameObject bombObject;
    [Header("音声")]
    [SerializeField] AudioClip bombClip;
    [SerializeField] SoundPlayer soundPlayer;

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
        attachableItem.OnDestroyEvent += (target) => Bomb();
    }

    /// <summary>
    /// 爆破させる
    /// </summary>
    /// <param name="holder"></param>
    private void Bomb()
    {
        // 親子関係を外す
        this.transform.parent = null;

        // 爆破
        bombController.Bomb();
        bombObject.SetActive(false);
        PlayOnShot(bombClip);

        // 削除
        Destroy(this.gameObject, killDuration);
    }

    public void PlayOnShot(AudioClip audioClip)
    {
        ((ISoundPlayable)soundPlayer).PlayOnShot(audioClip);
    }
}
