using Framework.StateMachine.Base;
using UnityEngine;

namespace Sample.StateMachine
{
    public class PlayerStateMachine : StateMachine<PlayerState>
    {
       
        private Animator animator;
        public  void Start()
        {
            animator = GetComponent<Animator>();
            InitALlStates();
            SwitchState(typeof(PlayerStateIdle));
            
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
