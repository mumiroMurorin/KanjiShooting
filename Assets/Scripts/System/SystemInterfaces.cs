using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using Kanji;

namespace Kanji
{
    //漢字レベル
    [Flags]
    public enum Level
    {
        Everything = ~0,
        Level1 = 1 << 0,
        Level2 = 1 << 1,
        Level3 = 1 << 2,
        Level4 = 1 << 3,
        Level5 = 1 << 4,
        Level6 = 1 << 5,
        Level7 = 1 << 6,
    }

    //部首
    [Flags]
    public enum Radical
    {
        Everything = ~0,
        Unset = 1 << 0, //未設定
        Ichi = 1 << 1, //一
        Nabebuta = 1 << 2, //亠
        Ninben = 1 << 3, //にんべん
        Hitoyane = 1 << 4, //ひとやね
        Hitoashi = 1 << 5, //ひとあし
        Dougamae = 1 << 6, //どうがまえ
        Katana = 1 << 7, //かたな
        Rittou = 1 << 8, //りっとう
        Chikara = 1 << 9, //ちから
        Kakushigamae = 1 << 10, //かくしがまえ
        Zyu = 1 << 11, //じゅう
        Warifu = 1 << 12, //わりふ
        Gandare = 1 << 13, //がんだれ
        Mata = 1 << 14, //また
        Kuchi = 1 << 15, //くち
        Kuchihen = 1 << 16, //くちへん
        Kunigamae = 1 << 17, //くにがまえ
        Tsuchi = 1 << 18, //つち
        Samurai = 1 << 19, //さむらい
        Suinyou = 1 << 20, //すいにょう
        Dai = 1 << 21, //だい
        Onnna = 1 << 22, //おんな
        Onnnahen = 1 << 23, //おんなへん
        Ko = 1 << 24, //こ
        Ukanmuri = 1 << 25, //うかんむり
        Sun = 1 << 26, //すん
        Kabane = 1 << 27, //
        Yama = 1 << 28, //
        Haba = 1 << 29, //
        Madare = 1 << 30, //
        Ennyou = 1 << 31, //
        Yumi = 1 << 32, //
        Yumihen = 1 << 33, //
        Gyouninben = 1 << 34, //
        Tsukanmuri = 1 << 35, //
        Kokoro = 1 << 36, //
        Risshinben = 1 << 37, //
        Hokodukuri = 1 << 38, //
        Tehen = 1 << 39, //
        Nobun = 1 << 40, //
        Onodukuri = 1 << 41, //
        Houhen = 1 << 42, //
        Nichihen = 1 << 43, //
        Hirabi = 1 << 44, //
        Tsuki = 1 << 45, //
        Ki = 1 << 46, //
        Kihen = 1 << 47, //
        Akubi = 1 << 48, //
        Tomeru = 1 << 49, //
        Nakare = 1 << 50, //
        Uji = 1 << 51, //
        Mizu = 1 << 52, //
        Sanzui = 1 << 53, //
        Hihen = 1 << 54, //
        Rekka = 1 << 55, //
        Ushihen = 1 << 56, //
        Inu = 1 << 57, //
        Kemonohen = 1 << 58, //
        Ouhen = 1 << 59, //
        Umareru = 1 << 60, //
        Tahen = 1 << 61, //
        Yamaidare = 1 << 62, //
        Hatsugashira = 1 << 63, //
        Shiro = 1 << 64, //
        Sara = 1 << 65, //
        Yahen = 1 << 66, //
        Ishihen = 1 << 67, //
        Shimesu = 1 << 68, //
        Shimesuhen = 1 << 69, //
        Nogihen = 1 << 70, //
        Anakanmuri = 1 << 71, //
        Tatsu = 1 << 72, //
        Takekanmuri = 1 << 73, //
        Komehen = 1 << 74, //
        Ito = 1 << 75, //
        Itohen = 1 << 76, //
        Amigashira = 1 << 77, //
        Hitsuji = 1 << 78, //
        Hane = 1 << 79, //
        Oikanmuri = 1 << 80, //
        Mimi = 1 << 81, //
        Niku = 1 << 82, //
        Nikuduki = 1 << 83, //
        Funehen = 1 << 84, //
        Kusakanmuri = 1 << 85, //
        Mushi = 1 << 86, //
        Gyougamae = 1 << 87, //
        Koromo = 1 << 88, //
        Koromohen = 1 << 89, //
        Miru = 1 << 90, //
        Shin = 1 << 91, //
        Mame = 1 << 92, //
        Kai = 1 << 93, //
        Gonben = 1 << 94, //
        Kaihen = 1 << 95, //
        Kuruma = 1 << 96, //
        Kurumahen = 1 << 97, //
        Shinnyou = 1 << 98, //
        Ozato = 1 << 99, //
        Torihen = 1 << 100, //
        Sato = 1 << 101, //
        Kanehen = 1 << 102, //
        Mongamae = 1 << 103, //
        Kozatohen = 1 << 104, //
        Furutori = 1 << 105, //
        Amekanmuri = 1 << 106, //
        Ao = 1 << 107, //
        Ogai = 1 << 108, //
        Shokuhen = 1 << 109, //
        Umahen = 1 << 110, //
    }

    //その他タグ
    [Flags]
    public enum OtherTag
    {
        Everything = ~0,
        Unset = 1 << 0,
        Bird = 1 << 1, //鳥
        Fish = 1 << 2, //魚
        Plant = 1 << 3, //植物
        Insect = 1 << 4, //虫
        Animal = 1 << 5, //動物
        Human = 1 << 6, //人間
        Food = 1 << 7, //食べ物
        Nature = 1 << 8, //自然
        Product = 1 << 9, //製品,物
        Place = 1 << 10, //地名
        LoanWord = 1 << 11, //外来語
        ForeignCountry = 1 << 12, //外国名,外国地名
        Verb = 1 << 13, //動詞
        Adjective = 1 << 14, //形容詞
        Adverb = 1 << 15, //副詞
        ThreeCharacterIdiom = 1 << 16, //三字熟語
        FourCharacterIdiom = 1 << 17, //四字熟語
        FiveCharacterIdiom = 1 << 18, //五字熟語
    }

    /// <summary>
    /// 問題のデータ
    /// </summary>
    public struct QuestionData
    {
        public string kanji;
        public string furigana;
        public string[] answers;

        public Level level;
        public Radical radical;
        public OtherTag otherTag;
    }

    public class Convert
    {
        /// <summary>
        /// 文字列を enum に変換
        /// </summary>
        /// <param name="levelString"></param>
        /// <returns></returns>
        public static Level StringToLevel(string levelString)
        {
            if (Enum.TryParse(levelString, out Level level)) { return level; }
            else { throw new ArgumentException($"'{levelString}' is not a valid Level."); }
        }

        /// <summary>
        /// 文字列を enum に変換
        /// </summary>
        /// <param name="levelString"></param>
        /// <returns></returns>
        public static Radical StringToRadical(string radicalString)
        {
            if (Enum.TryParse(radicalString, out Radical radical)) { return radical; }
            else { throw new ArgumentException($"'{radicalString}' is not a valid Radical."); }
        }

        /// <summary>
        /// 文字列を enum に変換
        /// </summary>
        /// <param name="levelString"></param>
        /// <returns></returns>
        public static OtherTag StringToOtherTag(string otherTagString)
        {
            if (Enum.TryParse(otherTagString, out OtherTag otherTag)) { return otherTag; }
            else { throw new ArgumentException($"'{otherTagString}' is not a valid OtherTag."); }
        }
    }
}

/// <summary>
/// 問題セレクター
/// </summary>
public interface IQuestionSelector
{
    public void Initialize();

    public QuestionData GetQuestionData(QuestionFilter filter);
}

/// <summary>
/// ステージ内フェーズの遷移
/// </summary>
public interface IStagePhaseTransitioner
{
    UniTask ExecuteAsync(CancellationToken cancellationToken);
}

/// <summary>
/// ウェーブ内の敵スポーン情報を取り扱う
/// </summary>
public interface IWaveStatus
{
    public void Initialize(IQuestionSelector qSelector, KanjiObjectSpawner kSpawner);

    public void SpawnEnemy(float timeRatio, EnemyInitializationData enemyInitializationData);

    public void DespawnEnemy();
}