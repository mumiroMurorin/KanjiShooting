//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Variety/PlayerInputs.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputs : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""0845d29c-792a-421d-a679-1c8eee0e7511"",
            ""actions"": [
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""7cc542fa-7163-41a7-9465-d8f61f953c7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""BackSpace"",
                    ""type"": ""Button"",
                    ""id"": ""bea4f728-cf9f-4a43-a3ee-c876a83b602c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""90581a93-e935-4c8f-a9c6-5de6870f6ff6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RotatePermit"",
                    ""type"": ""Button"",
                    ""id"": ""d88e709d-6a7a-4230-a974-ff41f0950838"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateFromKey"",
                    ""type"": ""Value"",
                    ""id"": ""ac578646-1798-462c-bdcc-936a5a4bd671"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RotateFromArraw"",
                    ""type"": ""Value"",
                    ""id"": ""d4a3ea8b-aa65-42cc-8027-9d334830d11d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d99c82a3-3eda-41c7-8b3b-de1f06d18daf"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea1472c8-4b4b-4772-96c4-39a897feb906"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a2c59a7-2c7f-486c-98d2-4622f1757d46"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BackSpace"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8d461da-c6c4-4be4-aa04-756d06786832"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotatePermit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""195456ba-8d6a-48bc-94b7-477924706928"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateFromKey"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4cbb82e9-f366-4957-b9a4-f1c284429e7c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateFromKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""00db33f4-fd57-436f-8a38-3eec29fbc5fe"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateFromKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4470140e-1ce9-49d4-bfe0-aca4a0af9bed"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateFromKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fc500c2b-9b72-4d78-b710-f18e868ef0a7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateFromKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""253ce220-80cf-41cb-bfbf-4f11924fa147"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateFromArraw"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5a0a9ae2-eb1a-419f-a8aa-979ad7fc6080"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateFromArraw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""22857f3f-e848-4777-b4ca-ffca8d67e752"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateFromArraw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1b0e37e5-ebf9-4d7e-a0f2-1287110512b6"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateFromArraw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0de53f20-e24a-4184-9b79-a7418a63f515"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateFromArraw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_BackSpace = m_Player.FindAction("BackSpace", throwIfNotFound: true);
        m_Player_Rotate = m_Player.FindAction("Rotate", throwIfNotFound: true);
        m_Player_RotatePermit = m_Player.FindAction("RotatePermit", throwIfNotFound: true);
        m_Player_RotateFromKey = m_Player.FindAction("RotateFromKey", throwIfNotFound: true);
        m_Player_RotateFromArraw = m_Player.FindAction("RotateFromArraw", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_BackSpace;
    private readonly InputAction m_Player_Rotate;
    private readonly InputAction m_Player_RotatePermit;
    private readonly InputAction m_Player_RotateFromKey;
    private readonly InputAction m_Player_RotateFromArraw;
    public struct PlayerActions
    {
        private @PlayerInputs m_Wrapper;
        public PlayerActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @BackSpace => m_Wrapper.m_Player_BackSpace;
        public InputAction @Rotate => m_Wrapper.m_Player_Rotate;
        public InputAction @RotatePermit => m_Wrapper.m_Player_RotatePermit;
        public InputAction @RotateFromKey => m_Wrapper.m_Player_RotateFromKey;
        public InputAction @RotateFromArraw => m_Wrapper.m_Player_RotateFromArraw;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @BackSpace.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBackSpace;
                @BackSpace.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBackSpace;
                @BackSpace.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBackSpace;
                @Rotate.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotate;
                @RotatePermit.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotatePermit;
                @RotatePermit.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotatePermit;
                @RotatePermit.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotatePermit;
                @RotateFromKey.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateFromKey;
                @RotateFromKey.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateFromKey;
                @RotateFromKey.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateFromKey;
                @RotateFromArraw.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateFromArraw;
                @RotateFromArraw.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateFromArraw;
                @RotateFromArraw.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateFromArraw;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @BackSpace.started += instance.OnBackSpace;
                @BackSpace.performed += instance.OnBackSpace;
                @BackSpace.canceled += instance.OnBackSpace;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @RotatePermit.started += instance.OnRotatePermit;
                @RotatePermit.performed += instance.OnRotatePermit;
                @RotatePermit.canceled += instance.OnRotatePermit;
                @RotateFromKey.started += instance.OnRotateFromKey;
                @RotateFromKey.performed += instance.OnRotateFromKey;
                @RotateFromKey.canceled += instance.OnRotateFromKey;
                @RotateFromArraw.started += instance.OnRotateFromArraw;
                @RotateFromArraw.performed += instance.OnRotateFromArraw;
                @RotateFromArraw.canceled += instance.OnRotateFromArraw;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnAttack(InputAction.CallbackContext context);
        void OnBackSpace(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnRotatePermit(InputAction.CallbackContext context);
        void OnRotateFromKey(InputAction.CallbackContext context);
        void OnRotateFromArraw(InputAction.CallbackContext context);
    }
}
