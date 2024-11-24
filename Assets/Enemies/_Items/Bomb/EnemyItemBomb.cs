using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemBomb : MonoBehaviour
{
    [SerializeField] EnemyManager enemy;
    [SerializeField] BombController bombController;
    [SerializeField] GameObject bombObject; 
    [SerializeField] float killDuration = 5f;

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

        // �폜
        Destroy(this.gameObject, killDuration);
    }

}
