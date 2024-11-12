using System;
using System.Collections.Generic;
using Singleton;
using UnityEngine;

namespace EDA
{
    public class EventCenter : Singleton<EventCenter>
    {
        private readonly Dictionary<EventEnum, IEvent> eventsDic = new();

        #region 注册事件

        public void Register<T>(EventEnum eventEnum, T newEvent) where T : IEvent
        {
            if (eventsDic.TryAdd(eventEnum, newEvent)) Debug.Log("新事件注册" + eventEnum);
        }

        public void UnRegister<T>(EventEnum eventEnum, T oldEvent) where T : IEvent
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("取消注册" + eventEnum);
            eventsDic.Remove(eventEnum);
        }

        #endregion

        #region 订阅事件

        public void Subscribe(EventEnum eventEnum, Action call)
        {
            if (!eventsDic.ContainsKey(eventEnum))
            {
                Debug.Log("未注册事件" + eventEnum);
                return;
            }

            Debug.Log("订阅事件" + eventEnum);
            ((Event)eventsDic[eventEnum]).action += call;
        }

        public void Subscribe<T>(EventEnum eventEnum, Action<T> call)
        {
            if (!eventsDic.ContainsKey(eventEnum))
            {
                Debug.Log("未注册事件" + eventEnum);
                return;
            }

            Debug.Log("订阅事件" + eventEnum);
            ((Event<T>)eventsDic[eventEnum]).action += call;
        }

        public void Subscribe<T1, T2>(EventEnum eventEnum, Action<T1, T2> call)
        {
            if (!eventsDic.ContainsKey(eventEnum))
            {
                Debug.Log("未注册事件" + eventEnum);
                return;
            }

            Debug.Log("订阅事件" + eventEnum);
            ((Event<T1, T2>)eventsDic[eventEnum]).action += call;
        }

        public void Subscribe<T1, T2, T3>(EventEnum eventEnum, Action<T1, T2, T3> call)
        {
            if (!eventsDic.ContainsKey(eventEnum))
            {
                Debug.Log("未注册事件" + eventEnum);
                return;
            }

            Debug.Log("订阅事件" + eventEnum);
            ((Event<T1, T2, T3>)eventsDic[eventEnum]).action += call;
        }

        public void Subscribe<T1, T2, T3, T4>(EventEnum eventEnum, Action<T1, T2, T3, T4> call)
        {
            if (!eventsDic.ContainsKey(eventEnum))
            {
                Debug.Log("未注册事件" + eventEnum);
                return;
            }

            Debug.Log("订阅事件" + eventEnum);
            ((Event<T1, T2, T3, T4>)eventsDic[eventEnum]).action += call;
        }

        #endregion

        #region 取消订阅

        public void Unsubscribe(EventEnum eventEnum, Action call)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("取消订阅" + eventEnum);
            ((Event)eventsDic[eventEnum]).action -= call;
        }

        public void Unsubscribe<T>(EventEnum eventEnum, Action<T> call)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("取消订阅" + eventEnum);
            ((Event<T>)eventsDic[eventEnum]).action -= call;
        }

        public void Unsubscribe<T1, T2>(EventEnum eventEnum, Action<T1, T2> call)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("取消订阅" + eventEnum);
            ((Event<T1, T2>)eventsDic[eventEnum]).action -= call;
        }

        public void Unsubscribe<T1, T2, T3>(EventEnum eventEnum, Action<T1, T2, T3> call)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("取消订阅" + eventEnum);
            ((Event<T1, T2, T3>)eventsDic[eventEnum]).action -= call;
        }

        public void Unsubscribe<T1, T2, T3, T4>(EventEnum eventEnum, Action<T1, T2, T3, T4> call)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("取消订阅" + eventEnum);
            ((Event<T1, T2, T3, T4>)eventsDic[eventEnum]).action -= call;
        }

        #endregion

        #region 触发事件

        public void Invoke(EventEnum eventEnum)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("触发事件" + eventEnum);
            ((Event)eventsDic[eventEnum])?.action.Invoke();
        }

        public void Invoke<T>(EventEnum eventEnum, T arg)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("触发事件" + eventEnum);
            ((Event<T>)eventsDic[eventEnum])?.action.Invoke(arg);
        }

        public void Invoke<T1, T2>(EventEnum eventEnum, T1 arg1, T2 arg2)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("触发事件" + eventEnum);
            ((Event<T1, T2>)eventsDic[eventEnum])?.action.Invoke(arg1, arg2);
        }

        public void Invoke<T1, T2, T3>(EventEnum eventEnum, T1 arg1, T2 arg2, T3 arg3)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("触发事件" + eventEnum);
            ((Event<T1, T2, T3>)eventsDic[eventEnum])?.action.Invoke(arg1, arg2, arg3);
        }

        public void Invoke<T1, T2, T3, T4>(EventEnum eventEnum, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("触发事件" + eventEnum);
            ((Event<T1, T2, T3, T4>)eventsDic[eventEnum])?.action.Invoke(arg1, arg2, arg3, arg4);
        }

        #endregion
    }
}