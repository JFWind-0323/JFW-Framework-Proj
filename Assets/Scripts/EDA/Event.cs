using System;

namespace EDA
{
    public interface IEvent
    {
    }

    public class Event : IEvent
    {
        public Action action;
    }

    public class Event<T> : IEvent
    {
        public Action<T> action;
    }

    public class Event<T1, T2> : IEvent
    {
        public Action<T1, T2> action;
    }

    public class Event<T1, T2, T3> : IEvent
    {
        public Action<T1, T2, T3> action;
    }

    public class Event<T1, T2, T3, T4> : IEvent
    {
        public Action<T1, T2, T3, T4> action;
    }
}