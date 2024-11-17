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

        // 各種ハンドラーの初期化
        foreach (IInputHandler handler in inputHandlers)
        {
            handler.Initialize(this);
        }

        //カーソルの非表示化
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

        // ローディング中のとき入力を受け付けない
        StageManager.Instance.CurrentStageStatusreactiveproperty
            .Where(status => status == StageStatus.Loading)
            .Subscribe(status =>
            {
                CanInput = false;
                CanInputJapanese = false;
            })
            .AddTo(this.gameObject);

        // バトル中のとき入力を受け付ける
        StageManager.Instance.CurrentStageStatusreactiveproperty
            .Where(status => status == StageStatus.Fighting)
            .Subscribe(status =>
            {
                CanInput = true;
                CanInputJapanese = true;
            })
            .AddTo(this.gameObject);

        // ステージ終了時入力を受け付けない
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
/// 入力に対するコールバックを実装
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

        // このままだと縦横が逆になってしまうので反転
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