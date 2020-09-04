// GENERATED AUTOMATICALLY FROM 'Assets/Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Input : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""PlayerControls"",
            ""id"": ""f6cf29e0-2bae-4038-b0b5-7e468fec15ce"",
            ""actions"": [
                {
                    ""name"": ""Control"",
                    ""type"": ""Value"",
                    ""id"": ""e634087e-5951-483a-89b5-73af4fa6efb7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c5089e66-4838-432a-b549-80d275e59126"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Control"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ddfd7d61-bce3-4cde-809e-e4189ddcb0a1"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Control"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerControls
        m_PlayerControls = asset.FindActionMap("PlayerControls", throwIfNotFound: true);
        m_PlayerControls_Control = m_PlayerControls.FindAction("Control", throwIfNotFound: true);
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

    // PlayerControls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Control;
    public struct PlayerControlsActions
    {
        private @Input m_Wrapper;
        public PlayerControlsActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Control => m_Wrapper.m_PlayerControls_Control;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Control.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnControl;
                @Control.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnControl;
                @Control.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnControl;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Control.started += instance.OnControl;
                @Control.performed += instance.OnControl;
                @Control.canceled += instance.OnControl;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
    public interface IPlayerControlsActions
    {
        void OnControl(InputAction.CallbackContext context);
    }
}
