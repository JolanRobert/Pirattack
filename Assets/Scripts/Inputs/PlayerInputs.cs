//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.0
//     from Assets/Scripts/Inputs/PlayerInputs.inputactions
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

public partial class @PlayerInputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""0466047e-4285-4934-b150-920240e45988"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""d15f1737-fa6e-4b06-9703-a936e948f6ae"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""0ba36f14-eb68-4014-b82c-f1a5893e70f1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""482767b1-1a5a-4065-9872-6d47e76b13f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchColor"",
                    ""type"": ""Button"",
                    ""id"": ""026d3e44-e086-4b1d-a463-65aff47e7518"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""d2f38423-f456-4a4b-9445-86630b6832d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CancelInteract"",
                    ""type"": ""Button"",
                    ""id"": ""00c74a7c-6d26-4596-95ac-daa28f97a979"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LB"",
                    ""type"": ""Button"",
                    ""id"": ""705eb60d-073a-4f29-9f79-ceae16a13635"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RB"",
                    ""type"": ""Button"",
                    ""id"": ""0f67fbdb-b9d1-4552-a4b1-5f00787000d1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""A"",
                    ""type"": ""Button"",
                    ""id"": ""bbb9b46a-67b6-4fd9-958b-46d2f1f5fd36"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""X"",
                    ""type"": ""Button"",
                    ""id"": ""eae654b9-0508-471b-80d6-8f7c05bbe57c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Y"",
                    ""type"": ""Button"",
                    ""id"": ""cdafd902-1cdd-4a29-9667-63735c4f1165"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftStick"",
                    ""type"": ""Value"",
                    ""id"": ""452e1397-1a50-42db-92d6-43ec1cbef052"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RightStick"",
                    ""type"": ""Value"",
                    ""id"": ""cf10cb36-8e11-463d-bec9-31c4aee64a3f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""e830564a-7ff0-44da-b428-aa80da6013ea"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""beb2bf64-aa55-4a94-8297-c4b52709ed33"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d89dfe7a-48d0-48a0-aa58-4672f2861d44"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""aa60acc4-7e51-4db8-b77d-e7f15676b3f9"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3cab1572-9a53-4519-9800-cfca4d7e9f19"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""bc508c11-0efc-4ad5-9021-3c9143b64ed0"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3673c396-30ec-4cd1-87f7-e35ce9bfdb29"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4370c6d6-6e48-40fb-9456-4f4b00e24e81"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7dfc3e4d-3880-4a73-ade2-4773cde38f5c"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2504fd3d-8e3e-4626-bd9b-f90ed53710d9"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""17febd94-6e0f-4a40-a189-d16b41565b3b"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c7484bf-e74f-401d-b6b7-c0a1a2ffd336"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SwitchColor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f1b2ddf-d5f6-4ee1-9977-059b1ad636e4"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b648cd4-ff34-4a1c-9b8f-2ec8d34df62c"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CancelInteract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0369d802-a4c7-4bec-8f4a-e49bf68008e9"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b7b3c5f-aa94-43a2-817e-59ca306d5dda"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""499a767d-c91a-4a2e-a297-0225878b14ee"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71bc2311-7bf1-475a-8602-572c02b2a625"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2151b77b-e9f7-4f9c-866d-df08fc0619d1"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RightStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""557de354-86aa-43d2-8dcf-3f78b83e95bf"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa35eee8-d3da-4194-877d-956e75d6b8dd"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""edf43b33-39ce-42e9-a21c-75914e7c19f0"",
            ""actions"": [
                {
                    ""name"": ""Return"",
                    ""type"": ""Button"",
                    ""id"": ""0e623dcf-9924-4e3e-b4fa-8e9a31d48696"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Join"",
                    ""type"": ""Button"",
                    ""id"": ""96d6bc39-c3e0-42b1-9f61-b4fb64613dbf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""f034cd1f-8fd7-45a1-a7b2-56bf9fcf46d4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""825b2f1b-70e6-440c-bf81-ce6434ab22b5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""120cfba5-a782-4579-83a9-28b693a4a9bb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""865b8693-6e97-4888-958b-8b358d3569fb"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Return"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b429c8c9-3122-4bd2-ac4e-908800fef003"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Return"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e36dd465-7035-4200-8c6a-7fadfe7e722c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60d66c3e-bd91-464e-9a1e-6d7b5cda7e09"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Left Stick"",
                    ""id"": ""0919b8eb-6af0-4c24-86e8-2bbee61c6598"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3434a6b8-e5f4-4af0-9e92-a3212bc5b84a"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2482601f-41ab-4c5b-9e19-dfdbfe04bfbc"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d3add985-2ee7-48b9-85c3-391525ea597c"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3912dd9f-be59-43be-ac88-3cf6db5cd845"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ed0f1941-cbc5-44e9-a17e-7ac44c0d91c1"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Rotate = m_Player.FindAction("Rotate", throwIfNotFound: true);
        m_Player_Shoot = m_Player.FindAction("Shoot", throwIfNotFound: true);
        m_Player_SwitchColor = m_Player.FindAction("SwitchColor", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_CancelInteract = m_Player.FindAction("CancelInteract", throwIfNotFound: true);
        m_Player_LB = m_Player.FindAction("LB", throwIfNotFound: true);
        m_Player_RB = m_Player.FindAction("RB", throwIfNotFound: true);
        m_Player_A = m_Player.FindAction("A", throwIfNotFound: true);
        m_Player_X = m_Player.FindAction("X", throwIfNotFound: true);
        m_Player_Y = m_Player.FindAction("Y", throwIfNotFound: true);
        m_Player_LeftStick = m_Player.FindAction("LeftStick", throwIfNotFound: true);
        m_Player_RightStick = m_Player.FindAction("RightStick", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Return = m_UI.FindAction("Return", throwIfNotFound: true);
        m_UI_Join = m_UI.FindAction("Join", throwIfNotFound: true);
        m_UI_Submit = m_UI.FindAction("Submit", throwIfNotFound: true);
        m_UI_Move = m_UI.FindAction("Move", throwIfNotFound: true);
        m_UI_Pause = m_UI.FindAction("Pause", throwIfNotFound: true);
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
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Rotate;
    private readonly InputAction m_Player_Shoot;
    private readonly InputAction m_Player_SwitchColor;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_CancelInteract;
    private readonly InputAction m_Player_LB;
    private readonly InputAction m_Player_RB;
    private readonly InputAction m_Player_A;
    private readonly InputAction m_Player_X;
    private readonly InputAction m_Player_Y;
    private readonly InputAction m_Player_LeftStick;
    private readonly InputAction m_Player_RightStick;
    public struct PlayerActions
    {
        private @PlayerInputs m_Wrapper;
        public PlayerActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Rotate => m_Wrapper.m_Player_Rotate;
        public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
        public InputAction @SwitchColor => m_Wrapper.m_Player_SwitchColor;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @CancelInteract => m_Wrapper.m_Player_CancelInteract;
        public InputAction @LB => m_Wrapper.m_Player_LB;
        public InputAction @RB => m_Wrapper.m_Player_RB;
        public InputAction @A => m_Wrapper.m_Player_A;
        public InputAction @X => m_Wrapper.m_Player_X;
        public InputAction @Y => m_Wrapper.m_Player_Y;
        public InputAction @LeftStick => m_Wrapper.m_Player_LeftStick;
        public InputAction @RightStick => m_Wrapper.m_Player_RightStick;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Rotate.started += instance.OnRotate;
            @Rotate.performed += instance.OnRotate;
            @Rotate.canceled += instance.OnRotate;
            @Shoot.started += instance.OnShoot;
            @Shoot.performed += instance.OnShoot;
            @Shoot.canceled += instance.OnShoot;
            @SwitchColor.started += instance.OnSwitchColor;
            @SwitchColor.performed += instance.OnSwitchColor;
            @SwitchColor.canceled += instance.OnSwitchColor;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @CancelInteract.started += instance.OnCancelInteract;
            @CancelInteract.performed += instance.OnCancelInteract;
            @CancelInteract.canceled += instance.OnCancelInteract;
            @LB.started += instance.OnLB;
            @LB.performed += instance.OnLB;
            @LB.canceled += instance.OnLB;
            @RB.started += instance.OnRB;
            @RB.performed += instance.OnRB;
            @RB.canceled += instance.OnRB;
            @A.started += instance.OnA;
            @A.performed += instance.OnA;
            @A.canceled += instance.OnA;
            @X.started += instance.OnX;
            @X.performed += instance.OnX;
            @X.canceled += instance.OnX;
            @Y.started += instance.OnY;
            @Y.performed += instance.OnY;
            @Y.canceled += instance.OnY;
            @LeftStick.started += instance.OnLeftStick;
            @LeftStick.performed += instance.OnLeftStick;
            @LeftStick.canceled += instance.OnLeftStick;
            @RightStick.started += instance.OnRightStick;
            @RightStick.performed += instance.OnRightStick;
            @RightStick.canceled += instance.OnRightStick;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Rotate.started -= instance.OnRotate;
            @Rotate.performed -= instance.OnRotate;
            @Rotate.canceled -= instance.OnRotate;
            @Shoot.started -= instance.OnShoot;
            @Shoot.performed -= instance.OnShoot;
            @Shoot.canceled -= instance.OnShoot;
            @SwitchColor.started -= instance.OnSwitchColor;
            @SwitchColor.performed -= instance.OnSwitchColor;
            @SwitchColor.canceled -= instance.OnSwitchColor;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @CancelInteract.started -= instance.OnCancelInteract;
            @CancelInteract.performed -= instance.OnCancelInteract;
            @CancelInteract.canceled -= instance.OnCancelInteract;
            @LB.started -= instance.OnLB;
            @LB.performed -= instance.OnLB;
            @LB.canceled -= instance.OnLB;
            @RB.started -= instance.OnRB;
            @RB.performed -= instance.OnRB;
            @RB.canceled -= instance.OnRB;
            @A.started -= instance.OnA;
            @A.performed -= instance.OnA;
            @A.canceled -= instance.OnA;
            @X.started -= instance.OnX;
            @X.performed -= instance.OnX;
            @X.canceled -= instance.OnX;
            @Y.started -= instance.OnY;
            @Y.performed -= instance.OnY;
            @Y.canceled -= instance.OnY;
            @LeftStick.started -= instance.OnLeftStick;
            @LeftStick.performed -= instance.OnLeftStick;
            @LeftStick.canceled -= instance.OnLeftStick;
            @RightStick.started -= instance.OnRightStick;
            @RightStick.performed -= instance.OnRightStick;
            @RightStick.canceled -= instance.OnRightStick;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
    private readonly InputAction m_UI_Return;
    private readonly InputAction m_UI_Join;
    private readonly InputAction m_UI_Submit;
    private readonly InputAction m_UI_Move;
    private readonly InputAction m_UI_Pause;
    public struct UIActions
    {
        private @PlayerInputs m_Wrapper;
        public UIActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Return => m_Wrapper.m_UI_Return;
        public InputAction @Join => m_Wrapper.m_UI_Join;
        public InputAction @Submit => m_Wrapper.m_UI_Submit;
        public InputAction @Move => m_Wrapper.m_UI_Move;
        public InputAction @Pause => m_Wrapper.m_UI_Pause;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void AddCallbacks(IUIActions instance)
        {
            if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            @Return.started += instance.OnReturn;
            @Return.performed += instance.OnReturn;
            @Return.canceled += instance.OnReturn;
            @Join.started += instance.OnJoin;
            @Join.performed += instance.OnJoin;
            @Join.canceled += instance.OnJoin;
            @Submit.started += instance.OnSubmit;
            @Submit.performed += instance.OnSubmit;
            @Submit.canceled += instance.OnSubmit;
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
        }

        private void UnregisterCallbacks(IUIActions instance)
        {
            @Return.started -= instance.OnReturn;
            @Return.performed -= instance.OnReturn;
            @Return.canceled -= instance.OnReturn;
            @Join.started -= instance.OnJoin;
            @Join.performed -= instance.OnJoin;
            @Join.canceled -= instance.OnJoin;
            @Submit.started -= instance.OnSubmit;
            @Submit.performed -= instance.OnSubmit;
            @Submit.canceled -= instance.OnSubmit;
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
        }

        public void RemoveCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIActions instance)
        {
            foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnSwitchColor(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnCancelInteract(InputAction.CallbackContext context);
        void OnLB(InputAction.CallbackContext context);
        void OnRB(InputAction.CallbackContext context);
        void OnA(InputAction.CallbackContext context);
        void OnX(InputAction.CallbackContext context);
        void OnY(InputAction.CallbackContext context);
        void OnLeftStick(InputAction.CallbackContext context);
        void OnRightStick(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnReturn(InputAction.CallbackContext context);
        void OnJoin(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
