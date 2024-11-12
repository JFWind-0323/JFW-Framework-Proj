using StateMachine.InterFace;
using UnityEngine;

public abstract class GenericState<TState> : ScriptableObject, IState where TState : GenericState<TState>
{
    protected StateMachine<TState> stateMachine;
    protected Animator animator;
    [SerializeField] protected string stateName;
    [Range(0f, 1f)] protected float duration;
    protected int stateHash;
    protected bool IsAnimationFinished;
    public abstract void Enter();
    public abstract void Exit();
    public abstract void LogicUpdate();
    public abstract void PhisicUpdate();
}