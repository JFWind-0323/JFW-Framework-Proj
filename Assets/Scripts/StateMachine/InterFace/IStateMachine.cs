namespace StateMachine.InterFace
{
	public interface IStateMachine<TState> where TState : GenericState<TState>
	{
		void SwitchState(TState newState);
	}
}
