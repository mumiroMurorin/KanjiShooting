using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanjiObjectGeneral : MonoBehaviour, IKanjiObject
{
    public BoxCollider KanjiCollider { get; private set; }

    public GameObject GameObject { get; private set; }

    public void SetGameObject(GameObject obj)
    {
        GameObject = obj;

        // �R���C�_�[�̎擾
        if (obj.TryGetComponent(out BoxCollider collider)) { KanjiCollider = collider; }
    }

    public void SetMaterials(KanjiMaterial kanjiMaterial)
    {
        // �}�e���A���̐ݒ�
        if(GameObject.TryGetComponent(out MeshRenderer meshRenderer)) { kanjiMaterial.SetMaterial(meshRenderer); }
    }

    public void SetTransform(Transform transform)
    {
        this.gameObject.transform.SetParent(transform);
        this.gameObject.transform.position = transform.position;
        this.gameObject.transform.rotation = transform.rotation;
        this.gameObject.transform.localScale = transform.localScale;
    }
}
