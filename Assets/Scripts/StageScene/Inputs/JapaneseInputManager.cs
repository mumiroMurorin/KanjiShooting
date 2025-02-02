using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;

public class JapaneseInputManager : MonoBehaviour, IJapaneseInputHolder
{
    [SerializeField] UnityEvent<string> onChangeAnswer;
    [SerializeField] SerializeInterface<ICanInput> inputPermit;

    [SerializeField] AudioClip inputSE;
    [SerializeField] AudioClip backSpaceSE;

    JapaneseInputHandler japaneseInputHandler = new JapaneseInputHandler();
    public IReadOnlyReactiveProperty<string> AnswerReactiveProperty { get { return japaneseInputHandler.AnswerReactiveProperty; } }

    private void Update()
    {
        // 日本語入力処理
        InputKeyBoard();
    }

    /// <summary>
    /// 日本語入力
    /// </summary>
    private void InputKeyBoard()
    {
        if (!inputPermit.Value.CanInput) { return; }
        if (!inputPermit.Value.CanInputJapanese) { return; }

        // キー入力を取得
        if (!Input.anyKeyDown) { return; }

        KeyCode inputKey = KeyCode.None;
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (!Input.GetKeyDown(keyCode)) { continue; }
            inputKey = keyCode;
        }

        // 入力された文字に対応していなかったら(例えばEnter)返す
        if (!japaneseInputHandler.OnKeyInput(inputKey)) { return; }

        //Debug.Log("現在の入力: " + japaneseInputHandler.GetResult());
        onChangeAnswer.Invoke(japaneseInputHandler.GetResult());

        // SE再生
        Sound.SoundManager.Instance.PlaySE(inputSE);
    }

    /// <summary>
    /// 一文字消す
    /// </summary>
    public void BackSpace()
    {
        if (!japaneseInputHandler.BackSpace()) { return; }
        onChangeAnswer.Invoke(japaneseInputHandler.GetResult());

        // SE再生
        Sound.SoundManager.Instance.PlaySE(backSpaceSE);
    }

    /// <summary>
    /// 入力日本語のリセット
    /// </summary>
    public void ClearInput()
    {
        japaneseInputHandler.Clear();
    }

    /// <summary>
    /// 入力されている答えを返す
    /// </summary>
    /// <returns></returns>
    public string GetAnswer() 
    { 
        return japaneseInputHandler.GetResult();
    } 
}

public interface IJapaneseInputHolder
{
    /// <summary>
    /// 日本語入力のリセット
    /// </summary>
    public void ClearInput();

    /// <summary>
    /// 入力されている答えを返す
    /// </summary>
    /// <returns></returns>
    public string GetAnswer();

    public IReadOnlyReactiveProperty<string> AnswerReactiveProperty { get; }
}