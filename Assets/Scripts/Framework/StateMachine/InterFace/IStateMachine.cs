using Framework.StateMachine.Base;

namespace Framework.StateMachine.InterFace
{
	public interface IStateMachine<TState> where TState : GenericState<TState>
	{
		void SwitchState(TState newState);
	}
}
