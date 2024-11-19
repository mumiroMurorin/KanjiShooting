using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/StageData", fileName = "StageData")]
/// <summary>
/// ステージの詳細データ
/// </summary>
public class StageDetailData : ScriptableObject
{
    [SerializeField] string stageTitle;
    public string StageTitle { get { return stageTitle; } }

    [Header("難易度")]
    [SerializeField] int difficulty;
    public int Difficulty { get { return difficulty; } }

    [Header("ジャンル")]
    [SerializeField] string genre;
    public string Genre { get { return genre; } }

    [SerializeField] Sprite stageItemBackSprite;
    public Sprite StageItemBackSprite { get { return stageItemBackSprite; } }

    [SerializeField] Sprite backGroundSprite;
    public Sprite BackGroundSprite { get { return backGroundSprite; } }

    [Header("ステージ説明文")]
    [SerializeField, TextArea(1,5)] string stageDescription;
    public string StageDescription { get { return stageDescription; } }

    [Header("漢字の例(鑿,暫時…)")]
    [SerializeField] string kanjiExample;
    public string KanjiExample { get { return kanjiExample; } }

    [Header("漢字レベル")]
    [SerializeField] int kanjiLevel;
    public int KanjiLevel { get { return kanjiLevel; } }

    [Header("タイピングレベル")]
    [SerializeField] int typingLevel;
    public int TypingLevel { get { return typingLevel; } }
}