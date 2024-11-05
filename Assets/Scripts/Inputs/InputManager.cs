using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UniRx;

public class InputManager : MonoBehaviour
{
    [SerializeField] UnityEvent<Vector2> onMoveCursor;
    [SerializeField] UnityEvent onPushEnter;
    [SerializeField] UnityEvent onPushBackSpace;
    [SerializeField] UnityEvent<string> onChangeAnswer;

    JapaneseInputHandler japaneseInputHandler = new JapaneseInputHandler();

    public bool CanInput { private get; set; } = true;
    public bool CanInputJapanese { private get; set; } = true;

    private void OnMoveCursor(InputAction.CallbackContext context) 
    {
        if (!CanInput) { return; }
        onMoveCursor?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnPushEnter(InputAction.CallbackContext context)
    {
        if (!CanInput) { return; }
        onPushEnter?.Invoke();
    }

    private void OnPushBackSpace(InputAction.CallbackContext context)
    {
        if (!CanInput) { return; }
        if (!CanInputJapanese) { return; }
        onPushBackSpace?.Invoke();
    }

    private PlayerInputs gameInputs;

    private void Awake()
    {
        Bind();

        //�J�[�\���̔�\����
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

        //���[�f�B���O���̂Ƃ����͂��󂯕t���Ȃ�
        StageManager.Instance.CurrentStageStatusreactiveproperty
            .Where(status => status == StageStatus.Loading)
            .Subscribe(status =>
            {
                CanInput = false;
                CanInputJapanese = false;
            })
            .AddTo(this.gameObject);

        //�o�g�����̂Ƃ����͂��󂯕t����
        StageManager.Instance.CurrentStageStatusreactiveproperty
            .Where(status => status == StageStatus.Battling)
            .Subscribe(status =>
            {
                CanInput = true;
                CanInputJapanese = true;
            })
            .AddTo(this.gameObject);

        //�X�e�[�W�I�������͂��󂯕t���Ȃ�
        StageManager.Instance.CurrentStageStatusreactiveproperty
            .Where(status => status == StageStatus.StageFinish)
            .Subscribe(status =>
            {
                CanInput = false;
                CanInputJapanese = false;
            })
            .AddTo(this.gameObject);
    }

    private void Update()
    {
        //���{����͏���
        InputKeyBoard();
    }

    /// <summary>
    /// ���{�����
    /// </summary>
    private void InputKeyBoard()
    {
        if (!CanInput) { return; }
        if(!CanInputJapanese) { return; }

        // �L�[���͂��擾
        if (!Input.anyKeyDown) { return; }
        
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (!Input.GetKeyDown(keyCode)) { continue; }

            japaneseInputHandler.OnKeyInput(keyCode);
        }

        //Debug.Log("���݂̓���: " + japaneseInputHandler.GetResult());
        onChangeAnswer.Invoke(japaneseInputHandler.GetResult());
    }

    /// <summary>
    /// ���͓��{��̃��Z�b�g
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
