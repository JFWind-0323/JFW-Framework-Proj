using System;
using System.Collections.Generic;
using Framework.Singleton;
using UnityEngine;

namespace Framework.EDA
{
    public class ConditionCenter : Singleton<ConditionCenter>
    {
        private readonly Dictionary<ConditionEnum, ICondition> conditionsDict = new()
        {
            { ConditionEnum.None, new EDA_Condition() },
        };

        #region 条件注册

        public void Register<T>(ConditionEnum conditionEnum, T newCondition) where T : ICondition
        {
            conditionsDict[conditionEnum] ??= Activator.CreateInstance<T>();
            if (conditionsDict.TryAdd(conditionEnum, newCondition))
                Debug.Log(" 新条件： " + conditionEnum + " 注册成功 ");
        }

        public void UnRegister(ConditionEnum conditionEnum)
        {
            if (!conditionsDict.ContainsKey(conditionEnum))
                return;
            conditionsDict.Remove(conditionEnum);
            Debug.Log(" 条件： " + conditionEnum + " 注销成功 ");
        }

        #endregion

        #region 添加条件监听

        public void AddListener(ConditionEnum conditionEnum, Func<bool> condition)
        {
            if (!conditionsDict.ContainsKey(conditionEnum))
            {
                Debug.LogError("条件未注册: " + conditionEnum);
            }

            Debug.Log("条件："+conditionEnum+" 订阅成功");
            try
            {
                ((EDA_Condition)conditionsDict[conditionEnum]).condition += condition;
            }
            catch (InvalidCastException)
            {
                Debug.LogError($"委托类型不匹配,请检查条件：{conditionEnum}");
            }
           
        }

        public void AddListener<T>(ConditionEnum conditionEnum, Func<T, bool> condition) where T : ICondition
        {
            if (!conditionsDict.ContainsKey(conditionEnum))
            {
                Debug.LogError("条件未注册: " + conditionEnum);
            }

            Debug.Log("条件："+conditionEnum+" 订阅成功");
            try
            {
                ((EDA_Condition<T>)conditionsDict[conditionEnum]).condition += condition;
            }
            catch (InvalidCastException)
            {
                Debug.LogError($"委托类型不匹配,请检查条件：{conditionEnum}");
            }
        }

        public void AddListener<T1, T2>(ConditionEnum conditionEnum, Func<T1, T2, bool> condition)
            where T1 : ICondition
        {
            if (!conditionsDict.ContainsKey(conditionEnum))
            {
                Debug.LogError("条件未注册: " + conditionEnum);
            }

            Debug.Log("条件："+conditionEnum+" 订阅成功");
            try
            {
                ((EDA_Condition<T1,T2>)conditionsDict[conditionEnum]).condition += condition;
            }
            catch (InvalidCastException)
            {
                Debug.LogError($"委托类型不匹配,请检查条件：{conditionEnum}");
            }
        }

        public void AddListener<T1, T2, T3>(ConditionEnum conditionEnum, Func<T1, T2, T3, bool> condition)
            where T1 : ICondition
        {
            if (!conditionsDict.ContainsKey(conditionEnum))
            {
                Debug.LogError("条件未注册: " + conditionEnum);
            }

            Debug.Log("条件："+conditionEnum+" 订阅成功");
            try
            {
                ((EDA_Condition<T1,T2,T3>)conditionsDict[conditionEnum]).condition += condition;
            }
            catch (InvalidCastException)
            {
                Debug.LogError($"委托类型不匹配,请检查条件：{conditionEnum}");
            }
        }

        public void AddListener<T1, T2, T3, T4>(ConditionEnum conditionEnum,
            Func<T1, T2, T3, T4, bool> condition)
            where T1 : ICondition
        {
            if (!conditionsDict.ContainsKey(conditionEnum))
            {
                Debug.LogError("条件未注册: " + conditionEnum);
            }

            Debug.Log("条件："+conditionEnum+" 订阅成功");
            try
            {
                ((EDA_Condition<T1, T2, T3, T4>)conditionsDict[conditionEnum]).condition += condition;
            }
            catch (InvalidCastException)
            {
                Debug.LogError($"委托类型不匹配,请检查条件：{conditionEnum}");
            }
        }

        #endregion

        #region 移除条件监听

        public void RemoveListener(ConditionEnum conditionEnum, Func<bool> condition)
        {
            if (!conditionsDict.ContainsKey(conditionEnum))
            {
                Debug.LogError("条件未注册: " + conditionEnum);
            }

            ((EDA_Condition)conditionsDict[conditionEnum]).condition -= condition;
        }

        public void RemoveListener<T>(ConditionEnum conditionEnum, Func<T, bool> condition)
            where T : ICondition
        {
            if (!conditionsDict.ContainsKey(conditionEnum))
            {
                Debug.LogError("条件未注册: " + conditionEnum);
            }

            ((EDA_Condition<T>)conditionsDict[conditionEnum]).condition -= condition;
        }

        public void RemoveListener<T1, T2>(ConditionEnum conditionEnum, Func<T1, T2, bool> condition)
            where T1 : ICondition
        {
            if (!conditionsDict.ContainsKey(conditionEnum))
            {
                Debug.LogError("条件未注册: " + conditionEnum);
            }

            ((EDA_Condition<T1, T2>)conditionsDict[conditionEnum]).condition -= condition;
        }

        public void RemoveListener<T1, T2, T3>(ConditionEnum conditionEnum, Func<T1, T2, T3, bool> condition)
            where T1 : ICondition
        {
            if (!conditionsDict.ContainsKey(conditionEnum))
            {
                Debug.LogError("条件未注册: " + conditionEnum);
            }

            ((EDA_Condition<T1, T2, T3>)conditionsDict[conditionEnum]).condition -= condition;
        }

        public void RemoveListener<T1, T2, T3, T4>(ConditionEnum conditionEnum,
            Func<T1, T2, T3, T4, bool> condition)
            where T1 : ICondition
        {
            if (!conditionsDict.ContainsKey(conditionEnum))
            {
                Debug.LogError("条件未注册: " + conditionEnum);
            }

            ((EDA_Condition<T1, T2, T3, T4>)conditionsDict[conditionEnum]).condition -= condition;
        }

        #endregion

        #region 条件检查

        public bool Check(ConditionEnum conditionEnum)
        {
            if (!conditionsDict.ContainsKey(conditionEnum))
            {
                Debug.LogError("条件未注册: " + conditionEnum);
            }

            return ((EDA_Condition)conditionsDict[conditionEnum]).condition.Invoke();
        }


        public bool Check<T>(ConditionEnum conditionEnum, T arg) where T : ICondition
        {
            if (!conditionsDict.ContainsKey(conditionEnum))
            {
                Debug.LogError("条件未注册: " + conditionEnum);
            }

            return ((EDA_Condition<T>)conditionsDict[conditionEnum]).condition.Invoke(arg);
        }

        public bool Check<T1, T2>(ConditionEnum conditionEnum, T1 arg1, T2 arg2) where T1 : ICondition
        {
            if (!conditionsDict.ContainsKey(conditionEnum))
            {
                Debug.LogError("条件未注册: " + conditionEnum);
            }

            return ((EDA_Condition<T1, T2>)conditionsDict[conditionEnum]).condition.Invoke(arg1, arg2);
        }

        public bool Check<T1, T2, T3>(ConditionEnum conditionEnum, T1 arg1, T2 arg2, T3 arg3)
            where T1 : ICondition
        {
            if (!conditionsDict.ContainsKey(conditionEnum))
            {
                Debug.LogError("条件未注册: " + conditionEnum);
            }

            return ((EDA_Condition<T1, T2, T3>)conditionsDict[conditionEnum]).condition.Invoke(arg1, arg2, arg3);
        }

        public bool Check<T1, T2, T3, T4>(ConditionEnum conditionEnum, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where T1 : ICondition
        {
            if (!conditionsDict.ContainsKey(conditionEnum))
            {
                Debug.LogError("条件未注册: " + conditionEnum);
            }

            return ((EDA_Condition<T1, T2, T3, T4>)conditionsDict[conditionEnum]).condition.Invoke(arg1, arg2, arg3,
                arg4);
        }

        #endregion
    }
}