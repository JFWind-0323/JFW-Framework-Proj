using Framework.StateMachine.Interface;
using UnityEngine;

namespace Framework.StateMachine.Base
{
    public abstract class GenericState<TState> : ScriptableObject, IState where TState : GenericState<TState>
    {
        /*
         * 子状态的父类应从此类继承，便于管理
         */
        public string stateName;
        protected StateMachine<TState> stateMachine;
        public abstract void Init(params object[] args);
        public abstract void Enter();
        public abstract void Exit();
        public abstract void LogicUpdate();
        public abstract void PhisicUpdate();
    }
}