﻿using System;
using System.Collections.Generic;
using Framework.Singleton;
using UnityEngine;

namespace Framework.EDA
{
    public class EventCenter : Singleton<EventCenter>
    {
        /*
         * 事件中心，用于管理事件的注册、订阅、发布
         * 事件中心的字典中存储着所有注册的事件，通过事件枚举来索引
         * 事件中心的订阅列表中存储着所有订阅了的事件的回调函数，通过事件枚举来索引
         * 请将所有事件都注册在事件中心的构造函数中，需要时调用\
         * 订阅事件时，需要给Subscribe传入泛型参数，并传入回调函数
         */
        private readonly Dictionary<EventEnum, IEvent> eventsDic = new()
        {
            { EventEnum.None, new EDA_Event() },
        };

        #region 注册事件

        /// <summary>
        /// 注册事件,将事件初始化，注册到事件中心的字典中
        /// </summary>
        /// <param dataName="eventEnum"></param>
        /// <param dataName="newEvent"></param>
        /// <typeparam dataName="T"></typeparam>
        public void Register<T>(EventEnum eventEnum, T newEvent) where T : IEvent
        {
            newEvent ??= Activator.CreateInstance<T>();
            if (eventsDic.TryAdd(eventEnum, newEvent))
                Debug.Log("新事件：" + eventEnum + "注册成功");
        }

        /// <summary>
        /// 取消注册事件，从事件中心的字典中移除事件
        /// </summary>
        /// <param dataName="eventEnum"></param>
        /// <param dataName="oldEvent"></param>
        /// <typeparam dataName="T"></typeparam>
        public void UnRegister<T>(EventEnum eventEnum, T oldEvent) where T : IEvent
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("事件：" + eventEnum + " 注销成功");
            eventsDic.Remove(eventEnum);
        }

        #endregion

        #region 订阅事件

        /// <summary>
        /// 订阅事件，将事件的回调函数添加到事件的订阅列表中
        /// </summary>
        /// <param dataName="eventEnum">事件枚举</param>
        /// <param dataName="call">回调函数</param>
        public void AddListener(EventEnum eventEnum, Action call)
        {
            if (!eventsDic.ContainsKey(eventEnum))
            {
                Debug.LogError("未注册事件" + eventEnum);
                return;
            }

            Debug.Log("事件" + eventEnum + " 订阅成功");
            try
            {
                ((EDA_Event)eventsDic[eventEnum]).action += call;
            }
            catch (InvalidCastException)
            {
                Debug.LogError("委托类型不匹配，请检查类型是否正确");
            }
        }

        /// <summary>
        /// 订阅事件，将事件的回调函数添加到事件的订阅列表中
        /// </summary>
        /// <param dataName="eventEnum">事件枚举</param>
        /// <param dataName="call">回调函数</param>
        /// <typeparam dataName="T"></typeparam>
        public void AddListener<T>(EventEnum eventEnum, Action<T> call)
        {
            if (!eventsDic.ContainsKey(eventEnum))
            {
                Debug.LogError("未注册事件" + eventEnum);
                return;
            }

            Debug.Log("事件" + eventEnum + " 订阅成功");
            try
            {
                ((EDA_Event<T>)eventsDic[eventEnum]).action += call;
            }
            catch (InvalidCastException)
            {
                Debug.LogError($"委托类型不匹配，请检查事件：{eventEnum}");
            }
        }

        /// <summary>
        /// 订阅事件，将事件的回调函数添加到事件的订阅列表中
        /// </summary>
        /// <param dataName="eventEnum">事件枚举</param>
        /// <param dataName="call">回调函数</param>
        /// <typeparam dataName="T1"></typeparam>
        /// <typeparam dataName="T2"></typeparam>
        public void AddListener<T1, T2>(EventEnum eventEnum, Action<T1, T2> call)
        {
            if (!eventsDic.ContainsKey(eventEnum))
            {
                Debug.LogError("未注册事件" + eventEnum);
                return;
            }

            Debug.Log("事件" + eventEnum + " 订阅成功");
            try
            {
                ((EDA_Event<T1, T2>)eventsDic[eventEnum]).action += call;
            }
            catch (InvalidCastException)
            {
                Debug.LogError($"委托类型不匹配，请检查事件：{eventEnum}");
            }
        }

        /// <summary>
        /// 订阅事件，将事件的回调函数添加到事件的订阅列表中
        /// </summary>
        /// <param dataName="eventEnum">事件枚举</param>
        /// <param dataName="call">回调函数</param>
        /// <typeparam dataName="T1"></typeparam>
        /// <typeparam dataName="T2"></typeparam>
        /// <typeparam dataName="T3"></typeparam>
        public void AddListener<T1, T2, T3>(EventEnum eventEnum, Action<T1, T2, T3> call)
        {
            if (!eventsDic.ContainsKey(eventEnum))
            {
                Debug.LogError("未注册事件" + eventEnum);
                return;
            }

            Debug.Log("事件" + eventEnum + " 订阅成功");
            try
            {
                ((EDA_Event<T1, T2, T3>)eventsDic[eventEnum]).action += call;
            }
            catch (InvalidCastException)
            {
                Debug.LogError($"委托类型不匹配，请检查事件：{eventEnum}");
            }
        }

        /// <summary>
        /// 订阅事件，将事件的回调函数添加到事件的订阅列表中
        /// </summary>
        /// <param dataName="eventEnum">事件枚举</param>
        /// <param dataName="call">回调函数</param>
        /// <typeparam dataName="T1"></typeparam>
        /// <typeparam dataName="T2"></typeparam>
        /// <typeparam dataName="T3"></typeparam>
        /// <typeparam dataName="T4"></typeparam>
        public void AddListener<T1, T2, T3, T4>(EventEnum eventEnum, Action<T1, T2, T3, T4> call)
        {
            if (!eventsDic.ContainsKey(eventEnum))
            {
                Debug.LogError("未注册事件" + eventEnum);
                return;
            }

            Debug.Log("事件" + eventEnum + " 订阅成功");
            try
            {
                ((EDA_Event<T1, T2, T3, T4>)eventsDic[eventEnum]).action += call;
            }
            catch (InvalidCastException)
            {
                Debug.LogError($"委托类型不匹配，请检查事件：{eventEnum}");
            }
        }

        #endregion

        #region 取消订阅

        /// <summary>
        /// 取消订阅事件，将事件的回调函数从事件的订阅列表中移除
        /// </summary>
        /// <param dataName="eventEnum"></param>
        /// <param dataName="call"></param>
        public void RemoveListener(EventEnum eventEnum, Action call)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("取消订阅" + eventEnum);
            ((EDA_Event)eventsDic[eventEnum]).action -= call;
        }

        /// <summary>
        /// 取消订阅事件，将事件的回调函数从事件的订阅列表中移除
        /// </summary>
        /// <param dataName="eventEnum"></param>
        /// <param dataName="call"></param>
        /// <typeparam dataName="T"></typeparam>
        public void RemoveListener<T>(EventEnum eventEnum, Action<T> call)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("取消订阅" + eventEnum);
            ((EDA_Event<T>)eventsDic[eventEnum]).action -= call;
        }

        /// <summary>
        /// 取消订阅事件，将事件的回调函数从事件的订阅列表中移除
        /// </summary>
        /// <param dataName="eventEnum"></param>
        /// <param dataName="call"></param>
        /// <typeparam dataName="T1"></typeparam>
        /// <typeparam dataName="T2"></typeparam>
        public void RemoveListener<T1, T2>(EventEnum eventEnum, Action<T1, T2> call)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("取消订阅" + eventEnum);
            ((EDA_Event<T1, T2>)eventsDic[eventEnum]).action -= call;
        }

        /// <summary>
        /// 取消订阅事件，将事件的回调函数从事件的订阅列表中移除
        /// </summary>
        /// <param dataName="eventEnum"></param>
        /// <param dataName="call"></param>
        /// <typeparam dataName="T1"></typeparam>
        /// <typeparam dataName="T2"></typeparam>
        /// <typeparam dataName="T3"></typeparam>
        public void RemoveListener<T1, T2, T3>(EventEnum eventEnum, Action<T1, T2, T3> call)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("取消订阅" + eventEnum);
            ((EDA_Event<T1, T2, T3>)eventsDic[eventEnum]).action -= call;
        }

        /// <summary>
        /// 取消订阅事件，将事件的回调函数从事件的订阅列表中移除
        /// </summary>
        /// <param dataName="eventEnum"></param>
        /// <param dataName="call"></param>
        /// <typeparam dataName="T1"></typeparam>
        /// <typeparam dataName="T2"></typeparam>
        /// <typeparam dataName="T3"></typeparam>
        /// <typeparam dataName="T4"></typeparam>
        public void RemoveListener<T1, T2, T3, T4>(EventEnum eventEnum, Action<T1, T2, T3, T4> call)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("取消订阅" + eventEnum);
            ((EDA_Event<T1, T2, T3, T4>)eventsDic[eventEnum]).action -= call;
        }

        #endregion

        #region 触发事件

        /// <summary>
        /// 触发事件，调用事件的回调函数
        /// </summary>
        /// <param dataName="eventEnum"></param>
        public void Invoke(EventEnum eventEnum)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("触发事件" + eventEnum);

            var eventObj = (EDA_Event)eventsDic[eventEnum];
            if (eventObj.action != null)
            {
                eventObj.action.Invoke();
            }
            else
            {
                Debug.LogWarning("事件 " + eventEnum + " 的回调函数未初始化或为空");
            }
        }

        /// <summary>
        /// 触发事件，调用事件的回调函数
        /// </summary>
        /// <param dataName="eventEnum"></param>
        /// <param dataName="arg"></param>
        /// <typeparam dataName="T"></typeparam>
        public void Invoke<T>(EventEnum eventEnum, T arg)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("触发事件" + eventEnum);

            var eventObj = (EDA_Event<T>)eventsDic[eventEnum];
            if (eventObj?.action != null)
            {
                eventObj.action.Invoke(arg);
            }
            else
            {
                Debug.LogWarning("事件 " + eventEnum + " 的回调函数未初始化或为空");
            }
        }

        /// <summary>
        /// 触发事件，调用事件的回调函数
        /// </summary>
        /// <param dataName="eventEnum"></param>
        /// <param dataName="arg1"></param>
        /// <param dataName="arg2"></param>
        /// <typeparam dataName="T1"></typeparam>
        /// <typeparam dataName="T2"></typeparam>
        public void Invoke<T1, T2>(EventEnum eventEnum, T1 arg1, T2 arg2)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("触发事件" + eventEnum);

            var eventObj = (EDA_Event<T1, T2>)eventsDic[eventEnum];
            if (eventObj?.action != null)
            {
                eventObj.action.Invoke(arg1, arg2);
            }
            else
            {
                Debug.LogWarning("事件 " + eventEnum + " 的回调函数未初始化或为空");
            }
        }

        /// <summary>
        /// 触发事件，调用事件的回调函数
        /// </summary>
        /// <param dataName="eventEnum"></param>
        /// <param dataName="arg1"></param>
        /// <param dataName="arg2"></param>
        /// <param dataName="arg3"></param>
        /// <typeparam dataName="T1"></typeparam>
        /// <typeparam dataName="T2"></typeparam>
        /// <typeparam dataName="T3"></typeparam>
        public void Invoke<T1, T2, T3>(EventEnum eventEnum, T1 arg1, T2 arg2, T3 arg3)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("触发事件" + eventEnum);

            var eventObj = (EDA_Event<T1, T2, T3>)eventsDic[eventEnum];
            if (eventObj?.action != null)
            {
                eventObj.action.Invoke(arg1, arg2, arg3);
            }
            else
            {
                Debug.LogWarning("事件 " + eventEnum + " 的回调函数未初始化或为空");
            }
        }

        /// <summary>
        /// 触发事件，调用事件的回调函数
        /// </summary>
        /// <param dataName="eventEnum"></param>
        /// <param dataName="arg1"></param>
        /// <param dataName="arg2"></param>
        /// <param dataName="arg3"></param>
        /// <param dataName="arg4"></param>
        /// <typeparam dataName="T1"></typeparam>
        /// <typeparam dataName="T2"></typeparam>
        /// <typeparam dataName="T3"></typeparam>
        /// <typeparam dataName="T4"></typeparam>
        public void Invoke<T1, T2, T3, T4>(EventEnum eventEnum, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (!eventsDic.ContainsKey(eventEnum)) return;
            Debug.Log("触发事件" + eventEnum);

            var eventObj = (EDA_Event<T1, T2, T3, T4>)eventsDic[eventEnum];
            if (eventObj?.action != null)
            {
                eventObj.action.Invoke(arg1, arg2, arg3, arg4);
            }
            else
            {
                Debug.LogWarning("事件 " + eventEnum + " 的回调函数未初始化或为空");
            }
        }

        #endregion
    }
}