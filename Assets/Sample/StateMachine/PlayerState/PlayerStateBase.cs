using Config;
using Framework.StateMachine.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Sample.StateMachine.PlayerState
{
    public class PlayerStateBase : GenericState<PlayerStateBase>
    {
        public PlayerConfig playerConfig;
        [Range(0f, 1f)] public float duration;
        protected int stateHash => Animator.StringToHash(stateName);
        protected Animator animator;
        protected bool IsAnimationFinished;

        public override void Init(params object[] args)
        {
<<<<<<<< Updated upstream:Assets/Scripts/Sample/StateMachine/PlayerState/PlayerStateBase.cs
            stateMachine = args[0] as StateMachine<PlayerStateBase>;
            animator = args[1] as Animator;
========
            this.stateMachine = args[0] as StateMachine<PlayerStateBase>;
            this.animator = args[1] as Animator;
>>>>>>>> Stashed changes:Assets/Sample/StateMachine/PlayerState/PlayerStateBase.cs
        }

        public override void Enter()
        {
            animator.Play(stateHash);
            stateMachine.gameObject.GetComponent<Image>().color = playerConfig.color;
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