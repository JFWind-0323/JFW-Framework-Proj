namespace Framework.StateMachine.InterFace
{
    public interface IState
    {
        //单个状态的接口，具体的状态需要继承ScriptableObject（可序列化对象），
        //由于不能多继承，所以使用【接口】来等效【抽象类】
        void Enter();

        void Exit();

        void LogicUpdate();

        void PhisicUpdate();
    }
}