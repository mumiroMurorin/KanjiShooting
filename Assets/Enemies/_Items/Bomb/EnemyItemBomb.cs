using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemBomb : MonoBehaviour , ISoundPlayable
{
    [SerializeField] float killDuration = 5f;
    [SerializeField] EnemyManager enemy;
    [SerializeField] BombController bombController;
    [SerializeField] GameObject bombObject;
    [Header("����")]
    [SerializeField] AudioClip bombClip;
    [SerializeField] SoundPlayer soundPlayer;

    IAttachableItemOnDestroy attachableItem;

    private void Start()
    {
        // EnemyManager���C���^�[�t�F�[�X���p�����Ė���������
        if(!enemy.transform.TryGetComponent<IAttachableItemOnDestroy>(out attachableItem))
        {
            Debug.LogWarning($"�yEnemy�z�C���^�[�t�F�[�X IAttachableItemOnDestroy �� {enemy.gameObject.name} �ɂ���܂���");
            return;
        }

        SetEvent();
    }

    private void SetEvent()
    {
        attachableItem.OnDestroyEvent += (target) => Bomb();
    }

    /// <summary>
    /// ���j������
    /// </summary>
    /// <param name="holder"></param>
    private void Bomb()
    {
        // �e�q�֌W���O��
        this.transform.parent = null;

        // ���j
        bombController.Bomb();
        bombObject.SetActive(false);
        PlayOnShot(bombClip);

        // �폜
        Destroy(this.gameObject, killDuration);
    }

    public void PlayOnShot(AudioClip audioClip)
    {
        ((ISoundPlayable)soundPlayer).PlayOnShot(audioClip);
    }
}
