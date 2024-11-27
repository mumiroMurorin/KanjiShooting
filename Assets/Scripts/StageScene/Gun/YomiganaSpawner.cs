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

    [Header("�����傫��(�{��)")]
    [SerializeField] float size;
    [Header("�����Ԋu")]
    [SerializeField] float interval;
    [Header("�ǂ݉����I�u�W�F�N�g")]
    [SerializeField] HiraganaObject[] yomiganaObjects;

    Dictionary<char, GameObject> dictionary;
    CharacterController[] spawnedCharacters;

    // ����
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
    /// ������
    /// </summary>
    private void Initialize()
    {
        DestroyCharacterObjects();

        spawnedCharacters = new CharacterController[MAX_ANSWER_LENGTH];

        //�f�B�N�V���i���[�̓o�^
        dictionary = new Dictionary<char, GameObject>();
        foreach (HiraganaObject y in yomiganaObjects)
        {
            dictionary.Add(y.charcter, y.obj);
        }
    }

    /// <summary>
    /// �������ς�����Ƃ�
    /// </summary>
    /// <param name="answer"></param>
    public void OnChangeAnswer(string answer)
    {
        DestroyCharacterObjects();
        _answer.Value = "";

        //1����������
        string tmp = "";
        for (int i = 0; i < answer.Length; i++)
        {
            //�������`�F�b�N
            if (i >= MAX_ANSWER_LENGTH)
            {
                Debug.LogError("�ő啶�����ɒB���Ă��܂�");
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
    /// �ǂ݉�����1�����폜
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
    /// �Ђ炪�ȃI�u�W�F�N�g�̏o��(���P�H)
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
    /// �w�肳�ꂽ�����̕������I�u�W�F�N�g��Ԃ�
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    private GameObject GetYomiganaObject(char c)
    {
        if (!dictionary.TryGetValue(c,out GameObject obj)) 
        { 
            Debug.Log($"�������I�u�W�F�N�g��������܂���ł���: {c}");
            return null;
        }

        return obj;
    }

    /// <summary>
    /// �ǂ݉����I�u�W�F�N�g�����݂����Ƃ���������
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
