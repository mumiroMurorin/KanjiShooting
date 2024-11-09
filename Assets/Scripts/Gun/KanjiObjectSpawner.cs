using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kanji;

public class KanjiObjectSpawner : MonoBehaviour
{
    [SerializeField] float objectSize = 50;
    [SerializeField] float objectDepth = 20;
    [SerializeField] int fontNumber = 0;
    [SerializeField] Material kanjiMaterial;
    [SerializeField] Material kanjiEdgeMaterial;
    [SerializeField] bool isZaxisReversal = true;

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// èâä˙âª
    /// </summary>
    /// <param name="target"></param>
    public void Initialize()
    {
        FlyingText.defaultMaterial = kanjiMaterial;
        FlyingText.defaultEdgeMaterial = kanjiEdgeMaterial;
        FlyingText.defaultFont = fontNumber;
        FlyingText.defaultSize = objectSize;
        FlyingText.defaultDepth = objectDepth;
    }

    /// <summary>
    /// äøéöÇÃèoåª
    /// </summary>
    /// <param name="pos"></param>
    public GameObject SpawnKanji(QuestionData data)
    {
        GameObject parent = new GameObject(data.kanji);
        GameObject kanji = FlyingText.GetObject(data.kanji);
        kanji.transform.SetParent(parent.transform);
        kanji.transform.position = parent.transform.position;
        kanji.transform.rotation = isZaxisReversal ? parent.transform.rotation * Quaternion.Euler(0, 180f, 0f) : parent.transform.rotation;

        return parent;
    }

    /// <summary>
    /// äøéöÇÃèoåª
    /// </summary>
    /// <param name="pos"></param>
    public GameObject SpawnKanji(QuestionData data, Transform parent)
    {
        GameObject obj = FlyingText.GetObject(data.kanji);
        obj.transform.position = parent.position;
        obj.transform.rotation = isZaxisReversal ? parent.rotation * Quaternion.Euler(0, 180f, 0f) : parent.rotation;
        obj.transform.SetParent(parent);

        return obj;
    }
}
