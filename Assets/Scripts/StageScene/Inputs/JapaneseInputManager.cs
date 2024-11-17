using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JapaneseInputManager : MonoBehaviour
{
    [SerializeField] UnityEvent<string> onChangeAnswer;
    [SerializeField] SerializeInterface<ICanInput> inputPermit;

    JapaneseInputHandler japaneseInputHandler = new JapaneseInputHandler();

    private void Update()
    {
        // ���{����͏���
        InputKeyBoard();
    }

    /// <summary>
    /// ���{�����
    /// </summary>
    private void InputKeyBoard()
    {
        if (!inputPermit.Value.CanInput) { return; }
        if (!inputPermit.Value.CanInputJapanese) { return; }

        // �L�[���͂��擾
        if (!Input.anyKeyDown) { return; }

        KeyCode inputKey = KeyCode.None;
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (!Input.GetKeyDown(keyCode)) { continue; }
            inputKey = keyCode;
        }

        // ���͂��ꂽ�����ɑΉ����Ă��Ȃ�������(�Ⴆ��Enter)�Ԃ�
        if (!japaneseInputHandler.OnKeyInput(inputKey)) { return; }

        //Debug.Log("���݂̓���: " + japaneseInputHandler.GetResult());
        onChangeAnswer.Invoke(japaneseInputHandler.GetResult());
    }

    /// <summary>
    /// �ꕶ������
    /// </summary>
    public void BackSpace()
    {
        japaneseInputHandler.BackSpace();
        onChangeAnswer.Invoke(japaneseInputHandler.GetResult());
    }

    /// <summary>
    /// ���͓��{��̃��Z�b�g
    /// </summary>
    public void ClearInput()
    {
        japaneseInputHandler.Clear();
    }

}
