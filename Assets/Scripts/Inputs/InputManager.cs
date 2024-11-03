using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] UnityEvent<Vector2> onMoveCursor;
    [SerializeField] UnityEvent onPushEnter;
    [SerializeField] UnityEvent onPushBackSpace;
    [SerializeField] UnityEvent<string> onChangeAnswer;

    JapaneseInputHandler japaneseInputHandler = new JapaneseInputHandler();

    private void OnMoveCursor(InputAction.CallbackContext context) 
    {
        onMoveCursor?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnPushEnter(InputAction.CallbackContext context)
    {
        onPushEnter?.Invoke();
    }

    private void OnPushBackSpace(InputAction.CallbackContext context)
    {
        onPushBackSpace?.Invoke();
    }

    private PlayerInputs gameInputs;

    private void Awake()
    {
        Bind();

        //カーソルの非表示化
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameInputs = new PlayerInputs();

        gameInputs.Player.Move.started += OnMoveCursor;
        gameInputs.Player.Move.performed += OnMoveCursor;

        gameInputs.Player.Attack.started += OnPushEnter;

        gameInputs.Player.BackSpace.started += OnPushBackSpace;

        gameInputs.Enable();
    }

    private void Bind()
    {
        onPushBackSpace.AddListener(japaneseInputHandler.BackSpace);
    }

    private void Update()
    {
        InputKeyBoard();
    }

    private void InputKeyBoard()
    {
        // キー入力を取得
        if (!Input.anyKeyDown) { return; }
        
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (!Input.GetKeyDown(keyCode)) { continue; }

            japaneseInputHandler.OnKeyInput(keyCode);
        }

        //Debug.Log("現在の入力: " + japaneseInputHandler.GetResult());
        onChangeAnswer.Invoke(japaneseInputHandler.GetResult());
    }

    /// <summary>
    /// 入力日本語のリセット
    /// </summary>
    public void ClearInput()
    {
        japaneseInputHandler.Clear();
    }

    private void OnDestroy()
    {
        gameInputs?.Dispose();
    }

}
