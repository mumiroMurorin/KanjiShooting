using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[System.Serializable]
class HiraganaObject
{
    public char charcter;
    public GameObject obj;
}

public class YomiganaSpawner : MonoBehaviour, IYomiganaSpawner
{
    const int MAX_ANSWER_LENGTH = 15;

    [Header("文字大きさ(倍率)")]
    [SerializeField] float size;
    [Header("文字間隔")]
    [SerializeField] float interval;
    [Header("読み仮名オブジェクト")]
    [SerializeField] HiraganaObject[] yomiganaObjects;

    Dictionary<char, GameObject> dictionary;
    CharacterController[] spawnedCharacters;

    // 答え
    ReactiveProperty<string> _answer = new ReactiveProperty<string>("");
    public IReadOnlyReactiveProperty<string> Answer
    {
        get { return _answer; }
    }

    public Transform BulletTransform  { set; private get; }

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Initialize()
    {
        DestroyCharacterObjects();

        spawnedCharacters = new CharacterController[MAX_ANSWER_LENGTH];

        //ディクショナリーの登録
        dictionary = new Dictionary<char, GameObject>();
        foreach (HiraganaObject y in yomiganaObjects)
        {
            dictionary.Add(y.charcter, y.obj);
        }
    }

    /// <summary>
    /// 答えが変わったとき
    /// </summary>
    /// <param name="answer"></param>
    public void OnChangeAnswer(string answer)
    {
        DestroyCharacterObjects();
        _answer.Value = "";

        //1文字ずつ生成
        string tmp = "";
        for (int i = 0; i < answer.Length; i++)
        {
            //文字数チェック
            if (i >= MAX_ANSWER_LENGTH)
            {
                Debug.LogError("最大文字数に達しています");
                break;
            }

            char c = answer[i];
            tmp += c;
            spawnedCharacters[i] = SpawnYomiganaObject(c, i, answer.Length);
        }

        _answer.Value = tmp;
    }

    public void OnShoot()
    {
        spawnedCharacters = new CharacterController[MAX_ANSWER_LENGTH];
    }

    public void OnChargeStart()
    {
        if (spawnedCharacters == null) { return; }

        foreach (CharacterController c in spawnedCharacters)
        {
            if (c) { c.ChargeStart(); }
        }
    }

    public void OnChargeCancell()
    {
        if (spawnedCharacters == null) { return; }

        foreach (CharacterController c in spawnedCharacters)
        {
            if (c) { c.ChargeCancell(); }
        }
    }

    public void OnChargeComplete()
    {
        if (spawnedCharacters == null) { return; }

        foreach (CharacterController c in spawnedCharacters)
        {
            if (c) { c.ChargeComplete(); }
        }
    }

    /*
    /// <summary>
    /// 読み仮名の1文字削除
    /// </summary>
    /// <param name="index"></param>
    public void RemoveYomigana(int index)
    {
        if(yomigana.Length <= index) { Debug.LogError($"Out of Range: {index}"); }
        if(index < 0) { Debug.LogError($"Out of Range: {index}"); }
        yomigana.Remove(index, 1);


    }
    */

    /// <summary>
    /// ひらがなオブジェクトの出現(改善？)
    /// </summary>
    /// <param name="c"></param>
    private CharacterController SpawnYomiganaObject(char c, int index, int length)
    {
        GameObject prefab = GetYomiganaObject(c);
        if(prefab == null) { return null; }

        Vector3 pos = new Vector3((-(length - 1) / 2f + index) * interval, 0, 0);
        GameObject obj = Instantiate(prefab, pos, BulletTransform.rotation, BulletTransform);
        obj.transform.localScale *= size;
        CharacterController character = obj.GetComponent<CharacterController>();
        character.Move(pos);

        return character;
    }

    /// <summary>
    /// 指定された文字の平仮名オブジェクトを返す
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    private GameObject GetYomiganaObject(char c)
    {
        if (!dictionary.TryGetValue(c,out GameObject obj)) 
        { 
            Debug.Log($"平仮名オブジェクトが見つかりませんでした: {c}");
            return null;
        }

        return obj;
    }

    /// <summary>
    /// 読み仮名オブジェクトが存在したとき消し去る
    /// </summary>
    private void DestroyCharacterObjects()
    {
        if (spawnedCharacters == null) { return; }

        for (int i = 0; i < spawnedCharacters.Length; i++) 
        {
            CharacterController c = spawnedCharacters[i];
            if (c) { c.Destroy(); }
        }
    }
}
