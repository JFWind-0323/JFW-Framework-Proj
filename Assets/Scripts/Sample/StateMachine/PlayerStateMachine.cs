using Framework.StateMachine.Base;
using Sample.StateMachine.PlayerState;
using UnityEngine;

namespace Sample.StateMachine
{
    public class PlayerStateMachine : StateMachine<PlayerStateBase>
    {
       
        private Animator animator;
        public  void Start()
        {
            animator = GetComponent<Animator>();
            InitALlStates();
            SwitchState(typeof(PlayerStateBaseIdle));
            
        }

        void InitALlStates()
        {
            foreach (var state in stateList)
            {
                state.Init(this,animator);
                //Debug.Log(state);
            }
        }
    }
}
