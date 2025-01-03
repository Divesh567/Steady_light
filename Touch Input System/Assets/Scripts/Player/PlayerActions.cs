//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Scripts/Player/PlayerActions.inputactions
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

public partial class @PlayerActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""Mouse"",
            ""id"": ""a39fb00f-fb58-4c48-9ec2-156faa0c2b83"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""caec2d4b-65f2-4929-aec0-a5f8be2a926c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""DoMove"",
                    ""type"": ""Button"",
                    ""id"": ""ba87a124-80e8-45d1-986f-8c800760a871"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""StopMovement"",
                    ""type"": ""Button"",
                    ""id"": ""e2e4dd4a-2498-4798-bfb7-d99490e4c14b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""88699696-6d43-4920-8254-a5b66e8a90a6"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3af20849-d0b5-4d5a-9152-b6e3b42facff"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59d11d35-2410-41b9-9ff5-71e1998b3b53"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": "";New Control Scheme"",
                    ""action"": ""DoMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af2662b1-d38d-4644-9f2b-3dd6163cfa56"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": "";New Control Scheme"",
                    ""action"": ""DoMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a3e1666c-652d-46c1-bb9d-89d50829145a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StopMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b94d592-8ab5-47fb-9558-cd1392a48cd6"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StopMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""New Control Scheme"",
            ""bindingGroup"": ""New Control Scheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""mobile"",
            ""bindingGroup"": ""mobile"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_Movement = m_Mouse.FindAction("Movement", throwIfNotFound: true);
        m_Mouse_DoMove = m_Mouse.FindAction("DoMove", throwIfNotFound: true);
        m_Mouse_StopMovement = m_Mouse.FindAction("StopMovement", throwIfNotFound: true);
    }

    ~@PlayerActions()
    {
        UnityEngine.Debug.Assert(!m_Mouse.enabled, "This will cause a leak and performance issues, PlayerActions.Mouse.Disable() has not been called.");
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

    // Mouse
    private readonly InputActionMap m_Mouse;
    private List<IMouseActions> m_MouseActionsCallbackInterfaces = new List<IMouseActions>();
    private readonly InputAction m_Mouse_Movement;
    private readonly InputAction m_Mouse_DoMove;
    private readonly InputAction m_Mouse_StopMovement;
    public struct MouseActions
    {
        private @PlayerActions m_Wrapper;
        public MouseActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Mouse_Movement;
        public InputAction @DoMove => m_Wrapper.m_Mouse_DoMove;
        public InputAction @StopMovement => m_Wrapper.m_Mouse_StopMovement;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void AddCallbacks(IMouseActions instance)
        {
            if (instance == null || m_Wrapper.m_MouseActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MouseActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @DoMove.started += instance.OnDoMove;
            @DoMove.performed += instance.OnDoMove;
            @DoMove.canceled += instance.OnDoMove;
            @StopMovement.started += instance.OnStopMovement;
            @StopMovement.performed += instance.OnStopMovement;
            @StopMovement.canceled += instance.OnStopMovement;
        }

        private void UnregisterCallbacks(IMouseActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @DoMove.started -= instance.OnDoMove;
            @DoMove.performed -= instance.OnDoMove;
            @DoMove.canceled -= instance.OnDoMove;
            @StopMovement.started -= instance.OnStopMovement;
            @StopMovement.performed -= instance.OnStopMovement;
            @StopMovement.canceled -= instance.OnStopMovement;
        }

        public void RemoveCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMouseActions instance)
        {
            foreach (var item in m_Wrapper.m_MouseActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MouseActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MouseActions @Mouse => new MouseActions(this);
    private int m_NewControlSchemeSchemeIndex = -1;
    public InputControlScheme NewControlSchemeScheme
    {
        get
        {
            if (m_NewControlSchemeSchemeIndex == -1) m_NewControlSchemeSchemeIndex = asset.FindControlSchemeIndex("New Control Scheme");
            return asset.controlSchemes[m_NewControlSchemeSchemeIndex];
        }
    }
    private int m_mobileSchemeIndex = -1;
    public InputControlScheme mobileScheme
    {
        get
        {
            if (m_mobileSchemeIndex == -1) m_mobileSchemeIndex = asset.FindControlSchemeIndex("mobile");
            return asset.controlSchemes[m_mobileSchemeIndex];
        }
    }
    public interface IMouseActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnDoMove(InputAction.CallbackContext context);
        void OnStopMovement(InputAction.CallbackContext context);
    }
}
