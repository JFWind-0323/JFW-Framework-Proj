using System;

namespace Framework.EDA
{
    public interface ICondition
    {
    }

    public class EDA_Condition : ICondition
    {
        public Func<bool> condition;
    }

    public class EDA_Condition<T> : ICondition
    {
        public Func<T, bool> condition;
    }

    public class EDA_Condition<T1, T2> : ICondition
    {
        public Func<T1, T2, bool> condition;
    }

    public class EDA_Condition<T1, T2, T3> : ICondition
    {
        public Func<T1, T2, T3, bool> condition;
    }

    public class EDA_Condition<T1, T2, T3, T4> : ICondition
    {
        public Func<T1, T2, T3, T4, bool> condition;
    }
}