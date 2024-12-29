using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IEnemy
{
    public abstract void Initialize(EnemyInitializationData initialData);

    public abstract void Despawn();
}

public interface IAttachableItemOnDestroy
{
    public Action<Transform> OnDestroyEvent { get; set; }
}

/// <summary>
/// 漢字オブジェクトの編集を行える
/// </summary>
public interface IKanjiObject
{
    public void SetGameObject(GameObject obj);

    public void SetMaterials(KanjiMaterial kanjiMaterial);

    public void SetTransform(Transform transform);

    public BoxCollider KanjiCollider { get; }

    public GameObject GameObject { get; }
}

[Serializable]
public class KanjiMaterial
{
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material edgeMaterial;

    /// <summary>
    /// 漢字オブジェクトの正背面、側面にマテリアルをセット
    /// </summary>
    /// <param name="meshRenderer"></param>
    public void SetMaterial(MeshRenderer meshRenderer)
    {
        // 現在のマテリアル配列を取得
        Material[] materials = meshRenderer.materials;

        if (materials.Length > 0) { materials[0] = defaultMaterial; }
        if (materials.Length > 1) { materials[1] = edgeMaterial; }
        meshRenderer.materials = materials;
    }
}