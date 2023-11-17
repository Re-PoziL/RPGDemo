using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum Binding
{
    Bag,
    Store,
    Option
}

public class InputSystem : SingletonMono<InputSystem>
{
    private PlayerInputActions playerInputActions;

    public event EventHandler OnOptionAction;
    public event EventHandler OnBagAction;
    public event EventHandler OnStoreAction;


    private void Awake()
    {
        if (playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();
        }
        playerInputActions.UI.Bag.performed += Bag_performed;
        playerInputActions.UI.Store.performed += Store_performed;
        playerInputActions.UI.Option.performed += Option_performed;
       
    }

    private void Start()
    {
        playerInputActions.UI.Enable();
    }

    private void Option_performed(InputAction.CallbackContext obj)
    {
        OnOptionAction?.Invoke(this,EventArgs.Empty);
    }

    private void Store_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnStoreAction?.Invoke(this, EventArgs.Empty);
    }

    private void Bag_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnBagAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnEnable()
    {
        

    }

    //获取绑定的字符串
    public string GetBinding(Binding binding)
    {
        return binding switch
        {
            Binding.Bag => playerInputActions.UI.Bag.bindings[0].ToDisplayString(),
            Binding.Store => playerInputActions.UI.Store.bindings[0].ToDisplayString(),
            Binding.Option => playerInputActions.UI.Option.bindings[0].ToDisplayString(),
            _ => null,
        };
    }

    //重新设置按键
    public void ReBinding(Binding binding,Action onReBindAction)
    {
        InputAction inputAction = binding switch
        {
            Binding.Bag => playerInputActions.UI.Bag,
            Binding.Store => playerInputActions.UI.Store,
            Binding.Option => playerInputActions.UI.Option,
            _ => null,
        };

        //先停用ui行为组
        playerInputActions.UI.Disable();

        //把要更改的指定行为修改。0代表第一个行为下第一个输入，以此类推
        //start启动过程
        inputAction.PerformInteractiveRebinding(0).OnComplete(callback =>
        {
            callback.Dispose();
            playerInputActions.UI.Enable();
            onReBindAction();
        }).Start();



    }
    
    private void OnDisable()
    {
        playerInputActions.UI.Disable();
        playerInputActions.UI.Bag.performed -= Bag_performed;
        playerInputActions.UI.Store.performed -= Store_performed;
        playerInputActions.UI.Option.performed -= Option_performed;
    }
}
