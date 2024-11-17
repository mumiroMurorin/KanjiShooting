using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UniRx;
using StageTransition;


public class InputManager : MonoBehaviour, ICanInput
{
    [SerializeReference, SubclassSelector] IInputHandler[] inputHandlers;

    public bool CanInput { get; set; } = true;
    public bool CanInputJapanese { get; set; } = true;

    private PlayerInputs gameInputs;

    private void Awake()
    {
        Initialze();
        Bind();
    }

    private void Initialze()
    {
        gameInputs = new PlayerInputs();

        // �e��n���h���[�̏�����
        foreach (IInputHandler handler in inputHandlers)
        {
            handler.Initialize(this);
        }

        //�J�[�\���̔�\����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameInputs.Enable();
    }

    private void Bind()
    {
        foreach (IInputHandler handler in inputHandlers)
        {
            handler.Bind(gameInputs);
        }

        // ���[�f�B���O���̂Ƃ����͂��󂯕t���Ȃ�
        StageManager.Instance.CurrentStageStatusreactiveproperty
            .Where(status => status == StageStatus.Loading)
            .Subscribe(status =>
            {
                CanInput = false;
                CanInputJapanese = false;
            })
            .AddTo(this.gameObject);

        // �o�g�����̂Ƃ����͂��󂯕t����
        StageManager.Instance.CurrentStageStatusreactiveproperty
            .Where(status => status == StageStatus.Fighting)
            .Subscribe(status =>
            {
                CanInput = true;
                CanInputJapanese = true;
            })
            .AddTo(this.gameObject);

        // �X�e�[�W�I�������͂��󂯕t���Ȃ�
        StageManager.Instance.CurrentStageStatusreactiveproperty
            .Where(status => status == StageStatus.StageFinish)
            .Subscribe(status =>
            {
                CanInput = false;
                CanInputJapanese = false;
            })
            .AddTo(this.gameObject);
    }

    private void OnDestroy()
    {
        gameInputs?.Dispose();
    }

}

public interface ICanInput
{
    public bool CanInput { get; }
    public bool CanInputJapanese { get; }
}


/// <summary>
/// ���͂ɑ΂���R�[���o�b�N������
/// </summary>
public interface IInputHandler
{
    void Initialize(ICanInput handler);

    void Bind(PlayerInputs inputs);
}

public class PlayerRotateHandler : IInputHandler
{
    [SerializeField] UnityEvent<Vector2> onInput;
    ICanInput permit;

    public void Initialize(ICanInput handler)
    {
        permit = handler;
    }

    public void Bind(PlayerInputs inputs)
    {
        inputs.Player.Rotate.started += OnAction;
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        if (!permit.CanInput) { return; }

        // ���̂܂܂��Əc�����t�ɂȂ��Ă��܂��̂Ŕ��]
        Vector2 vector2 = context.ReadValue<Vector2>();
        onInput?.Invoke(new Vector2(-vector2.y, vector2.x));
    }
}

public class AttackHandler : IInputHandler
{
    [SerializeField] UnityEvent onInput;
    ICanInput permit;

    public void Initialize(ICanInput handler)
    {
        permit = handler;
    }

    public void Bind(PlayerInputs inputs)
    {
        inputs.Player.Attack.started += OnAction;
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        if (!permit.CanInput) { return; }

        onInput?.Invoke();
    }
}

public class AttackHoldHandler : IInputHandler
{
    [SerializeField] UnityEvent onPerfomred;
    [SerializeField] UnityEvent onCanceled;
    ICanInput permit;

    public void Initialize(ICanInput handler)
    {
        permit = handler;
    }

    public void Bind(PlayerInputs inputs)
    {
        inputs.Player.Attack.performed += OnPerformed;
        inputs.Player.Attack.canceled += OnCanceled;
    }

    private void OnPerformed(InputAction.CallbackContext context)
    {
        if (!permit.CanInput) { return; }

        onPerfomred?.Invoke();
    }

    private void OnCanceled(InputAction.CallbackContext context)
    {
        if (!permit.CanInput) { return; }

        onCanceled?.Invoke();
    }
}

public class BackSpaceHandler : IInputHandler
{
    [SerializeField] UnityEvent onInput;
    ICanInput permit;

    public void Initialize(ICanInput handler)
    {
        permit = handler;
    }

    public void Bind(PlayerInputs inputs)
    {
        inputs.Player.BackSpace.started += OnAction;
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        if (!permit.CanInput) { return; }
        if (!permit.CanInputJapanese) { return; }

        onInput?.Invoke();
    }
}

public class RotatePermitHandler : IInputHandler
{
    [SerializeField] UnityEvent onStarted;
    [SerializeField] UnityEvent onCanceled;
    ICanInput permit;

    public void Initialize(ICanInput handler)
    {
        permit = handler;
    }

    public void Bind(PlayerInputs inputs)
    {
        inputs.Player.RotatePermit.started += OnStarted;
        inputs.Player.RotatePermit.canceled += OnCanceled;
    }

    private void OnStarted(InputAction.CallbackContext context)
    {
        if (!permit.CanInput) { return; }

        onStarted?.Invoke();
    }

    private void OnCanceled(InputAction.CallbackContext context)
    {
        if (!permit.CanInput) { return; }

        onCanceled?.Invoke();
    }
}

public class PlayerRotateFromKeyInputHandler : IInputHandler
{
    [SerializeField] UnityEvent<Vector2> onInput;
    ICanInput permit;

    public void Initialize(ICanInput handler)
    {
        permit = handler;
    }

    public void Bind(PlayerInputs inputs)
    {
        inputs.Player.RotateFromKey.performed += OnPerformed;
        inputs.Player.RotateFromKey.canceled += OnCanceled;
    }

    private void OnPerformed(InputAction.CallbackContext context)
    {
        if (!permit.CanInput) { return; }
        if (permit.CanInputJapanese) { return; }

        Vector2 vector2 = context.ReadValue<Vector2>();
        onInput?.Invoke(new Vector2(-vector2.y, vector2.x));
    }

    private void OnCanceled(InputAction.CallbackContext context)
    {
        if (!permit.CanInput) { return; }
        if (permit.CanInputJapanese) { return; }

        onInput?.Invoke(Vector2.zero);
    }
}

public class PlayerRotateFromArrawHandler : IInputHandler
{
    [SerializeField] UnityEvent<Vector2> onInput;
    ICanInput permit;

    public void Initialize(ICanInput handler)
    {
        permit = handler;
    }

    public void Bind(PlayerInputs inputs)
    {
        inputs.Player.RotateFromArraw.performed += OnPerformed;
        inputs.Player.RotateFromArraw.canceled += OnCanceled;
    }

    private void OnPerformed(InputAction.CallbackContext context)
    {
        if (!permit.CanInput) { return; }

        Vector2 vector2 = context.ReadValue<Vector2>();
        onInput?.Invoke(new Vector2(-vector2.y, vector2.x));
    }

    private void OnCanceled(InputAction.CallbackContext context)
    {
        if (!permit.CanInput) { return; }

        onInput?.Invoke(Vector2.zero);
    }
}