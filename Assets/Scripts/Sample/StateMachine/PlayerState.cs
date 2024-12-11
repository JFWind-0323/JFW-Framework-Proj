using Framework.DataPersistence;
using Framework.StateMachine.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Sample.StateMachine
{
    public class PlayerState : GenericState<PlayerState>
    {
        public string stateName;
        public PlayerData playerData;
        [Range(0f, 1f)] public float duration;
        protected int stateHash => Animator.StringToHash(stateName);
        protected Animator animator;
        protected bool IsAnimationFinished;

        public override void Init(params object[] args)
        {
            this.stateMachine = args[0] as StateMachine<PlayerState>;
            this.animator = args[1] as Animator;
        }

        public override void Enter()
        {
            animator.Play(stateHash);
            stateMachine.gameObject.GetComponent<Image>().color =playerData.color;
        }

        public override void Exit()
        {
        }

        public override void LogicUpdate()
        {
        }

        public override void PhisicUpdate()
        {
        }
    }
}