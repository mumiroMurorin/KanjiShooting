using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/StageData", fileName = "StageData")]
/// <summary>
/// �X�e�[�W�̏ڍ׃f�[�^
/// </summary>
public class StageDetailData : ScriptableObject , IStageTransitionData
{
    [SerializeField] string stageTitle;
    public string StageTitle { get { return stageTitle; } }

    [Header("�V�[����")]
    [SerializeField] string sceneName;
    public string SceneName { get { return sceneName; } }

    [Header("��Փx")]
    [SerializeField] int difficulty;
    public int Difficulty { get { return difficulty; } }

    [Header("�W������")]
    [SerializeField] string genre;
    public string Genre { get { return genre; } }

    [SerializeField] Sprite stageItemBackSprite;
    public Sprite StageItemBackSprite { get { return stageItemBackSprite; } }

    [SerializeField] Sprite backGroundSprite;
    public Sprite BackGroundSprite { get { return backGroundSprite; } }

    [Header("�X�e�[�W������")]
    [SerializeField, TextArea(1,5)] string stageDescription;
    public string StageDescription { get { return stageDescription; } }

    [Header("�����̗�(�w,�b���c)")]
    [SerializeField] string kanjiExample;
    public string KanjiExample { get { return kanjiExample; } }

    [Header("�������x��")]
    [SerializeField] int kanjiLevel;
    public int KanjiLevel { get { return kanjiLevel; } }

    [Header("�^�C�s���O���x��")]
    [SerializeField] int typingLevel;
    public int TypingLevel { get { return typingLevel; } }
}

/// <summary>
/// �X�e�[�W�J�ڂ̍ۂɕK�v�ȃf�[�^
/// </summary>
public interface IStageTransitionData
{
    string SceneName { get; }
}