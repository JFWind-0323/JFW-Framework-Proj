using System.Collections.Generic;
using UnityEngine;
using System;
using StateMachine.InterFace;

public class StateMachine<TState> : MonoBehaviour, IStateMachine<TState> where TState : GenericState<TState>
{
    protected string stateSOPath;
    public TState[] stateList;
    public TState currentState;
    public Dictionary<System.Type, TState> stateTable;
    public virtual void Awake()
    {
        stateTable = new Dictionary<Type, TState>(stateList.Length);
        Initialize(stateSOPath);
    }
    public virtual void Initialize(string stateSOPath)
    {
        if (stateSOPath == string.Empty)
        {
            Debug.LogError("未设置状态列表SO路径");
            Debug.Break();
        }
        stateList = Resources.LoadAll<TState>(stateSOPath);
        if (stateList == null || stateList.Length == 0)
        {
            Debug.LogWarning("状态列表为空");
            Debug.LogError("请检查状态列表SO路径是否正确");
            Debug.Break();
        }
    }

    private void Update()
    {
        currentState.LogicUpdate();

    }

    private void FixedUpdate()
    {
        currentState.PhisicUpdate();
    }

    protected void SwitchOn(TState new_state)
    {
        /*
           进入新状态
            1.将当前状态赋值为新状态
            2.调用其Enter方法
           使用情景
                -初始化玩家状态
                -切换状态
        */

        currentState = new_state;
        currentState.Enter();
    }

    public void SwitchState(TState newState)
    {
        /*
            切换状态
            1.退出当前状态
            2.进入新状态
            使用情景
                -经过一系列逻辑判断需要切换状态时
        */
        currentState.Exit();
        SwitchOn(newState);
    }

    public void SwitchState(Type state_type)
    {
        //重载1,通过System.Type获取到具体状态
        SwitchState(stateTable[state_type]);
    }
}