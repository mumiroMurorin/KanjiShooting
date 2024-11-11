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

    [SerializeField] int difficulity;
    public int Difficulity { get { return difficulity; } }

    [SerializeField] string genre;
    public string Genre { get { return genre; } }

    [SerializeField] Sprite stageItemBackSprite;
    public Sprite StageItemBackSprite { get { return stageItemBackSprite; } }

    [SerializeField] Sprite backGroundSprite;
    public Sprite BackGroundSprite { get { return backGroundSprite; } }

    [SerializeField] string stageDescription;
    public string StageDescription { get { return stageDescription; } }

    [SerializeField] string kanjiExample;
    public string KanjiExample { get { return kanjiExample; } }

    [SerializeField] int kanjiLevel;
    public int KanjiLevel { get { return kanjiLevel; } }

    [SerializeField] int typingLevel;
    public int TypingLevel { get { return typingLevel; } }
}