using Framework.StateMachine.Base;

namespace Framework.StateMachine.Interface
{
	public interface IStateMachine<TState> where TState : GenericState<TState>
	{
		void SwitchState(TState newState);
	}
}
